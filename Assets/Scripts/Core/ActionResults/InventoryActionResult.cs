namespace Core.ActionResults
{
    public class InventoryActionResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        private InventoryActionResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static InventoryActionResult Success(string message = null) =>
            new InventoryActionResult(true, message);

        public static InventoryActionResult Fail(string message) =>
            new InventoryActionResult(false, message);
    }
}