using Game.Popups;
using GameplaySystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseSystem
{
    public class BaseItem
    {
        public Squad CurrentSquad
        {
            get
            {
                return _squadItem;
            }
            private set
            {
                _squadItem = value;
            }
        }
        
        private Squad _squadItem;

        public Color CurrentColor
        {
            get
            {
                return _color;
            }
            private set
            {
                _color = value;
            }
        }

        private Color _color;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }

        private bool _isSelected = false;

        private BaseEmpty _base;
        private BaseView _view;
        private int _countUnits;
        private Action<BaseItem, Squad> _onChangeBaseSquad;


        public BaseItem(BaseView view, Action<BaseItem, Squad> onChangeBaseSquad)
        {
            _view = view;
            _onChangeBaseSquad = onChangeBaseSquad;
        }

        public Collider2D GetCollider() => _view.GetCollider();

        public Raions Raion => _view.Raion;

        public void Init(DataBase dataBase)
        {
            _view.Init();
            _countUnits = dataBase.CountOnStart;
            ChangeSquad(dataBase.ColorSquad, dataBase.CurrentSquad);
            _view.UpdateCounter(_countUnits);
            InitBaseType();
        }

        public void InitBaseType(BaseType type = BaseType.Barracks)
        {
            if (_base != null)
            {
                _base.OnUpdateBases -= UpdateCounter;
            }

            switch (type)
            {
                case BaseType.Barracks:
                    _base = new BarrackBase();
                    break;
                /*
                case BaseType.Factory:
                    _base = new FactoryBase();
                    break;
                case BaseType.Castle:
                    _base = new CastleBase();
                    break;
                */
                default:
                    _base = new CityBase();
                    break;
            }

            _base.OnUpdateBases += UpdateCounter;
        }

        public int GetMovedCountUnits()
        {
            int count = _countUnits / 2;
            _countUnits -= count;
            _view.UpdateCounter(_countUnits);
            return count;
        }

        public Vector3 GetBasePos() => _view.transform.position;

        public void AddedUnit(int count)
        {
            _countUnits += count;
            _view.UpdateCounter(_countUnits);
        }

        public int GetCountUnits() => _countUnits;

        public void ChangeSquad(Color color, Squad squad)
        {
            _onChangeBaseSquad?.Invoke(this, squad);
            _squadItem = squad;
            _color = color;
            _view.ChangeColorSquad(color);
        }

        public void ClickOnBase() => _view.ClickOnBase();

        public void UpdateCounter()
        {
            _countUnits++;
            _view.UpdateCounter(_countUnits);
        }

        public void UpdateBase()
        {
            if (_base == null) return;

            _base.UpdatePower();
        }

        public void SelectBase(PointerEventData data)
        {
            if (!_isSelected) return;
            var p = Camera.main.ScreenToWorldPoint(data.position);
            _view.SetLine(new Vector3(p.x, p.y, _view.gameObject.transform.position.z));
        }

        public void End(PointerEventData data) => _view.SetLine(_view.gameObject.transform.position);
    }

    public abstract class BaseEmpty
    {
        public Action OnUpdateBases;
        public virtual void UpdatePower() { }
    }

    public class CityBase : BaseEmpty
    {

    }

    public class BarrackBase : BaseEmpty
    {
        private float _currentTime;
        private float _step = 1f;

        public override void UpdatePower()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _step)
            {
                OnUpdateBases?.Invoke();
                _currentTime = 0;
            }
        }
    }
    /*

public class CastleBase : IBaseEmpty
{

}

public class FactoryBase : IBaseEmpty
{

}
    */
}
