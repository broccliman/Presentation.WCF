using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace Contracts.Utility
{
    /// <summary>
    /// The utility class for data related operations.
    /// </summary>
    public static class DataUtility
    {
        /// <summary>
        /// Serializes the given object to a string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The serialized obj.</returns>
        public static string SerializeAsString(object obj)
        {
            var blob = SerializeAsBlob(obj);
            return Encoding.UTF8.GetString(blob);
        }

        /// <summary>
        /// Serializes the given object to a byte array.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The serialized obj.</returns>
        public static byte[] SerializeAsBlob(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var xml = new XmlSerializer(obj.GetType());
                xml.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes the byte array into an object of the specified type.
        /// </summary>
        /// <param name="type">The type of the object being deserialized.</param>
        /// <param name="obj">The byte array to deserialize.</param>
        /// <returns>A new object from the byte array.</returns>
        public static object Deserialize(Type type, byte[] obj)
        {
            using (var memStream = new MemoryStream(obj))
            {
                var xml = new XmlSerializer(type);
                return xml.Deserialize(memStream);
            }
        }

        /// <summary>
        /// Compresses the given data.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <returns>The compressed version of the data.</returns>
        public static byte[] Compress(byte[] data)
        {
            using (var streamData = new MemoryStream(data))
            using (var streamCompressed = new MemoryStream())
            using (var deflate = new DeflateStream(streamCompressed, CompressionMode.Compress))
            {
                streamData.CopyTo(deflate);
                deflate.Close();
                return streamCompressed.ToArray();
            }
        }

        /// <summary>
        /// Decompresses the given data.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <returns>The decompressed version of the data.</returns>
        public static byte[] Decompress(byte[] data)
        {
            using (var streamData = new MemoryStream(data))
            using (var streamDecompressed = new MemoryStream())
            using (var deflate = new DeflateStream(streamData, CompressionMode.Decompress))
            {
                deflate.CopyTo(streamDecompressed);
                return streamDecompressed.ToArray();
            }
        }
    }
}