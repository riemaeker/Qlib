using System;
using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
{
	[CustomEditor(typeof(PlainText))]
	public class PlainTextInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var text = (PlainText) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);

			var numChars = Math.Min(1000, text.Text.Length);
			GUILayout.Label(text.Text.Substring(0, numChars));

			if (numChars < text.Text.Length)
			{
				GUILayout.Space(15);
				GUILayout.Label("... (truncated)");
			}
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}