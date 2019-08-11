﻿using System.Collections.Generic;

namespace OpenMessage.Serialisation
{
    /// <summary>
    ///     An instance of a deserializer
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        ///     Determines which content types are supported by this deserializer
        /// </summary>
        IEnumerable<string> SupportedContentTypes { get; }

        /// <summary>
        ///     Deserializes the data to a given T
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="data">The data to convert from</param>
        /// <returns>An instance of T</returns>
        T From<T>(string data);

        /// <summary>
        ///     Deserializes the data to a given T
        /// </summary>
        /// <typeparam name="T">The type to convert to</typeparam>
        /// <param name="data">The data to convert from</param>
        /// <returns>An instance of T</returns>
        T From<T>(byte[] data);
    }
}