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

        ColorateGrid();

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

            switch (colorRGB)
            {

                case "00CFFF":

                    cell.SetTerrainType(CellTerrainType.Terrain2);

                    break;

                case "000000":

                    cell.SetTerrainType(CellTerrainType.Desert1);
                    break;
                default:

                    
                    Debug.Log("colore non trovato " + colorRGB);

                    break;

            }

            

        }
    }

    public CellData FindCell(int x, int z)
    {
        return Cells.Find(c => c.X == x && c.Z == z);
    }

    Color[,] GetGridDataFromTexture(Texture2D _texture, int gridWidth, int gridHeight)
    {

        Color[,] returnColors = new Color[Width,Height];



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

    public void SetCellTerrainType()
    {
        FindCell(5, 5).SetTerrainType(CellTerrainType.Terrain2);
        Debug.Log(FindCell(5, 5).cellTerrainType);
    }

    public void SetCellTexture(CellData _data, GameObject _tile)
    {
        Renderer tileT = _tile.GetComponent<Renderer>();
        switch (_data.cellTerrainType)
        {
            case CellTerrainType.Terrain1:
                Debug.Log("Coglione");
                break;
            case CellTerrainType.Terrain2:
                tileT.material = texture[0];                
                break;
            case CellTerrainType.Desert1:
                tileT.material.color = Color.black;
                break;
            default:
                break;
        }
        Debug.Log(FindCell(_data.X, _data.Z).X + "x" + FindCell(_data.X, _data.Z).Z + " " + tileT.material.mainTexture);
    }

}
