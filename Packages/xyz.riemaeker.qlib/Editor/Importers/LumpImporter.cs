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

			Asset asset = ctx.assetPath switch
			{
				"gfx/palette.lmp" => ScriptableObject.CreateInstance<Palette>(),
				"gfx/colormap.lmp" => ScriptableObject.CreateInstance<ColorMap>(),
				_ => ScriptableObject.CreateInstance<GenericAsset>()
			};

			asset.name = ctx.assetPath;
			asset.Deserialize(data);

			ctx.AddObjectToAsset(ctx.assetPath, asset);
			ctx.SetMainObject(asset);
		}

		#endregion
	}
}