using System;
using System.IO;

using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Image data for menus, HUDs, etc.
	/// </summary>
	public class Picture : Asset, IPalletized
	{
		#region Fields
		
		[SerializeField] private Texture2D _texture;
		[SerializeField] private Palette _palette;
		
		[SerializeField] private int _width;
		[SerializeField] private int _height;
		[SerializeField] private int[] _pixels;
		
		#endregion

		#region Properties
		
		/// <summary>
		/// Width.
		/// </summary>
		public int Width => _width;

		/// <summary>
		/// Width.
		/// </summary>
		public int Height => _height;
		
		/// <summary>
		/// Pixel data (palette indices).
		/// </summary>
		public int[] Pixels => _pixels;
		
		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			var reader = new BinaryReader(new MemoryStream(data));

			_width = (int) reader.ReadInt32();
			_height = (int) reader.ReadInt32();
			
			if (data.Length != _width * _height + sizeof(UInt32) * 2)
				throw new ArgumentException("Picture data has invalid size.");

			_pixels = new int[_width * _height];
			
			for (var i = 0; i < _pixels.Length; i++)
				_pixels[i] = Convert.ToInt32(reader.ReadByte());
		}

		public override byte[] Serialize()
		{
			var data = new byte[_width * _height + sizeof(UInt32) * 2];
			var writer = new BinaryWriter(new MemoryStream(data));
			
			writer.Write(_width);
			writer.Write(_height);
			
			for (var i = 0; i < _pixels.Length; i++)
				writer.Write(Convert.ToByte(_pixels[i]));
			
			return data;
		}
		
		#endregion

		public void Initialize(Palette palette)
		{
			_palette = palette;
		}

		public Texture2D GetTexture()
		{
			if (_texture == null)
				GenerateTexture();

			return _texture;
		}

		private void GenerateTexture()
		{
			var size = _width * _height;

			_texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
			_texture.filterMode = FilterMode.Point;

			var colors = new Color[size];
			int index;

			for (int y = 0; y < _height; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					var pidx = y * _width + x;
					var tidx = (_height - y - 1) * _width + x;
					var cidx = _pixels[pidx];

					if (cidx == 255) // Transparent
						colors[tidx] = new Color(0, 0, 0, 0);
					else
						colors[tidx] = _palette.Colors[_pixels[pidx]];
				}
			}

			_texture.SetPixels(colors);
			_texture.Apply(false);
		}
	}
}