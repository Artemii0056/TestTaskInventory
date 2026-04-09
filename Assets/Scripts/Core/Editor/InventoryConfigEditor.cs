#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    [CustomEditor(typeof(InventoryConfig))]
    public class InventoryConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            InventoryConfig config = (InventoryConfig)target;

            EditorGUILayout.Space();

            if (GUILayout.Button("Fill Default Prices"))
            {
                config.FillDefaultPrices();
            }
        }
    }
}
#endif