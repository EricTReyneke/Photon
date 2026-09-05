namespace Business.Photon.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when an attempt is made to insert
    /// a row using a primary key that already exists within a Photon table.
    /// </summary>
    internal class DuplicatePrimaryKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicatePrimaryKeyException"/>
        /// class with the specified primary key and model type.
        /// </summary>
        /// <param name="primaryKey">The duplicate primary key value.</param>
        /// <param name="modelType">The model type associated with the Photon table.</param>
        public DuplicatePrimaryKeyException(int primaryKey, Type modelType)
            : base(
                $"A row with primary key '{primaryKey}' already exists " +
                $"in the Photon table '{modelType.Name}'.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicatePrimaryKeyException"/>
        /// class with the specified error message.
        /// </summary>
        /// <param name="message">The message describing the error.</param>
        public DuplicatePrimaryKeyException(string message)
            : base(message)
        {
        }
    }
}