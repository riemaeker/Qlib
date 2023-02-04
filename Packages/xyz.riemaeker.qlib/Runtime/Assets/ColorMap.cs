using System;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Maps light levels to the 256-color palette.
	/// </summary>
	public class ColorMap : PalettizedImage
	{
		#region Fields

		private const int Size = 256 * 64;
		
		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			if (data.Length < Size)
				throw new ArgumentException("Color map data has invalid size.");

			_pixels = new int[Size];
			
			for (int i = 0; i < Size; i++)
				_pixels[i] = Convert.ToInt32(data[i]);

			_texture = null;
		}

		public override byte[] Serialize()
		{
			var data = new byte[Size];
			
			for (int i = 0; i < Size; i++)
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