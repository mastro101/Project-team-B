using UnityEngine;

public interface IEnemy {

    int ID { get; }
    int CombatPoint { get; set; }
    int Credits { get; }
    int Attack { get; set; }
    bool IsAlive { get; }
    int Damage { get; }
    int Materiali { get; }
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

public enum IEnemyType
{
    Green,
    Blue,
    Red,
    Yellow,
}