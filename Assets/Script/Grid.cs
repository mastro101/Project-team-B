using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject Tile;
    public GameObject Wall;
    public List<CellData> Cells = new List<CellData>();
    public float CellSize = -1;
    public string NameTile;
    public int Width = 0, Height = 0;

    public float DistanceTile;



    private void Awake()
    {
        Cells = new List<CellData>();
        GridSize(Width, Height);
    }

    void GridSize(int x, int z)
    {
        string ControlCity;

        CellSize = Tile.transform.localScale.x + DistanceTile;

        for (int _x = 0; _x < x; _x++)
        {
            for (int _z = 0; _z < z; _z++)
            {
                
                Cells.Add(new CellData(_x, _z, new Vector3(_x * CellSize, transform.position.y, _z * CellSize), NameTile));
            }
        }

        SetCity();
        SetWalls();

        // Rende visibile la griglia
        for (int _x = 0; _x < x; _x++)
        {
            for (int _z = 0; _z < z; _z++)
            {
                CellData cell = FindCell(_x, _z);
                if (cell.IsValid)
                {
                    GameObject tile = (GameObject)Instantiate(Tile);
                    tile.transform.position = cell.WorldPosition;
                    //Colora il centro
                    if (cell == Center())
                    {
                        tile.GetComponent<Renderer>().material.color = Color.black;
                    }

                    // Colora le città                    
                    ControlCity = FindCell(_x, _z).GetNameTile();
                    if (ControlCity != "")
                        tile.GetComponent<Renderer>().material.color = Color.red;
                    Debug.Log(ControlCity);

                    // Crea Muri (Provvisorio)
                    if (cell.Walls[0] == true)
                    {
                        GameObject wall = (GameObject)Instantiate(Wall);
                        wall.transform.position = cell.WorldPosition + new Vector3(0, 0.5f, 1);
                    }
                    if (cell.Walls[1] == true)
                    {
                        GameObject wall = (GameObject)Instantiate(Wall);
                        wall.transform.position = cell.WorldPosition + new Vector3(1, 0.5f, 0);
                        wall.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }

                }
            }
        }

    }

    // Imposta la posizione delle città sulla griglia
    void SetCity() {
        FindCell(6, 1).SetNameTile("A");
        FindCell(3, 3).SetNameTile("B");
        FindCell(9, 3).SetNameTile("C");
        FindCell(1, 6).SetNameTile("D");
        FindCell(11, 6).SetNameTile("E");
        FindCell(3, 9).SetNameTile("F");
        FindCell(9, 9).SetNameTile("G");
        FindCell(6, 11).SetNameTile("H");
    }

    void SetWalls()
    {
        Center().SetWalls(0);
        FindCell(6, 7).SetWalls(2);
        FindCell(7, 7).SetWalls(1);
        FindCell(8, 7).SetWalls(3);
    }

    public int GetWidth() {
        return Width;
    }

    public int GetHeight() {
        return Height;
    }

    #region API

    public CellData FindCell(int x, int z)
    {
        return Cells.Find(c => c.X == x && c.Z == z);
    }

    public CellData Center()
    {
        int w = Width / 2;
        int h = Height / 2;
        return Cells.Find(c => c.X == w && c.Z == h);
    }

    public Vector3 GetCenterPosition()
    {
        foreach (CellData cell in Cells)
        {
            if (cell.X == Width / 2 && cell.Z == Height / 2)
            {
                return cell.WorldPosition;
            }
        }
        return Cells[0].WorldPosition;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        foreach (CellData cell in Cells)
        {
            if (cell.X == x && cell.Z == z)
            {
                return cell.WorldPosition;
            }
        }
        return Cells[0].WorldPosition;
    }

    public bool IsValidPosition(int x, int z)
    {
        if (x < 0 || z < 0)
            return false;
        if (x > Width - 1 || z > Height - 1)
            return false;

        return true;
    }

        #endregion
}
