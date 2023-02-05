using System;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Maps light levels to the 256-color palette.
	/// </summary>
	public class ColorMap : PalettizedImage
	{
		#region Public methods

		public override void Deserialize(byte[] data)
		{
			_width = 256;
			_height = 64;
			var size = _width * _height;

			if (data.Length < size)
				throw new ArgumentException("Color map data has invalid size.");

			_pixels = new int[size];
			
			for (int i = 0; i < size; i++)
				_pixels[i] = Convert.ToInt32(data[i]);

			_texture = null;
		}

		public override byte[] Serialize()
		{
			var size = _width * _height;
			var data = new byte[size];
			
			for (int i = 0; i < size; i++)
				data[i] = Convert.ToByte(_pixels[i]);

			return data;
		}
		
		/// <summary>
		/// Gets the palette index for the color corresponding to the
		/// given base color at the given light level.
		/// </summary>
		/// <param name="colorIndex">Base color index (0-255).</param>
		/// <param name="lightLevel">Light level (0-64).</param>
		/// <returns>Index into the palette.</returns>
		public int GetColorIndexForLightLevel(int colorIndex, int lightLevel)
		{
			if (colorIndex < 0 || colorIndex > 255)
				throw new ArgumentException("Invalid color index, must be between 0 and 255.");
			
			if (lightLevel < 0 || lightLevel > 64)
				throw new ArgumentException("Invalid light level, must be between 0 and 64.");

			return _pixels[colorIndex * 64 + lightLevel];
		}
		
		#endregion
	}
}