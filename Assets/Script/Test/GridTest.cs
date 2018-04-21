using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour {

    public GameObject Tile;
    public GameObject Wall, City, City2;
    public GameObject Tower;
    public List<CellData> Cells = new List<CellData>();
    public float CellSize = -1;
    public string NameTile;
    public int Width = 0, Height = 0;
    public Material[] texture = new Material[3];

    public Texture2D heightmap, heightmapTerreinType;

    GameObject tile;

    public float DistanceTile;


    private void Awake()
    {
        Cells = new List<CellData>();
        GridSize(Width, Height);
        ColorateGrid();
        
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
                }
            }
        }
    }

    void ColorateGrid()
    {
        Color[,] terrainTypeColors = GetGridDataFromTexture(heightmapTerreinType, Height, Width);

        foreach (var cell in Cells)
        {

            string colorRGB = ColorUtility.ToHtmlStringRGB(terrainTypeColors[(int)cell.WorldPosition.x, (int)cell.WorldPosition.y]);

            switch (colorRGB)
            {

                case "FFFFFF":

                    cell.SetTerrainType(CellTerrainType.Terrain2);
                    

                    break;

                

                default:

                    //Debug.Log("colore non trovato " + colorRGB);

                    break;

            }

            SetCellTexture(cell, tile);

        }
    }

    public CellData FindCell(int x, int z)
    {
        return Cells.Find(c => c.X == x && c.Z == z);
    }

    Color[,] GetGridDataFromTexture(Texture2D _texture, int gridWidth, int gridHeight)
    {

        Color[,] returnColors = new Color[gridWidth, gridHeight];



        int cellWidth = _texture.width / gridWidth;

        int cellHeight = _texture.height / gridHeight;



        foreach (CellData cell in Cells)
        {

            int xPixelPosition = (int)(cell.WorldPosition.x * cellWidth) + (cellWidth / 2);

            int yPixelPosition = (int)(cell.WorldPosition.y * cellHeight) + (cellHeight / 2);

            Color resultColor = _texture.GetPixel(xPixelPosition, yPixelPosition);

            returnColors[(int)cell.WorldPosition.x, (int)cell.WorldPosition.y] = resultColor;



        }

        return returnColors;

    }



    public void SetCellTerrainType()
    {
        FindCell(5, 5).SetTerrainType(CellTerrainType.Terrain2);
        Debug.Log(FindCell(5, 5).cellTerrainType);
    }

    public void SetCellTexture(CellData _data, GameObject _tile)
    {
        Debug.Log(_data.cellTerrainType);
        switch (_data.cellTerrainType)
        {
            case CellTerrainType.Terrain1:
                break;
            case CellTerrainType.Terrain2:
                _tile.GetComponent<Renderer>().material = texture[0];
                Debug.Log(_tile.GetComponent<Renderer>().material.mainTexture);
                break;
            case CellTerrainType.Terrain3:
                break;
            default:
                break;
        }
    }

}
