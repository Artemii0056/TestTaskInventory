namespace Services.Debbuger
{
    public interface IDebugMessageService
    {
        void ShowMessage(string message);
        void ShowErrorMessage(string message);
    }
}