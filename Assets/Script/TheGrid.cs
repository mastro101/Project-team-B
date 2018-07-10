using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGrid : MonoBehaviour {

    public GameObject Tile, TileCredit, TileEnemy, TileEmpty, CenterTile;
    public GameObject Wall, City, City2, City3, City4;
    public GameObject Tower;
    public Material[] TileMaterial = new Material[4];
    public List<CellData> Cells = new List<CellData>();
    public float CellSize = -1;
    public string NameTile;
    public int Width = 0, Height = 0;
    public Material[] material = new Material[3];

    Mission mission;

    public Texture2D heightmapTerreinType;

    GameObject tile;

    public float DistanceTile;


    private void Awake()
    {
        Cells = new List<CellData>();
        mission = FindObjectOfType<Mission>();
        GridSize(Width, Height);
        //SetTower();
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
        setCreditPoint();
        setEmptyPoint();
        //SetCellTerrainType();
        ColorateGrid();

        int []_city = new int[4];

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


                    }
                    else if (FindCell(_x, _z).GetNameTile() != "" && FindCell(_x, _z).GetNameTile() != "Enemy" && FindCell(_x, _z).GetNameTile() != "Empty" && FindCell(_x, _z).GetNameTile() != "Credit")
                    {

                        if (cell.GetNameTile() == "A")
                        {
                            if (_city[0] == 0)
                            {
                                tile = (GameObject)Instantiate(City);
                                tile.transform.position = cell.WorldPosition + new Vector3(1.55f, 0, 1.55f);
                                tile.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            else
                            {
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            _city[0]++;
                        }
                        else if (cell.GetNameTile() == "B")
                        {
                            if (_city[1] == 0)
                            {
                                tile = (GameObject)Instantiate(City2);
                                tile.transform.position = cell.WorldPosition + new Vector3(1.55f, 0, 1.55f);
                                tile.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            else
                            {
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            _city[1]++;
                        }
                        else if (cell.GetNameTile() == "C")
                        {
                            if (_city[2] == 0)
                            {
                                tile = (GameObject)Instantiate(City3);
                                tile.transform.position = cell.WorldPosition + new Vector3(1.55f, 0, 1.55f);
                                tile.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            else
                            {
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            _city[2]++;
                        }
                        else if (cell.GetNameTile() == "D")
                        {
                            if (_city[3] == 0)
                            {
                                tile = (GameObject)Instantiate(City4);
                                tile.transform.position = cell.WorldPosition + new Vector3(1.55f, 0, 1.55f);
                                tile.transform.rotation = Quaternion.Euler(0, 180, 0);
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            else
                            {
                                tile = Instantiate(Tile);
                                tile.transform.position = cell.WorldPosition;
                                tile.GetComponent<Renderer>().enabled = false;
                            }
                            _city[3]++;
                        }
                        else
                        {
                            tile = Instantiate(Tile);
                            tile.transform.position = cell.WorldPosition;
                        }


                    }
                    else if (FindCell(_x, _z).GetNameTile() == "Enemy")
                    {
                        tile = (GameObject)Instantiate(TileEnemy);
                        tile.transform.position = cell.WorldPosition;
                        
                    }
                    else if (FindCell(_x, _z).GetNameTile() == "Empty")
                    {
                        tile = (GameObject)Instantiate(TileEmpty);
                        tile.transform.position = cell.WorldPosition;
                        mission.EmptyCell++;
                        Debug.Log("Empty Cell = " + mission.EmptyCell);
                    }
                    else if (FindCell(_x, _z).GetNameTile() == "Credit")
                    {
                        tile = (GameObject)Instantiate(TileCredit);
                        tile.transform.position = cell.WorldPosition;
                    }

                    cell.Tile = tile;

                    if (cell.GetNameTile() == "" || cell.GetNameTile() == "Enemy")
                        //SetCellTexture(cell, tile);


                    //Colora il centro
                    if (cell == Center())
                    {
                        tile = CenterTile;
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

    void ColorateGrid()
    {
        Color[,] terrainTypeColors = GetGridDataFromTexture(heightmapTerreinType, Width, Height);

        foreach (var cell in Cells)
        {

            string colorRGB = ColorUtility.ToHtmlStringRGB(terrainTypeColors[cell.X, cell.Z]);

            /*
            #00cfff   rgb(0,207,255);
            #15b788   rgb(21,183,136)
            #1c6850   rgb(28,104,80)
            #91e2c9   rgb(145,226,201)
            #b2dbce   rgb(178,219,206)

            #aace11   rgb(170,206,17)
            #cfea5e   rgb(207,234,94)
            #97af41   rgb(151,175,65)
            #5b662b   rgb(91,102,43)
            #d6e28f   rgb(214,226,143)
            #181c09  rgb(24,28,9)
            #145917   rgb(20,89,23)
            #aaaf77   rgb(170,175,119)
            #aaaf77   rgb(170,175,119)

            #1a1468   rgb(26,20,104)
            #678ca5    rgb(103,140,165)
            #0a2a3f   rgb(10,42,63)
            #3b464f   rgb(59,70,79)
            #afbdc6   rgb(175,189,198)

            #7a6a46   rgb(122,106,70)
            */

            switch (colorRGB)
            {

                case "00CFFF":
                    cell.SetTerrainType(CellTerrainType.Water1);
                    break;

                case "15B789":
                    cell.SetTerrainType(CellTerrainType.Water2);
                    break;

                case "1B674F":
                    cell.SetTerrainType(CellTerrainType.Water3);
                    break;

                case "91E1C8":
                    cell.SetTerrainType(CellTerrainType.Water4);
                    break;

                case "B2DBCE":
                    cell.SetTerrainType(CellTerrainType.Water5);
                    break;

                case "AACD10":
                    cell.SetTerrainType(CellTerrainType.Grass1);
                    break;

                case "AACF10":
                    cell.SetTerrainType(CellTerrainType.Grass1);
                    break;

                case "CEE95D":
                    cell.SetTerrainType(CellTerrainType.Grass2);
                    break;

                case "D0EC5D":
                    cell.SetTerrainType(CellTerrainType.Grass2);
                    break;

                case "96AF42":
                    cell.SetTerrainType(CellTerrainType.Grass3);
                    break;

                case "5A6629":
                    cell.SetTerrainType(CellTerrainType.Grass4);
                    break;

                case "5A662B":
                    cell.SetTerrainType(CellTerrainType.Grass4);
                    break;

                case "D6E18E":
                    cell.SetTerrainType(CellTerrainType.Grass5);
                    break;

                case "181C08":
                    cell.SetTerrainType(CellTerrainType.Grass6);
                    break;

                case "181E21":
                    cell.SetTerrainType(CellTerrainType.Desert);
                    break;

                case "145918":
                    cell.SetTerrainType(CellTerrainType.Grass7);
                    break;

                case "125915":
                    cell.SetTerrainType(CellTerrainType.Grass7);
                    break;

                case "155915":
                    cell.SetTerrainType(CellTerrainType.Grass7);
                    break;

                case "AAAF78":
                    cell.SetTerrainType(CellTerrainType.Grass8);
                    break;

                case "A9B077":
                    cell.SetTerrainType(CellTerrainType.Grass8);
                    break;

                case "AAAF75":
                    cell.SetTerrainType(CellTerrainType.Grass8);
                    break;

                case "CBED00":
                    cell.SetTerrainType(CellTerrainType.Grass9);
                    break;

                case "1B1468":
                    cell.SetTerrainType(CellTerrainType.Ancient1);
                    break;

                case "688CA7":
                    cell.SetTerrainType(CellTerrainType.Ancient2);
                    break;

                case "678CA5":
                    cell.SetTerrainType(CellTerrainType.Ancient2);
                    break;

                case "0A2A3F":
                    cell.SetTerrainType(CellTerrainType.Ancient3);
                    break;

                case "3C464F":
                    cell.SetTerrainType(CellTerrainType.Ancient4);
                    break;

                case "AFBDC6":
                    cell.SetTerrainType(CellTerrainType.Ancient5);
                    break;

                case "7B6A47":
                    cell.SetTerrainType(CellTerrainType.Desert);
                    break;

                case "7B6A44":
                    cell.SetTerrainType(CellTerrainType.Desert);
                    break;

                case "7B6946":
                    cell.SetTerrainType(CellTerrainType.Desert);
                    break;

                case "786A44":
                    cell.SetTerrainType(CellTerrainType.Desert);
                    break;

                case "000000":
                    
                    break;

                default:
                    Debug.Log(colorRGB + "   " + cell.X + "x" + cell.Z);
                    break;
            }
        }
    }

    // Imposta la posizione delle città sulla griglia
    void SetCity() {
        
        FindCell(11, 11).SetNameTile("D");
        FindCell(11, 10).SetNameTile("D");
        FindCell(10, 10).SetNameTile("D");
        FindCell(10, 11).SetNameTile("D");

        FindCell(2, 11).SetNameTile("B");
        FindCell(1, 10).SetNameTile("B");
        FindCell(2, 10).SetNameTile("B");
        FindCell(1, 11).SetNameTile("B");

        FindCell(1, 2).SetNameTile("A");
        FindCell(2, 1).SetNameTile("A");
        FindCell(2, 2).SetNameTile("A");
        FindCell(1, 1).SetNameTile("A");

        FindCell(11, 1).SetNameTile("C");
        FindCell(11, 2).SetNameTile("C");
        FindCell(10, 1).SetNameTile("C");
        FindCell(10, 2).SetNameTile("C");

        //FindCell(7, 6).SetNameTile("CittàDebug");
    }

    void SetWalls()
    {
        /*
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
        */
    }

    void SetEnemyPoint() {
        FindCell(0, 1).SetNameTile("Enemy");
        FindCell(0, 5).SetNameTile("Enemy");
        FindCell(0, 7).SetNameTile("Enemy");
        FindCell(0, 11).SetNameTile("Enemy");

        FindCell(1, 0).SetNameTile("Enemy");
        FindCell(1, 12).SetNameTile("Enemy");

        FindCell(2, 3).SetNameTile("Enemy");
        FindCell(2, 5).SetNameTile("Enemy");
        FindCell(2, 7).SetNameTile("Enemy");
        FindCell(2, 9).SetNameTile("Enemy");

        FindCell(3, 2).SetNameTile("Enemy");
        FindCell(3, 10).SetNameTile("Enemy");

        FindCell(5, 0).SetNameTile("Enemy");
        FindCell(5, 2).SetNameTile("Enemy");
        FindCell(5, 6).SetNameTile("Enemy");
        FindCell(5, 10).SetNameTile("Enemy");
        FindCell(5, 12).SetNameTile("Enemy");

        FindCell(6, 5).SetNameTile("Enemy");
        FindCell(6, 7).SetNameTile("Enemy");

        FindCell(7, 0).SetNameTile("Enemy");
        FindCell(7, 2).SetNameTile("Enemy");
        FindCell(7, 6).SetNameTile("Enemy");
        FindCell(7, 10).SetNameTile("Enemy");
        FindCell(7, 12).SetNameTile("Enemy");

        FindCell(9, 2).SetNameTile("Enemy");
        FindCell(9, 10).SetNameTile("Enemy");

        FindCell(10, 3).SetNameTile("Enemy");
        FindCell(10, 5).SetNameTile("Enemy");
        FindCell(10, 7).SetNameTile("Enemy");
        FindCell(10, 9).SetNameTile("Enemy");

        FindCell(11, 0).SetNameTile("Enemy");
        FindCell(11, 12).SetNameTile("Enemy");

        FindCell(12, 1).SetNameTile("Enemy");
        FindCell(12, 5).SetNameTile("Enemy");
        FindCell(12, 7).SetNameTile("Enemy");
        FindCell(12, 11).SetNameTile("Enemy");

    }

    void setCreditPoint()
    {
        FindCell(0, 1).SetNameTile("Credit");
        FindCell(1, 3).SetNameTile("Credit");
        FindCell(1, 9).SetNameTile("Credit");
        FindCell(3, 1).SetNameTile("Credit");
        FindCell(3, 6).SetNameTile("Credit");
        FindCell(3, 11).SetNameTile("Credit");
        FindCell(4, 4).SetNameTile("Credit");
        FindCell(4, 8).SetNameTile("Credit");
        FindCell(6, 0).SetNameTile("Credit");
        FindCell(6, 3).SetNameTile("Credit");
        FindCell(6, 9).SetNameTile("Credit");
        FindCell(6, 12).SetNameTile("Credit");
        FindCell(12, 1).SetNameTile("Credit");
        FindCell(11, 3).SetNameTile("Credit");
        FindCell(11, 9).SetNameTile("Credit");
        FindCell(9, 1).SetNameTile("Credit");
        FindCell(9, 6).SetNameTile("Credit");
        FindCell(9, 11).SetNameTile("Credit");
        FindCell(8, 4).SetNameTile("Credit");
        FindCell(8, 8).SetNameTile("Credit");
    }

    void setEmptyPoint()
    {
        FindCell(0, 2).SetNameTile("Empty");
        FindCell(0, 3).SetNameTile("Empty");
        FindCell(0, 4).SetNameTile("Empty");
        FindCell(0, 8).SetNameTile("Empty");
        FindCell(0, 9).SetNameTile("Empty");
        FindCell(0, 10).SetNameTile("Empty");
        FindCell(1, 4).SetNameTile("Empty");
        FindCell(1, 5).SetNameTile("Empty");
        FindCell(1, 6).SetNameTile("Empty");
        FindCell(1, 7).SetNameTile("Empty");
        FindCell(1, 8).SetNameTile("Empty");
        FindCell(2, 0).SetNameTile("Empty");
        FindCell(2, 6).SetNameTile("Empty");
        FindCell(2, 12).SetNameTile("Empty");
        FindCell(3, 0).SetNameTile("Empty");
        FindCell(3, 3).SetNameTile("Empty");
        FindCell(3, 4).SetNameTile("Empty");
        FindCell(3, 5).SetNameTile("Empty");
        FindCell(3, 7).SetNameTile("Empty");
        FindCell(3, 8).SetNameTile("Empty");
        FindCell(3, 9).SetNameTile("Empty");
        FindCell(3, 12).SetNameTile("Empty");
        FindCell(4, 0).SetNameTile("Empty");
        FindCell(4, 1).SetNameTile("Empty");
        FindCell(4, 3).SetNameTile("Empty");
        FindCell(4, 5).SetNameTile("Empty");
        FindCell(4, 6).SetNameTile("Empty");
        FindCell(4, 7).SetNameTile("Empty");
        FindCell(4, 8).SetNameTile("Empty");
        FindCell(4, 11).SetNameTile("Empty");
        FindCell(4, 12).SetNameTile("Empty");
        FindCell(5, 1).SetNameTile("Empty");
        FindCell(5, 3).SetNameTile("Empty");
        FindCell(5, 4).SetNameTile("Empty");
        FindCell(5, 8).SetNameTile("Empty");
        FindCell(5, 9).SetNameTile("Empty");
        FindCell(5, 11).SetNameTile("Empty");
        FindCell(6, 1).SetNameTile("Empty");
        FindCell(6, 2).SetNameTile("Empty");
        FindCell(6, 4).SetNameTile("Empty");
        FindCell(6, 6).SetNameTile("Empty");
        FindCell(6, 8).SetNameTile("Empty");
        FindCell(6, 10).SetNameTile("Empty");
        FindCell(6, 11).SetNameTile("Empty");
        FindCell(12, 2).SetNameTile("Empty");
        FindCell(12, 3).SetNameTile("Empty");
        FindCell(12, 4).SetNameTile("Empty");
        FindCell(12, 8).SetNameTile("Empty");
        FindCell(12, 9).SetNameTile("Empty");
        FindCell(12, 10).SetNameTile("Empty");
        FindCell(11, 4).SetNameTile("Empty");
        FindCell(11, 5).SetNameTile("Empty");
        FindCell(11, 6).SetNameTile("Empty");
        FindCell(11, 7).SetNameTile("Empty");
        FindCell(11, 8).SetNameTile("Empty");
        FindCell(10, 0).SetNameTile("Empty");
        FindCell(10, 6).SetNameTile("Empty");
        FindCell(10, 12).SetNameTile("Empty");
        FindCell(9, 0).SetNameTile("Empty");
        FindCell(9, 3).SetNameTile("Empty");
        FindCell(9, 4).SetNameTile("Empty");
        FindCell(9, 5).SetNameTile("Empty");
        FindCell(9, 7).SetNameTile("Empty");
        FindCell(9, 8).SetNameTile("Empty");
        FindCell(9, 9).SetNameTile("Empty");
        FindCell(9, 12).SetNameTile("Empty");
        FindCell(8, 0).SetNameTile("Empty");
        FindCell(8, 1).SetNameTile("Empty");
        FindCell(8, 3).SetNameTile("Empty");
        FindCell(8, 5).SetNameTile("Empty");
        FindCell(8, 6).SetNameTile("Empty");
        FindCell(8, 7).SetNameTile("Empty");
        FindCell(8, 8).SetNameTile("Empty");
        FindCell(8, 11).SetNameTile("Empty");
        FindCell(8, 12).SetNameTile("Empty");
        FindCell(7, 1).SetNameTile("Empty");
        FindCell(7, 3).SetNameTile("Empty");
        FindCell(7, 4).SetNameTile("Empty");
        FindCell(7, 8).SetNameTile("Empty");
        FindCell(7, 9).SetNameTile("Empty");
        FindCell(7, 11).SetNameTile("Empty");
    }

    /*void SetTower() {
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


    }*/

    Color[,] GetGridDataFromTexture(Texture2D _texture, int gridWidth, int gridHeight)
    {

        Color[,] returnColors = new Color[Width, Height];



        int cellWidth = 130 / gridWidth;

        int cellHeight = 130 / gridHeight;



        foreach (CellData cell in Cells)
        {

            int xPixelPosition = (int)(cell.X * cellWidth) + (cellWidth / 2);

            int yPixelPosition = (int)(cell.Z * cellHeight) + (cellHeight / 2);

            Color resultColor = _texture.GetPixel(xPixelPosition, yPixelPosition);

            returnColors[(int)cell.X, (int)cell.Z] = resultColor;



        }

        return returnColors;

    }

    /*public void SetCellTerrainType()
    {
        FindCell(5, 5).SetTerrainType(CellTerrainType.Terrain2);
        Debug.Log(FindCell(5, 5).cellTerrainType);
    }
    */


    public void SetCellTexture(CellData _data, GameObject _tile)
    {
        Renderer tileT = _tile.GetComponent<Renderer>();
        
        switch (_data.cellTerrainType)
        {
            case CellTerrainType.Water1:
                tileT.material = material[0];
                break;
            case CellTerrainType.Water2:
                tileT.material = material[1];
                _tile.transform.rotation = Quaternion.Euler(0, 90f, 0);
                break;
            case CellTerrainType.Water3:
                tileT.material = material[2];
                break;
            case CellTerrainType.Water4:
                tileT.material = material[3];
                break;
            case CellTerrainType.Water5:
                tileT.material = material[4];
                break;
            case CellTerrainType.Grass1:
                tileT.material = material[5];
                break;
            case CellTerrainType.Grass2:
                tileT.material = material[6];
                break;
            case CellTerrainType.Grass3:
                tileT.material = material[7];
                break;
            case CellTerrainType.Grass4:
                tileT.material = material[8];
                break;
            case CellTerrainType.Grass5:
                tileT.material = material[9];
                break;
            case CellTerrainType.Grass6:
                tileT.material = material[10];
                break;
            case CellTerrainType.Grass7:
                tileT.material = material[11];
                break;
            case CellTerrainType.Grass8:
                tileT.material = material[12];
                break;
            case CellTerrainType.Grass9:
                tileT.material = material[13];
                break;
            case CellTerrainType.Ancient1:
                tileT.material = material[14];
                break;
            case CellTerrainType.Ancient2:
                tileT.material = material[15];
                break;
            case CellTerrainType.Ancient3:
                tileT.material = material[16];
                _tile.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case CellTerrainType.Ancient4:
                tileT.material = material[17];
                break;
            case CellTerrainType.Ancient5:
                tileT.material = material[18];
                break;
            case CellTerrainType.Desert:
                tileT.material = material[19];
                break;
            default:
                break;
        }
        Debug.Log(FindCell(_data.X, _data.Z).X + "x" + FindCell(_data.X, _data.Z).Z + " " + tileT.material.mainTexture + "" + FindCell(_data.X, _data.Z).cellTerrainType);
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
