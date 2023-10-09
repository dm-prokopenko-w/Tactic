using BaseSystem;
using Game.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySystem
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
    public class GameData : Config
    {
        public ItemData BasesPlayer;
        public List<ItemData> Enemys;

        public DataBase GetDataById(Raions id)
        {
            var playerBase = BasesPlayer.Bases.Find(x => x.Id == id);
            if (playerBase != null)
            {
                return new DataBase(BasesPlayer.CurrentSquad, BasesPlayer.ColorSquad, playerBase.CountOnStart);
            }

            foreach (ItemData item in Enemys)
            {
                var enemyBase = item.Bases.Find(x => x.Id == id);
                if (enemyBase != null)
                {
                    return new DataBase(item.CurrentSquad, item.ColorSquad, enemyBase.CountOnStart);
                }
            }

            return null;
        }
    }

    [Serializable]
    public class ItemData
    {
        public Squad CurrentSquad;
        public Color ColorSquad;
        public List<BaseData> Bases;
    }

    public class DataBase
    {
        public Squad CurrentSquad;
        public Color ColorSquad;
        public int CountOnStart;

        public DataBase(Squad currentSquad, Color colorSquad, int countOnStart)
        {
            CurrentSquad = currentSquad;
            ColorSquad = colorSquad;
            CountOnStart = countOnStart;
        }
    }
}
