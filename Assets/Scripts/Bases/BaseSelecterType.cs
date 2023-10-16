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

        private void Start()
        {
            float angle = Mathf.PI / 2;// * 2;// / numberOfObjects;
            float angleStep = Mathf.PI / 4 - Mathf.PI / 2;
            for (int i = 0; i < numberOfObjects; i++)
            {
                Vector2 pos = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
                Vector3 newPos = new Vector3(
                    _parent.position.x + pos.x,
                    _parent.position.y + pos.y,
                    _parent.position.z);
                var obj = Instantiate(prefab, newPos, Quaternion.identity, _parent);
                obj.SetActive(true);
                angle += angleStep;
            }
        }
    }
}
