namespace InventorySystem.Shared.Tools
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; } = true;

        public List<string> Messages { get; } = [];

        public void Merge(ValidationDetail detail)
        {
            IsValid &= detail.IsValid;

            if (!detail.IsValid && !string.IsNullOrEmpty(detail.Message))
            {
                Messages.Add(detail.Message);
            }
        }
    }
}
