using System;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	///	256-color palette.
	/// </summary>
	public class Palette : Asset
	{
		#region Fields

		[SerializeField] private Color[] _colors = new Color[256];
		[SerializeField] private Texture2D _previewTexture;

		#endregion

		#region Properties
		
		/// <summary>
		/// Palette colors.
		/// </summary>
		public Color[] Colors => _colors;

		/// <summary>
		/// Texture representation of the palette.
		/// </summary>
		public Texture2D PreviewTexture
		{
			get
			{
				if (_previewTexture == null)
					_previewTexture = GeneratePreviewTexture();

				return _previewTexture;
			}
		}
		
		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			if (data.Length != 768)
				throw new ArgumentException("Palette data has invalid size.");
			
			for (int i = 0; i < data.Length / 3; i++)
			{
				Colors[i].r = Convert.ToUInt32(data[i * 3]) / 255f;
				Colors[i].g = Convert.ToUInt32(data[i * 3 + 1]) / 255f;
				Colors[i].b = Convert.ToUInt32(data[i * 3 + 2]) / 255f;
				Colors[i].a = 1;
			}
		}

		public override byte[] Serialize()
		{
			var data = new byte[768];

			for (int i = 0; i < 256; i++)
			{
				data[i * 3] = Convert.ToByte((int) (Colors[i].r * 255));
				data[i * 3 + 1] = Convert.ToByte((int) (Colors[i].g * 255));
				data[i * 3 + 2] = Convert.ToByte((int) (Colors[i].b * 255));
			}

			return data;
		}
		
		#endregion
		
		#region Private methods
		
		private Texture2D GeneratePreviewTexture()
		{
			var texture = new Texture2D(16, 16, TextureFormat.RGB24, false);
			texture.filterMode = FilterMode.Point;
			texture.SetPixels(_colors);
			texture.Apply(false);

			return texture;
		}

		#endregion
	}
}