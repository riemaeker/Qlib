using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
{
	[CustomEditor(typeof(Picture))]
	public class PictureInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var picture = (Picture) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			GUILayout.Label($"{picture.width} x {picture.height}");
			GUILayout.Space(15);
			
			GUILayout.Label("", GUILayout.Height(picture.height * 2), GUILayout.Width(picture.width * 2));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), picture.texture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}