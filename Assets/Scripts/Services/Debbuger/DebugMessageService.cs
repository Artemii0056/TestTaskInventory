using UnityEngine;

namespace Services.Debbuger
{
    public class DebugMessageService : IDebugMessageService
    {
        public void ShowMessage(string message) => 
            Debug.Log(message);

        public void ShowErrorMessage(string message) => 
            Debug.LogError(message);
    }

    public interface IDebugMessageService
    {
        void ShowMessage(string message);
        void ShowErrorMessage(string message);
    }
}