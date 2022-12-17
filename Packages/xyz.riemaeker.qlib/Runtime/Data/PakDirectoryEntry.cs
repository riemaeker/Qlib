using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Qlib.Data
{
	/// <summary>
	///   PAK directory entry.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct PakDirectoryEntry
	{
		/// <summary>
		///   File path.
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.PakAssetPathLength)]
		public byte[] Path;

		/// <summary>
		///   Offset to the file's data.
		/// </summary>
		public uint FileOffset;

		/// <summary>
		///   File size.
		/// </summary>
		public uint FileSize;

		/// <summary>
		///   File path as string.
		/// </summary>
		public string PathString => Encoding.ASCII
			.GetString(Path.TakeWhile((c, _) => c != '\0').ToArray());

		/// <summary>
		///   Data size.
		/// </summary>
		public static readonly int Size = Marshal.SizeOf(typeof(PakDirectoryEntry));
	}
}