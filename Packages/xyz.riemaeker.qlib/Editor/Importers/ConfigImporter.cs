using System.IO;
using Qlib.Assets;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Qlib.Editor
{
	[ScriptedImporter(1, "cfg")]
	public class ConfigImporter : ScriptedImporter
	{
		#region Public methods

		public override void OnImportAsset(AssetImportContext ctx)
		{
			var data = File.ReadAllBytes(ctx.assetPath);

			var asset = ScriptableObject.CreateInstance<PlainText>();
			asset.Deserialize(data);

			ctx.AddObjectToAsset(ctx.assetPath, asset);
			ctx.SetMainObject(asset);
		}

		#endregion
	}
}