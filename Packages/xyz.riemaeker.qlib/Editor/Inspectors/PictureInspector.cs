using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor
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
			
			GUILayout.Label("", GUILayout.Height(picture.GetTexture().height * 2), GUILayout.Width(picture.GetTexture().width * 2));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), picture.GetTexture());
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}