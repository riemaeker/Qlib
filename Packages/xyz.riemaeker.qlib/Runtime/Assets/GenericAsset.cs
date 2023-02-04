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
		
		#region Properties

		public byte[] Data
		{
			get => _data;
			set => _data = value;
		}
		
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