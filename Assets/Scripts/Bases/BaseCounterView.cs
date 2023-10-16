using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaseSystem
{
    public class BaseCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;
        [SerializeField] private Image _icon;

        public void InitView(Color color)
        {
            _icon.color = color;
        }

        public void ChangeCount(int count)
        {
            _counter.text = count.ToString();
        }
    }
}
