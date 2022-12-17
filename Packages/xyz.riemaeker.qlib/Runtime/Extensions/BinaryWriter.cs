using System.IO;
using System.Runtime.InteropServices;

namespace Qlib.Extensions
{
	/// <summary>
	///   Extension methods for BinaryWriter.
	/// </summary>
	public static class BinaryWriterExtensions
	{
		/// <summary>
		///   Writes a struct to the stream.
		/// </summary>
		/// <param name="writer">BinaryWriter object.</param>
		/// <param name="data">Data object to write.</param>
		/// <param name="offset">Offset from beginning of stream.</param>
		public static void WriteStruct(this BinaryWriter writer, object data, long offset = -1)
		{
			if (offset >= 0) writer.BaseStream.Seek(offset, SeekOrigin.Begin);

			var bytes = new byte[Marshal.SizeOf(data.GetType())];
			var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), true);
			writer.BaseStream.Write(bytes, 0, bytes.Length);
			handle.Free();
		}
	}
}