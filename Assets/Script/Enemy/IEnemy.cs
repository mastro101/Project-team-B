using UnityEngine;

public interface IEnemy {

    int ID { get; }
    int WinPoint { get; set; }
    int Credits { get; }
    int Attack { get; set; }
    bool IsAlive { get; }
    Player CurrentPlayer { get; set; }
    GameObject gameObject { get; }
    IEnemyState CurrentState { get; set; }

    void Spawn();
    void DestroyMe();
    void LoseRound();
    void DamagePlayer(Player player);


    event IEnemyEvents.EnemyEvent OnSpawn;
    event IEnemyEvents.EnemyEvent OnAttack;
    event IEnemyEvents.EnemyEvent OnDestroy;

}

public class IEnemyEvents
{
    public delegate void EnemyEvent(IEnemy enemy);
}

public enum IEnemyState
{
    InPool,
    Destroying,
    InUse,
}
