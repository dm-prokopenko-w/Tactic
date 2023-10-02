using UnityEngine;
using UnityEngine.UI;

namespace GameplaySystem
{
    public enum Squad
    {
        None,
        Player,
        EnemyRed,
        EnemyGreen
    }

    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _icon;
        [SerializeField] protected CircleCollider2D _collider;

        protected Squad _squadItem;
        private Color _color;

        public Squad GetSquad() => _squadItem;

        public Color GetColor() => _color;

        public CircleCollider2D GetCollider() => _collider;

        public virtual void ChangeSquad(Color color, Squad s)
        {
            _color = color;
            _icon.color = color;
            _squadItem = s;
        }
    }
}