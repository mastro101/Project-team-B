using System;
using UnityEngine;

[Serializable]
public class CellData
{

    public int X;
    public int Z;
    public Vector3 WorldPosition;
    public bool IsValid = true;
    public string NameTile;

    public CellData(int _xPos, int _zPos, Vector3 _worldPosition, string tile)
    {
        X = _xPos;
        Z = _zPos;
        WorldPosition = _worldPosition;
        NameTile = tile;
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


}