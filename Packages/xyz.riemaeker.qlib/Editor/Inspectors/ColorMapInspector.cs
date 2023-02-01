using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor
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
			
			GUILayout.Label("", GUILayout.Height(colorMap.GetTexture().height * 2), GUILayout.Width(colorMap.GetTexture().width * 2));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), colorMap.GetTexture());
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}