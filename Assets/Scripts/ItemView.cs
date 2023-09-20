using UnityEngine;
using UnityEngine.UI;

public enum Squad
{
    None,
    Player,
    EnemyRed,
    EnemyGreen
}

public enum TypeItemView
{
    None,
    Base,
    Unit,
}

public abstract class ItemView : MonoBehaviour
{
    [SerializeField] protected Image _icon;
    [SerializeField] protected CircleCollider2D _collider;
    [SerializeField] protected Rigidbody2D _rb;

    protected Squad _squadItem;
    private Color _color;

    public Squad GetSquad() => _squadItem;
    public void SetSquad(Squad s) => _squadItem = s;
    public CircleCollider2D GetCollider() => _collider;
    public void ChangeColor(Color color)
    {
        _color = color;
        _icon.color = color;
    }

    public Color GetColor() => _color;
}