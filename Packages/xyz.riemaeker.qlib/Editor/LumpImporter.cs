using System.IO;
using System.Linq;
using Qlib.Assets;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Qlib.Editor
{
	[ScriptedImporter(1, "lmp")]
	public class LumpImporter : ScriptedImporter
	{
		#region Public methods
		
		public override void OnImportAsset(AssetImportContext ctx)
		{
			var data = File.ReadAllBytes(ctx.assetPath);

			Asset asset = ctx.assetPath.Split("/").Last() switch
			{
				"palette.lmp" => ScriptableObject.CreateInstance<Palette>(),
				_ => ScriptableObject.CreateInstance<GenericAsset>()
			};
			
			asset.Deserialize(data);

			ctx.AddObjectToAsset(ctx.assetPath, asset);
			ctx.SetMainObject(asset);
		}

		#endregion
	}
}