using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSystem
{
    public class BaseSelecterType : MonoBehaviour
    {
        [SerializeField] private Transform _city;

        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject prefab;
        private int numberOfObjects = 3;
        private float radius = 0.75f;

        private List<GameObject> _buttons = new List<GameObject>();
        private bool _isActive;

        private void Start()
        {
            float angle = Mathf.PI / 2;// * 2;// / numberOfObjects;
            float angleStep = Mathf.PI / 4 - Mathf.PI / 2;
            _isActive = false;

            for (int i = 0; i < numberOfObjects; i++)
            {
                Vector2 pos = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
                Vector3 newPos = new Vector3(
                    _parent.position.x + pos.x,
                    _parent.position.y + pos.y,
                    _parent.position.z);
                var obj = Instantiate(prefab, newPos, Quaternion.identity, _parent);
                obj.SetActive(_isActive);
                _buttons.Add(obj);
                angle += angleStep;
            }
        }

        public void ActiveButtons()
        {
            _isActive = !_isActive;
            foreach (var button in _buttons)
            {
                button.SetActive(_isActive);
            }
        }
    }
}
