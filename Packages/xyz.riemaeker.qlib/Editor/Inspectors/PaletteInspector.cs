using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
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

			GUILayout.Label("", GUILayout.Height(palette.PreviewTexture.height * 8), GUILayout.Width(palette.PreviewTexture.width * 8));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), palette.PreviewTexture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}
