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
    
    public string Id;
    public Squad SquadItem;
    protected TypeItemView _type;

    public virtual void AddedUnit(int count)
    {
    }

    public virtual string GetTargetId() => null;
    public virtual int GetCountUnits() => 0;

    public virtual TypeItemView GetTypeItemView() => _type;

    public void ChangeColor(Color color)
    {
        _icon.color = color;
    }
}