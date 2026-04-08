using System.Globalization;
using UnityEngine;

namespace UI.InventoryScreen.Views
{
    public class InventoryInfoView : MonoBehaviour
    {
        [SerializeField] private TextView _moneyView;
        [SerializeField] private TextView _weightView;

        public void DrawMoney(float value) => 
            _moneyView.Setup(value.ToString(CultureInfo.InvariantCulture));

        public void DrawWeight(float weight) => 
            _weightView.Setup(weight.ToString(CultureInfo.InvariantCulture));
    }
}