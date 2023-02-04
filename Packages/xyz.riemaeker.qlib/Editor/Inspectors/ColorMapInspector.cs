using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
{
	[CustomEditor(typeof(ColorMap))]
	public class ColorMapInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var colorMap = (ColorMap) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);
			
			GUILayout.Label("", GUILayout.Height(colorMap.texture.height * 2), GUILayout.Width(colorMap.texture.width * 2));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), colorMap.texture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}