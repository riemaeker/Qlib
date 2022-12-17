using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	///   Generic asset, used as a fallback.
	/// </summary>
	public class GenericAsset : Asset
	{
		#region Fields

		[SerializeField] private byte[] _data;

		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			_data = data;
		}

		public override byte[] Serialize()
		{
			return _data;
		}

		#endregion
	}
}