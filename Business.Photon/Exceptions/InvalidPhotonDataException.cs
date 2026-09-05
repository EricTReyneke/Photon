namespace Business.Photon.Exceptions
{
    internal class InvalidPhotonDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataException"/>
        /// class with the specified error message.
        /// </summary>
        /// <param name="message">The message describing the invalid data.</param>
        public InvalidPhotonDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataException"/>
        /// class with the specified error message and underlying exception.
        /// </summary>
        /// <param name="message">The message describing the invalid data.</param>
        /// <param name="innerException">
        /// The exception that caused the current exception.
        /// </param>
        public InvalidPhotonDataException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}