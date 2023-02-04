using System;
using Qlib.Utilities;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Maps light levels to the 256-color palette.
	/// </summary>
	public class ProofOfPurchase : Picture
	{
		#region Fields

		private const int Size = 16 * 16;
		
		#endregion
		
		#region Public methods

		public override void Deserialize(byte[] data)
		{
			if (data.Length < Size)
				throw new ArgumentException("Proof of purchase lump has invalid size.");
			
			_width = 16;
			_height = 16;
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

		#endregion
	}
}