using System;
using UnityEngine;

[Serializable]
public class CellData
{

    public int X;
    public int Z;
    public Vector3 WorldPosition;
    public bool IsValid = true;
    public bool[] Walls = new bool[4];
    public string NameTile;
    public bool Enemy;
    public int PlayerNumber = 0;
    public int PlayerOnTile;
    public Player POnTile;
    public CellTerrainType cellTerrainType;

    public CellData(int _xPos, int _zPos, Vector3 _worldPosition, string tile, bool enemy, Player _pOnTile)
    {
        X = _xPos;
        Z = _zPos;
        WorldPosition = _worldPosition;
        NameTile = tile;
        Enemy = enemy;
        POnTile = _pOnTile;
    }

    public void SetValidity(bool _isValid)
    {
        IsValid = _isValid;
    }

    public bool GetValidity()
    {
        return IsValid;
    }

    public void SetNameTile(string name)
    {
        NameTile = name;
    }

    public string GetNameTile() {
        return NameTile;
    }

    public void SetEnemyNumber(int i)
    {
        PlayerNumber = i;
    }

    public int GetEnemyNumber()
    {
        return PlayerNumber;
    }

    public void SetWalls(int i)
    {
        Walls[i] = true;
    }

    public void SetWalls(int i, int n)
    {
        Walls[i] = true;
        Walls[n] = true;
    }

    public void SetWalls(int i, int n, int m)
    {
        Walls[i] = true;
        Walls[n] = true;
        Walls[m] = true;
    }

    public void SetEnemy(bool _isEnemy)
    {
        Enemy = _isEnemy;
    }

    public bool GetEnemy()
    {
        return Enemy;
    }

    public void SetPlayer(Player _POnTile)
    {
        POnTile = _POnTile;
    }

    public void SetTerrainType(CellTerrainType _type)
    {
        cellTerrainType = _type;
    }
}

public enum CellTerrainType
{
    Terrain1,
    Terrain2,
    Terrain3,
}