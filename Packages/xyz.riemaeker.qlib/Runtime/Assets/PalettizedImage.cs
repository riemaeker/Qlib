using Qlib.Utilities;
using UnityEngine;

namespace Qlib.Assets
{
	public abstract class PalettizedImage : Asset
	{
		[SerializeField] protected int _width;
		[SerializeField] protected int _height;
		[SerializeField] protected int[] _pixels;

		protected Texture2D _texture;
		
		public int width => _width;

		public int height => _height;

		public bool hasAlphaChannel { get; protected set; }

		public Texture2D texture
		{
			get
			{
				if (_texture == null)
					_texture = CreateTexture();

				return _texture;
			}
		}

		protected virtual Texture2D CreateTexture()
		{
			return TextureUtils.CreateTexture(_pixels, width, height, hasAlphaChannel);
		}
	}
}