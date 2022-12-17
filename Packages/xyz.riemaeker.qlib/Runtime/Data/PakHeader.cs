using System.Runtime.InteropServices;

namespace Qlib.Data
{
	/// <summary>
	///   PAK header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct PakHeader
	{
		/// <summary>
		///   PAK format marker.
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] FormatMarker;

		/// <summary>
		///   Offset to the file directory.
		/// </summary>
		public uint DirectoryOffset;

		/// <summary>
		///   Size of the file directory.
		/// </summary>
		public uint DirectorySize;

		/// <summary>
		///   Data size.
		/// </summary>
		public static readonly int Size = Marshal.SizeOf(typeof(PakHeader));
	}
}