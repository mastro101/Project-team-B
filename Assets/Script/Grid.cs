using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject Tile;
    public GameObject Wall, City, City2;
    public GameObject Tower;
    public List<CellData> Cells = new List<CellData>();
    public float CellSize = -1;
    public string NameTile;
    public int Width = 0, Height = 0;
    public Material[] texture = new Material[3];

    GameObject tile;

    public float DistanceTile;


    private void Awake()
    {
        Cells = new List<CellData>();
        GridSize(Width, Height);
        SetTower();
    }

    void GridSize(int x, int z)
    {
        //string ControlCity;
        //bool ControlEnemy = false;

        CellSize = Tile.transform.localScale.x + DistanceTile;

        for (int _x = 0; _x < x; _x++)
        {
            for (int _z = 0; _z < z; _z++)
            {

                Cells.Add(new CellData(_x, _z, new Vector3(_x * CellSize, transform.position.y, _z * CellSize), NameTile, false, null));
            }
        }

        SetCity();
        SetWalls();
        SetEnemyPoint();
        SetCellTerrainType();

        // Rende visibile la griglia
        for (int _x = 0; _x < x; _x++)
        {
            for (int _z = 0; _z < z; _z++)
            {
                CellData cell = FindCell(_x, _z);
                if (cell.IsValid)
                {

                    if (FindCell(_x, _z).GetNameTile() == "")
                    {
                        tile = (GameObject)Instantiate(Tile);
                        tile.transform.position = cell.WorldPosition;
                        SetCellTexture(cell, tile);
                    }
                    else if (FindCell(_x, _z).GetNameTile() != "" && FindCell(_x, _z).GetNameTile() != "Enemy")
                    {
                        int CityChoose = Random.Range(1, 3);
                        if (CityChoose == 1) {
                            tile = (GameObject)Instantiate(City);
                            tile.transform.position = cell.WorldPosition;
                        } else {
                            tile = (GameObject)Instantiate(City2);
                            tile.transform.position = cell.WorldPosition;
                        }


                    }
                    else if (FindCell(_x, _z).GetNameTile() == "Enemy")
                    {
                        tile = (GameObject)Instantiate(Tile);
                        tile.transform.position = cell.WorldPosition;
                        tile.GetComponent<Renderer>().material.color = Color.blue;
                    }


                    //Colora il centro
                    if (cell == Center())
                    {
                        tile.GetComponent<Renderer>().material.color = Color.black;
                    }

                    // Colora le città                    
                    /*ControlCity = FindCell(_x, _z).GetNameTile();
                    if (ControlCity != "")
                        tile.GetComponent<Renderer>().material.color = Color.red;*/

                    // Colora i nemici
                    /*ControlEnemy = FindCell(_x, _z).GetEnemy();
                    if (ControlEnemy)
                    if (ControlCity == "Enemy")*/

                    //Debug.Log(ControlCity);

                    // Crea Muri (Provvisorio)
                    if (cell.Walls[0] == true)
                    {
                        GameObject wall = (GameObject)Instantiate(Wall);
                        wall.transform.position = cell.WorldPosition + new Vector3(0, 0.5f, 1.55f);
                    }
                    if (cell.Walls[1] == true)
                    {
                        GameObject wall = (GameObject)Instantiate(Wall);
                        wall.transform.position = cell.WorldPosition + new Vector3(1.55f, 0.5f, 0);
                        wall.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }

                }
            }
        }

    }

    // Imposta la posizione delle città sulla griglia
    void SetCity() {
        FindCell(6, 11).SetNameTile("A");
        FindCell(6, 1).SetNameTile("B");
        FindCell(1, 6).SetNameTile("C");
        FindCell(11, 6).SetNameTile("D");
        FindCell(11, 11).SetNameTile("E");
        FindCell(1, 11).SetNameTile("F");
        FindCell(1, 1).SetNameTile("G");
        FindCell(11, 1).SetNameTile("H");

        FindCell(7, 6).SetNameTile("CittàDebug");
    }

    void SetWalls()
    {
        //4 centrali
        FindCell(6, 4).SetWalls(0);
        FindCell(6, 5).SetWalls(2);

        FindCell(6, 7).SetWalls(0);
        FindCell(6, 8).SetWalls(2);

        FindCell(4, 6).SetWalls(1);
        FindCell(5, 6).SetWalls(3);

        FindCell(7, 6).SetWalls(1);
        FindCell(8, 6).SetWalls(3);

        //in basso a sinistra
        FindCell(3, 3).SetWalls(0, 3);
        FindCell(3, 4).SetWalls(2);
        FindCell(2, 3).SetWalls(1);

        FindCell(3, 2).SetWalls(3);
        FindCell(2, 2).SetWalls(1);

        FindCell(0, 4).SetWalls(1);
        FindCell(0, 3).SetWalls(1);
        FindCell(0, 2).SetWalls(1);

        FindCell(1, 4).SetWalls(3);
        FindCell(1, 3).SetWalls(3);

        FindCell(1, 2).SetWalls(2, 3);
        FindCell(1, 1).SetWalls(0, 1);

        FindCell(2, 0).SetWalls(0);
        FindCell(3, 1).SetWalls(3);

        FindCell(2, 1).SetWalls(1, 2, 3);

        //in basso a destra
        FindCell(9, 3).SetWalls(2, 3);
        FindCell(9, 2).SetWalls(0);
        FindCell(8, 3).SetWalls(1);

        FindCell(10, 3).SetWalls(2);
        FindCell(10, 2).SetWalls(0);

        FindCell(8, 0).SetWalls(0);
        FindCell(9, 0).SetWalls(0);
        FindCell(10, 0).SetWalls(0);

        FindCell(8, 1).SetWalls(2);
        FindCell(9, 1).SetWalls(2);

        FindCell(10, 1).SetWalls(1, 2);
        FindCell(11, 1).SetWalls(0, 3);

        FindCell(11, 3).SetWalls(2);
        FindCell(12, 2).SetWalls(3);

        FindCell(11, 2).SetWalls(0, 1, 2);

        //in alto a sinistra
        FindCell(3, 9).SetWalls(0, 1);
        FindCell(3, 10).SetWalls(2);
        FindCell(4, 9).SetWalls(3);

        FindCell(2, 10).SetWalls(2);
        FindCell(2, 9).SetWalls(0);
        FindCell(1, 9).SetWalls(0);

        FindCell(1, 11).SetWalls(1, 2);
        FindCell(2, 11).SetWalls(0, 3);

        FindCell(2, 12).SetWalls(2);
        FindCell(3, 12).SetWalls(2);
        FindCell(4, 12).SetWalls(2);

        FindCell(3, 11).SetWalls(0);
        FindCell(4, 11).SetWalls(0);

        FindCell(0, 10).SetWalls(1);

        FindCell(1, 10).SetWalls(0, 2, 3);

        //in alto a destra
        FindCell(9, 9).SetWalls(1, 2);
        FindCell(9, 8).SetWalls(0);
        FindCell(10, 9).SetWalls(3);

        FindCell(9, 11).SetWalls(1);
        FindCell(9, 10).SetWalls(1);
        FindCell(10, 10).SetWalls(3);

        FindCell(10, 12).SetWalls(2);
        FindCell(11, 11).SetWalls(2, 3);
        FindCell(11, 10).SetWalls(0, 1);

        FindCell(11, 9).SetWalls(1);
        FindCell(11, 8).SetWalls(1);

        FindCell(12, 10).SetWalls(3);
        FindCell(12, 9).SetWalls(3);
        FindCell(12, 8).SetWalls(3);

        FindCell(10, 11).SetWalls(0, 1, 3);
    }

    void SetEnemyPoint() {
        FindCell(0, 0).SetNameTile("Enemy");
        FindCell(0, 2).SetNameTile("Enemy");
        FindCell(2, 0).SetNameTile("Enemy");
        FindCell(0, 3).SetNameTile("Enemy");
        FindCell(0, 4).SetNameTile("Enemy");

        FindCell(9, 0).SetNameTile("Enemy");
        FindCell(8, 0).SetNameTile("Enemy");
        FindCell(0, 10).SetNameTile("Enemy");
        FindCell(0, 12).SetNameTile("Enemy");
        FindCell(12, 2).SetNameTile("Enemy");

        FindCell(10, 0).SetNameTile("Enemy");
        FindCell(12, 0).SetNameTile("Enemy");
        FindCell(2, 12).SetNameTile("Enemy");
        FindCell(3, 12).SetNameTile("Enemy");
        FindCell(4, 12).SetNameTile("Enemy");

        FindCell(10, 12).SetNameTile("Enemy");
        FindCell(12, 12).SetNameTile("Enemy");
        FindCell(12, 8).SetNameTile("Enemy");
        FindCell(12, 9).SetNameTile("Enemy");
        FindCell(12, 10).SetNameTile("Enemy");
    }

    void SetTower() {
        FindCell(4, 4).SetValidity(false);
        FindCell(8, 4).SetValidity(false);
        FindCell(4, 8).SetValidity(false);
        FindCell(8, 8).SetValidity(false);

        CellData cell;
        GameObject Tower1 = (GameObject)Instantiate(Tower);

        cell = FindCell(4, 4);
        Tower1.transform.position = cell.WorldPosition;

        cell = FindCell(4, 8);
        Tower1 = (GameObject)Instantiate(Tower);
        Tower1.transform.position = cell.WorldPosition;

        cell = FindCell(8, 4);
        Tower1 = (GameObject)Instantiate(Tower);
        Tower1.transform.position = cell.WorldPosition;

        cell = FindCell(8, 8);
        Tower1 = (GameObject)Instantiate(Tower);
        Tower1.transform.position = cell.WorldPosition;


    }

    public void SetCellTerrainType()
    {
        FindCell(5, 5).SetTerrainType(CellTerrainType.Terrain1);
        Debug.Log(FindCell(5, 5).cellTerrainType);
    }

    public void SetCellTexture(CellData _data, GameObject _tile) {
        Debug.Log(_data.cellTerrainType);
        switch (_data.cellTerrainType)
        {
            case CellTerrainType.Terrain1:
                _tile.GetComponent<Renderer>().material = texture[0];
                Debug.Log(_tile.GetComponent<Renderer>().material.mainTexture);
                break;
            case CellTerrainType.Terrain2:
                break;
            case CellTerrainType.Terrain3:
                break;
            default:
                break;
        }
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

    public CellData GetCity(string city)
    {
        foreach (CellData cell in Cells)
        {
            if (cell.NameTile == city)
                return cell;
        }
        return Cells[0];
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
