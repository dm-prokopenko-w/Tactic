using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class AIModuleController
    {
        protected List<AIEmptyItem> _enemys = new List<AIEmptyItem>();

        public AIModuleController()
        {
            Init();
        }

        protected virtual void Init()
        {
            Debug.Log(999);
        }

        public List<AIEmptyItem> GetItems() => _enemys;

        public virtual void UpdateAI() { }
    }

    public abstract class AIEmptyItem
    {
        public string Id;

        public abstract void UpdateAI();
    }
}
