using UnityEngine;

namespace BaseSystem
{
    [CreateAssetMenu(fileName = "BasesData", menuName = "Configs/BaseData", order = 0)]
    public class BaseData : ScriptableObject
    {
        public Squad SquadType;
        public Color ColorSquad;
        public int CountOnStart;
    }
}