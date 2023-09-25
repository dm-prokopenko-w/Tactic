using BaseSystem;
using GameplaySystem;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace AISystem
{
    public abstract class AIState
    {
        protected AIItem _aiItem;

        public AIItem AIItem { set => _aiItem = value; }

        public abstract void ActiveState(AIController aiController);
    }

    public class IdleAIState : AIState
    {
        public override void ActiveState(AIController aiController)
        {
            _aiItem.SetState(new AttackAIState());
        }
    }

    public class AttackAIState : AIState
    {
        public override void ActiveState(AIController aiController)
        {
            aiController.ActiveState(_aiItem.CurrentSquad);
            _aiItem.SetState(new IdleAIState());
        }
    }
}
