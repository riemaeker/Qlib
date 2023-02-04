using System.IO;
using Qlib.Utilities;
using UnityEditor.AssetImporters;

namespace Qlib.Editor.Importers
{
	public class DefaultImporter : ScriptedImporter
	{
		#region Public methods

		public override void OnImportAsset(AssetImportContext ctx)
		{
			var data = File.ReadAllBytes(ctx.assetPath);
			var asset = AssetUtils.DeserializeAsset(ctx.assetPath, data);
			ctx.AddObjectToAsset(ctx.assetPath, asset);
			ctx.SetMainObject(asset);
		}

		#endregion
	}
	
	[ScriptedImporter(1, "cfg")]
	public class ConfigImporter : DefaultImporter {}
	
	[ScriptedImporter(1, "rc")]
	public class ResourceImporter : DefaultImporter {}
	
	[ScriptedImporter(1, "lmp")]
	public class LumpImporter : DefaultImporter {}
}