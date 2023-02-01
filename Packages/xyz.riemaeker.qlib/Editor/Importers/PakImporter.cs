using Qlib.Assets;
using UnityEditor.AssetImporters;

namespace Qlib.Editor
{
	[ScriptedImporter(1, "pak")]
	public class PakImporter : ScriptedImporter
	{
		#region Public methods

		public override void OnImportAsset(AssetImportContext ctx)
		{
			Package.Import(ctx.assetPath, ctx);
		}

		#endregion
	}
}