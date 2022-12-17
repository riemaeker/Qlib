using System.IO;
using System.Runtime.InteropServices;

namespace Qlib.Extensions
{
	/// <summary>
	///   Extension methods for BinaryReader.
	/// </summary>
	public static class BinaryReaderExtensions
	{
		/// <summary>
		///   Reads a chunk of binary data into a struct.
		/// </summary>
		/// <param name="reader">BinaryReader object.</param>
		/// <param name="offset">Offset from beginning of stream.</param>
		/// <typeparam name="T">Result type.</typeparam>
		public static T ReadStruct<T>(this BinaryReader reader, long offset = -1)
		{
			if (offset >= 0) reader.BaseStream.Seek(offset, SeekOrigin.Begin);

			var bytes = reader.ReadBytes(Marshal.SizeOf(typeof(T)));

			var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			var data = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();

			return data;
		}
	}
}