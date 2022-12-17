using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Qlib.Data;
using Qlib.Extensions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AssetImporters;
#endif

namespace Qlib.Assets
{
	/// <summary>
	///   Asset package (.pak file).
	/// </summary>
	public class Package : ScriptableObject
	{
		#region Private methods

		private static T DeserializeAsset<T>(byte[] data) where T : Asset
		{
			var asset = CreateInstance<T>();
			asset.Deserialize(data);

			return asset;
		}

		#endregion

		#region Properties

		/// <summary>
		///   Assets.
		/// </summary>
		public List<Asset> Assets = new();

		/// <summary>
		///   Asset paths.
		/// </summary>
		public List<string> AssetPaths = new();

		#endregion

		#region Public methods

#if UNITY_EDITOR
		/// <summary>
		///   Imports a package from a .pak file
		/// </summary>
		/// <param name="path">.pak file path.</param>
		/// <param name="context">Asset import context.</param>
		/// <returns></returns>
		public static void Import(string path, AssetImportContext context)
		{
			var package = Load(path);

			context.AddObjectToAsset(context.assetPath, package);
			context.SetMainObject(package);

			for (var i = 0; i < package.Assets.Count; i++)
				context.AddObjectToAsset(package.AssetPaths[i], package.Assets[i]);
		}
#endif

		/// <summary>
		///   Loads a package from a .pak file.
		/// </summary>
		/// <param name="path">.pak file path.</param>
		public static Package Load(string path)
		{
			var data = File.ReadAllBytes(path);
			var reader = new BinaryReader(new MemoryStream(data));

			// Read header and file directory.
			var header = reader.ReadStruct<PakHeader>();

			var formatString = Encoding.ASCII.GetString(header.FormatMarker.Take(4).ToArray());
			if (string.Equals(formatString, Constants.PakHeaderFormat) == false)
				throw new FormatException("Unsupported package format.");

			var fileCount = header.DirectorySize / PakDirectoryEntry.Size;
			var directory = new List<PakDirectoryEntry>((int) fileCount);

			for (var i = 0; i < fileCount; i++)
			{
				var offset = header.DirectoryOffset + i * PakDirectoryEntry.Size;
				directory.Add(reader.ReadStruct<PakDirectoryEntry>(offset));
			}

			// Create package with deserialized assets
			var package = CreateInstance<Package>();

			foreach (var entry in directory)
			{
				var assetData = new byte[entry.FileSize];
				reader.BaseStream.Seek(entry.FileOffset, SeekOrigin.Begin);
				reader.BaseStream.Read(assetData, 0, (int) entry.FileSize);

				Asset asset = entry.PathString.Split(".").Last() switch
				{
					"cfg" or "rc" => DeserializeAsset<PlainText>(assetData),
					_ => DeserializeAsset<GenericAsset>(assetData)
				};

				asset.name = entry.PathString;
				package.AddAsset(entry.PathString, asset);
			}

			return package;
		}

		/// <summary>
		///   Gets an asset from the package.
		/// </summary>
		/// <param name="path">Relative path.</param>
		/// <typeparam name="T">Asset type.</typeparam>
		public T GetAsset<T>(string path) where T : Asset
		{
			var index = AssetPaths.IndexOf(path);

			if (index < 0)
				throw new ArgumentException($"Package doesn't contain asset '{path}'.");

			if (Assets[index] is not T)
				throw new ArgumentException($"Asset is not of the expected type '{typeof(T).Name}'.");

			return (T) Assets[index];
		}

		/// <summary>
		///   Checks whether the package contains an asset with the given path.
		/// </summary>
		/// <param name="path">Relative path.</param>
		public bool ContainsAsset(string path)
		{
			return AssetPaths.Contains(path);
		}

		/// <summary>
		///   Adds an asset to the package.
		/// </summary>
		/// <param name="path">Relative path.</param>
		/// <param name="asset">Asset</param>
		public void AddAsset(string path, Asset asset)
		{
			if (AssetPaths.Contains(path))
				throw new ArgumentException($"Package already contains an asset named '{path}'.");

			if (Encoding.ASCII.GetBytes(path).Length > Constants.PakAssetPathLength)
				throw new ArgumentException($"Filename too long (max. {Constants.PakAssetPathLength} characters)");

			AssetPaths.Add(path);
			Assets.Add(asset);
		}

		/// <summary>
		///   Removes an asset from the package.
		/// </summary>
		/// <param name="path">Relative path.</param>
		public void RemoveAsset(string path)
		{
			if (AssetPaths.Contains(path) == false)
				throw new ArgumentException($"Package doesn't contain asset '{path}'.");

			var index = AssetPaths.IndexOf(path);
			AssetPaths.RemoveAt(index);
			Assets.RemoveAt(index);
		}

		/// <summary>
		///   Serializes and saves the package to a .pak file.
		/// </summary>
		/// <param name="path">Target path.</param>
		/// <param name="overwrite">Whether to overwrite existing files.</param>
		public void Save(string path, bool overwrite = false)
		{
			var writer = new BinaryWriter(new FileStream(path, overwrite ? FileMode.Create : FileMode.CreateNew));

			// Leave space for the header; we'll come back and write it at the end when
			// we know the directory offset.
			writer.Write(new byte[PakHeader.Size]);

			// Write out all the assets and populate the directory.
			var directory = new List<PakDirectoryEntry>(Assets.Count);

			for (var i = 0; i < Assets.Count; i++)
			{
				var assetPath = AssetPaths[i];
				var asset = Assets[i];

				var fileOffset = writer.BaseStream.Position;

				var assetData = asset.Serialize();
				writer.Write(assetData);

				var pathString = Enumerable.Repeat((byte) 0x0, Constants.PakAssetPathLength).ToArray();
				Encoding.ASCII.GetBytes(assetPath).CopyTo(pathString, 0);

				directory.Add(new PakDirectoryEntry
				{
					Path = pathString,
					FileOffset = (uint) fileOffset,
					FileSize = (uint) assetData.Length
				});
			}

			// Write out the directory.
			var directoryOffset = writer.BaseStream.Position;
			foreach (var entry in directory) writer.WriteStruct(entry);

			// Go back and write the header.
			var header = new PakHeader
			{
				FormatMarker = Encoding.ASCII.GetBytes(Constants.PakHeaderFormat),
				DirectoryOffset = (uint) directoryOffset,
				DirectorySize = (uint) (Assets.Count * PakDirectoryEntry.Size)
			};
			writer.WriteStruct(header, 0);

			writer.Close();
		}

		/// <summary>
		///   Extracts all assets to a target directory as individual files.
		/// </summary>
		/// <param name="targetDirectory">Target directory.</param>
		public void ExtractAll(string targetDirectory)
		{ 
			try
			{
				float counter = 0;

				for (var i = 0; i < Assets.Count; i++)
				{
					var assetPath = AssetPaths[i];
					var asset = Assets[i];

					EditorUtility.DisplayProgressBar("Extracting .pak assets...", assetPath, counter / Assets.Count);
					asset.Save(Path.Combine(targetDirectory, assetPath));
				}
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}
		}

		#endregion
	}
}