using Game.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSystem
{
    public class BaseItem
    {
        private IBaseEmpty _base;

        public void Init(BaseType type)
        {

        }

    }

    public interface IBaseEmpty
    {

    }
    public class CityBase: IBaseEmpty
    {

    }

    public class BarrackBase : IBaseEmpty
    {

    }

    public class CastleBase : IBaseEmpty
    {

    }

    public class FactoryBase : IBaseEmpty
    {

    }
}
