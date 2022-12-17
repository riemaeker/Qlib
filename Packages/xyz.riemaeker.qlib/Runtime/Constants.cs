namespace Qlib
{
	public static class Constants
	{
		/// <summary>
		///   Format string used to check whether a .pak file is valid.
		/// </summary>
		public const string PakHeaderFormat = "PACK";

		/// <summary>
		///   Maximum length of an asset path inside a .pak file.
		/// </summary>
		public const int PakAssetPathLength = 56;
	}
}