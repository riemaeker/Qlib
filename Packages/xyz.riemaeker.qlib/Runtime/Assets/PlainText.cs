using System.Text;
using UnityEngine;

namespace Qlib.Assets
{
	/// <summary>
	///   ASCII text asset.
	/// </summary>
	public class PlainText : Asset
	{
		#region Fields

		[SerializeField] private string _text;

		#endregion

		#region Properties

		public string Text
		{
			get => _text;
			set => _text = value;
		}

		#endregion

		#region Public methods

		public override void Deserialize(byte[] data)
		{
			_text = Encoding.ASCII.GetString(data);
		}

		public override byte[] Serialize()
		{
			return Encoding.ASCII.GetBytes(_text);
		}

		#endregion
	}
}