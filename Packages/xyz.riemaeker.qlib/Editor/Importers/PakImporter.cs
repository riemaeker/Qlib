using Qlib.Assets;
using UnityEditor.AssetImporters;

namespace Qlib.Editor.Importers
{
	[ScriptedImporter(1, "pak")]
	public class PakImporter : ScriptedImporter
	{
		#region Public methods

		public override void OnImportAsset(AssetImportContext context)
		{
			var package = Package.Load(context.assetPath);

			context.AddObjectToAsset(context.assetPath, package);
			context.SetMainObject(package);

			for (var i = 0; i < package.Assets.Count; i++)
			{
				context.AddObjectToAsset(package.AssetPaths[i], package.Assets[i]);
			}
		}

		#endregion
	}
}