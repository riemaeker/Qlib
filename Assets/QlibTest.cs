using UnityEngine;
using Qlib.Assets;

public class QlibTest : MonoBehaviour
{
	[SerializeField] private Palette _palette;
	[SerializeField] private ColorMap _colorMap;
	[SerializeField] private Renderer _renderer;

	void Start()
	{ 
		//_colorMap.Initialize(_palette);
		_renderer.material.SetTexture("_MainTex", _colorMap.GetTexture());
	}
}