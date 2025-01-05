namespace InventorySystem.Shared.Tools
{
    public class ValidationDetail(bool isValid, string message = "")
    {
        public bool IsValid { get; } = isValid;

        public string Message { get; } = message;
    }
}
