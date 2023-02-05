using System;
using System.IO;
using Qlib.Utilities;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Image data for menus, HUDs, etc.
	/// </summary>
	public class Picture : PalettizedImage
	{
		#region Properties

		public new bool hasAlphaChannel = true;
		
		#endregion
		
		#region Public methods

		public static Picture Create(Texture2D texture)
		{
			var picture = CreateInstance<Picture>();
			picture.SetPixels(texture);

			return picture;
		}
		
		public override void Deserialize(byte[] data)
		{
			var reader = new BinaryReader(new MemoryStream(data));

			_width = reader.ReadInt32();
			_height = reader.ReadInt32();

			if (data.Length != _width * _height + sizeof(Int32) * 2)
				throw new ArgumentException("Picture data has invalid size.");

			_pixels = new int[_width * _height];
			
			for (var i = 0; i < _pixels.Length; i++)
				_pixels[i] = Convert.ToInt32(reader.ReadByte());
			
			_texture = null;
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

		public void SetPixels(Texture2D tex)
		{
			_width = tex.width;
			_height = tex.height;
			_pixels = TextureUtils.Palettize(tex);
		}
		
		protected override Texture2D CreateTexture()
		{
			// TODO: flip pixels?
			/*
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
			*/
			
			
			return TextureUtils.CreateTexture(_pixels, width, height, hasAlphaChannel);
		}
		
		#endregion
	}
}