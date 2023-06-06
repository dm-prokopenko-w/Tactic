using UnityEngine;

public enum Squad
{
    None,
    Player,
    EnemyRed,    
    GreenRed   
}

public abstract  class Item : MonoBehaviour
{
    public Squad SquadItem;
}
