using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	/// Interface for assets that generate a Texture derived from a <c>Palette</c>.
	/// </summary>
	public interface IPalletized
	{
		/// <summary>
		/// Generatesor updates the asset's texture based on a 256-color palette.
		/// </summary>
		/// <param name="palette"></param>
		public void Initialize(Palette palette);
		
		/// <summary>
		/// Gets the 24-bit texture.
		/// </summary>
		public Texture2D GetTexture();
	}
}