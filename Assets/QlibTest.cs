using UnityEngine;
using Qlib.Assets;

public class QlibTest : MonoBehaviour
{
	[SerializeField] private Package _pak;

	void Start()
	{ 
		if (_pak != null)
		{
			Debug.Log($"Package contains {_pak.Assets.Count} assets.");
		}
	}
}