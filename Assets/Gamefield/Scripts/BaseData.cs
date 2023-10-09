using Game.Configs;
using GameplaySystem;
using UnityEngine;

namespace BaseSystem
{
    [CreateAssetMenu(fileName = "BasesData", menuName = "Configs/BaseData", order = 0)]
    public class BaseData : ScriptableObject
    {
        public Raions Id;
        public int CountOnStart;
    }
}
