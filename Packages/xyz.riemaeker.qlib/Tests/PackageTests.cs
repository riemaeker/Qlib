using System.IO;
using System.Text;
using NUnit.Framework;
using Qlib.Assets;
using UnityEngine;

namespace Qlib.Tests
{
	public class PackageTests
	{
		/// <summary>
		///   Creates a Package in script, adds an asset to it, saves it
		///   to disk and loads it.
		/// </summary>
		[Test]
		public void CreateSaveAndLoad()
		{
			var pakPath = Path.Combine(Application.dataPath, "test.pak"); 
			const string assetPath = "asset.data";
			
			try
			{
				var assetData = Encoding.ASCII.GetBytes("Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn.");

				var asset = ScriptableObject.CreateInstance<GenericAsset>();
				asset.Deserialize(assetData);

				var package = ScriptableObject.CreateInstance<Package>();
				package.AddAsset(assetPath, asset);

				package.Save(pakPath);

				Assert.True(File.Exists(pakPath), "Saved .pak exists.");

				var loadedPackage = Package.Load(pakPath);
				Assert.IsNotNull(loadedPackage, "Loaded Package from file.");
				Assert.AreEqual(package.Assets.Count, loadedPackage.Assets.Count, "Saved Package contains correct number of assets.");

				Assert.IsTrue(loadedPackage.ContainsAsset(assetPath), "Loaded Package contains asset.");

				var loadedAsset = loadedPackage.GetAsset<GenericAsset>(assetPath);
				Assert.IsNotNull(loadedAsset, "Loaded asset from Package.");
				Assert.AreEqual(asset.Serialize(), loadedAsset.Serialize(), "Loaded asset data is intact.");

				loadedPackage.RemoveAsset(assetPath);
				Assert.IsFalse(loadedPackage.ContainsAsset(assetPath), "Removed asset from Package.");
				
				File.Delete(pakPath);
			}
			finally
			{
				if (File.Exists(pakPath))
					File.Delete(pakPath);
			}
		}
	}
}