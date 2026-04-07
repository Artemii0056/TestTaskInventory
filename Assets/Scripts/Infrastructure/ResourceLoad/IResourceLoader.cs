using UnityEngine;

namespace Infrastructure.ResourceLoad
{
    public interface IResourceLoader
    {
        T Load<T>(string path) where T : Object;
        T LoadScriptableObject<T>(string path) where T : ScriptableObject;
        T[] LoadAll<T>(string path) where T : ScriptableObject;
    }
}