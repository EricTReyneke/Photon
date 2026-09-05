namespace Business.Photon.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when a specified primary key
    /// does not exist within a Photon table.
    /// </summary>
    internal class PrimaryKeyNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyNotFoundException"/>
        /// class with the specified primary key and model type.
        /// </summary>
        /// <param name="primaryKey">The primary key that could not be found.</param>
        /// <param name="modelType">The model type associated with the Photon table.</param>
        public PrimaryKeyNotFoundException(int primaryKey, Type modelType)
            : base(
                $"A row with primary key '{primaryKey}' does not exist " +
                $"in the Photon table '{modelType.Name}'.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyNotFoundException"/>
        /// class with the specified error message.
        /// </summary>
        /// <param name="message">The message describing the error.</param>
        public PrimaryKeyNotFoundException(string message)
            : base(message)
        {
        }
    }
}