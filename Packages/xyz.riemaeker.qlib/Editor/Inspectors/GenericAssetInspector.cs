using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
{
	[CustomEditor(typeof(GenericAsset))]
	public class GenericAssetInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var asset = (GenericAsset) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			GUILayout.Label($"Size: {asset.Data.Length} bytes");

			GUILayout.EndVertical();
		}
		
		#endregion
	}
}