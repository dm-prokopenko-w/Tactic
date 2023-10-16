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

        private IBaseEmpty _base;
        private BaseView _view;
        private int _countUnits;
        private bool _isSelected = false;
        private Action<BaseItem, Squad> _onChangeBaseSquad;

        private Squad _squadItem;
        private Color _color;

        public BaseItem(BaseView view, Action<BaseItem, Squad> onChangeBaseSquad)
        {
            _view = view;
            _onChangeBaseSquad = onChangeBaseSquad;
        }

        public Collider2D GetCollider() => _view.GetCollider();

        public void SelectedBase(bool value) => _isSelected = value;

        public bool IsSelectedBase() => _isSelected;

        public Raions Raion => _view.Raion;

        public void Init(DataBase dataBase)
        {
            _view.Init();
            _countUnits = dataBase.CountOnStart;
            ChangeSquad(dataBase.ColorSquad, dataBase.CurrentSquad);
            _view.UpdateCounter(_countUnits);
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

        public void UpdateCounter()
        {
            _countUnits++;
            _view.UpdateCounter(_countUnits);
        }

        public Color GetColor() => _color;

        public void SelectBase(PointerEventData data)
        {
            if (!_isSelected) return;
            var p = Camera.main.ScreenToWorldPoint(data.position);
            _view.SetLine(new Vector3(p.x, p.y, _view.gameObject.transform.position.z));
        }

        public void End(PointerEventData data)  => _view.SetLine(_view.gameObject.transform.position);
    }

    public interface IBaseEmpty
    {

    }
    public class CityBase : IBaseEmpty
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
