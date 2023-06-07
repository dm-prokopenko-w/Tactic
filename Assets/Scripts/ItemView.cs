using UnityEngine;

public enum Squad
{
    None,
    Player,
    EnemyRed,    
    GreenRed   
}

public abstract  class ItemView : MonoBehaviour
{
    public string Id;
    public Squad SquadItem;
}
