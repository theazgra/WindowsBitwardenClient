namespace BitwardenNET.BwLogic
{
    public class AsyncResult<T>
    {
        /// <summary>
        /// Result of the asynchronous operation.
        /// </summary>
        public T Result { get; internal set; }

        /// <summary>
        /// Indicate successful or failed operation.
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Reason of failed operation.
        /// </summary>
        public string ErrorMessage { get; internal set; }
    }
}
