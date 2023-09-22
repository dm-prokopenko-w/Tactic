using BaseSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySystem
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public List<BaseData> BasesPlayer;
        public List<Enemy> Enemy;
    }

    [Serializable]
    public class Enemy
    {
        public string Id;
        public List<BaseData> BasesEnemy;
    }
}
