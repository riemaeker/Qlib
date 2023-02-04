using Qlib.Assets;
using UnityEditor;
using UnityEngine;

namespace Qlib.Editor.Inspectors
{
	[CustomEditor(typeof(ProofOfPurchase))]
	public class ProofOfPurchaseInspector : UnityEditor.Editor
	{
		#region Public methods

		public override void OnInspectorGUI()
		{
			var pop = (ProofOfPurchase) target;

			GUILayout.BeginVertical();
			GUILayout.Space(15);
			
			GUILayout.Label("", GUILayout.Height(pop.texture.height * 2), GUILayout.Width(pop.texture.width * 2));
			GUI.DrawTexture(GUILayoutUtility.GetLastRect(), pop.texture);
			
			GUILayout.EndVertical();
		}

		#endregion
	}
}