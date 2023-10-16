using UnityEngine;

namespace GameplaySystem
{
    public enum Squad
    {
        None,
        Player,
        EnemyRed,
        EnemyGreen,
        EnemyYellow
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

        public void ChangeSquad(Color color, Squad s)
        {
            _color = color;
            _squadItem = s;

            ChangeColorSquad(color);
        }

        public void ChangeColorSquad(Color color)
        {
            _icon.color = color;
        }
    }
}