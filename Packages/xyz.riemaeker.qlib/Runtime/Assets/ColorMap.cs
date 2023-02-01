using System;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Maps light levels to the 256-color palette.
	/// </summary>
	public class ColorMap : Asset, IPalletized
	{
		#region Fields

		private const int Size = 256 * 64;
		
		[SerializeField] private int[] _map = new int[Size];
		[SerializeField] private Texture2D _texture;
		[SerializeField] private Palette _palette;
		
		#endregion

		#region Properties

		/// <summary>
		/// Color mapping data.
		/// </summary>
		public int[] Map => _map;
		
		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			if (data.Length < Size)
				throw new ArgumentException("Color map data has invalid size.");

			for (int i = 0; i < Size; i++)
				_map[i] = Convert.ToInt32(data[i]);
		}

		public override byte[] Serialize()
		{
			var data = new byte[Size];
			
			for (int i = 0; i < Size; i++)
				data[i] = Convert.ToByte(_map[i]);

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

			return _map[colorIndex * 64 + lightLevel];
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
			_texture = new Texture2D(256, 64, TextureFormat.RGB24, false);
			_texture.filterMode = FilterMode.Point;	

			var colors = new Color[Size];
			
			for (int i = 0; i < Size; i++)
				colors[i] = _palette.Colors[_map[i]];

			_texture.SetPixels(colors);
			_texture.Apply(false);
		}
	}
}