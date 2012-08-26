using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Contracts.Utility;

namespace Contracts.Portal
{
    /// <summary>
    /// The wrapper for the portal payload.
    /// </summary>
    [Serializable]
    public class PortalPayload
    {
		/// <summary>
		/// The formatter.
		/// </summary>
		[NonSerialized]
		private static readonly BinaryFormatter Formatter = new BinaryFormatter();

    	/// <summary>
    	/// The message.
    	/// </summary>
		private readonly byte[] _message;

    	/// <summary>
    	/// The message type.
    	/// </summary>
    	private readonly Type _messageType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalPayload" /> class.
        /// </summary>
        private PortalPayload(object message)
        {
            _message = DataUtility.SerializeAsBlob(message);
            _messageType = message.GetType();
        }
		
        /// <summary>
        /// Converts the message to a byte array.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The byte array.</returns>
        public static byte[] ToByteArray(object message)
        {
            var payload = new PortalPayload(message);

            byte[] data;

            using (var ms = new MemoryStream())
            {
				Formatter.Serialize(ms, payload);
                data = ms.ToArray();
            }

            return DataUtility.Compress(data);
        }

        /// <summary>
        /// Deserializes the given byte array.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static object FromByteArray(byte[] payload)
        {
            var data = DataUtility.Decompress(payload);

            using (var ms = new MemoryStream(data))
            {
				var portalPayload = (PortalPayload)(Formatter.Deserialize(ms));
                return DataUtility.Deserialize(portalPayload._messageType, portalPayload._message);
            }
        }
    }
}