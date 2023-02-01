using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor
{
	[CustomEditor(typeof(Package))]
	public class PackageInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var Target = (Package) target;

			GUI.enabled = true;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			if (GUILayout.Button("Extract all assets..."))
			{
				var targetFolder = EditorUtility.SaveFolderPanel(
					"Extract .pak assets to folder",
					Application.dataPath,
					Target.name);

				if (targetFolder != null) Target.ExtractAll(targetFolder);
			}

			GUILayout.EndVertical();
		}

		#endregion
	}
}