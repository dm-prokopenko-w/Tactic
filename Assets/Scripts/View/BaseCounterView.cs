using TMPro;
using UnityEngine;

namespace BaseSystem
{
    public class BaseCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public void ChangeCount(int count)
        {
            Debug.Log(count);
            _counter.text = count.ToString();
        }
    }
}
