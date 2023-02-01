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
			var palette = (Palette) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			GUILayout.Label("", GUILayout.Height(128), GUILayout.Width(128));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), palette.PreviewTexture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}
