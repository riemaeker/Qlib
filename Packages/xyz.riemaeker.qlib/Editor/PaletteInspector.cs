using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor
{
	[CustomEditor(typeof(Palette))]
	public class PaletteInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var Target = (Palette) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			GUILayout.Label("", GUILayout.Height(256), GUILayout.Width(256));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), Target.PreviewTexture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}
