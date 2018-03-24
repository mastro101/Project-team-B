﻿using UnityEngine;

public interface IEnemy {

    int ID { get; }
    int Stamina { get; }
    int CombatPoints { get; }
    int Credits { get; }
    int Attack { get; }
    bool IsAlive { get; }
    GameObject gameObject { get; }
    IEnemyState CurrentState { get; set; }

    void Spawn();
    void DestroyMe();
    void TakeDamage(int damage);
    void AttackPlayer(Player player);


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
