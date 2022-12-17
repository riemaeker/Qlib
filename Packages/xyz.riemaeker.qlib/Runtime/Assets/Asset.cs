using System;
using System.IO;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	///   Base class for serializable/deserializable assets.
	/// </summary>
	public abstract class Asset : ScriptableObject
	{
		#region Public methods

		public virtual void Deserialize(byte[] data)
		{
		}

		public virtual byte[] Serialize()
		{
			return null;
		}

		/// <summary>
		///   Serializes the asset and writes it to a file.
		/// </summary>
		/// <param name="path">Destination path (including file name).</param>
		public void Save(string path)
		{
			var data = Serialize();
			if (data == null || data.Length == 0) throw new Exception("No data to write.");

			var targetDirectory = Path.GetDirectoryName(path);
			if (targetDirectory == null)
				throw new Exception($"Invalid destination path '{path}'.");

			Directory.CreateDirectory(targetDirectory);
			File.WriteAllBytes(path, Serialize());
		}

		#endregion
	}
}