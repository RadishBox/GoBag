using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    public GameObject GridTilePrefab;
    public string[] tilesString;
    private BagGridTile[,] _tiles;

    [SerializeField]
    private float tileWidth;
    [SerializeField]
    private float tileHeight;

    [SerializeField]
    private int _baseHeight;
    [SerializeField]
    private int _baseWidth;
    [SerializeField]
    private int _maximumHeight;
    [SerializeField]
    private int _maximumWidth;

    // Use this for initialization
    void Start ()
    {
        GenerateBaseGrid();
        GenerateGrid();
        GenerateRandomGrid();
    }

    private void GenerateRandomGrid()
    {
        int addedTiles = 0;
        int addedTilesLimit = 0;
        int currentLevelHeight = ExploreGameController.Instance.Map.GetComponent<MapController>().Height * 250;

        if (currentLevelHeight <= 1350)
        {
            addedTilesLimit = 3;
        }
        else if (currentLevelHeight > 1350 && currentLevelHeight <= 7500 )
        {
            addedTilesLimit = 7;
        }
        else
        {
            addedTilesLimit = 10;
        }

        // From base grid with base height and base width modify it randomly
        for (int i = 0; i < Tiles.GetLength(1); i++)
        {
            for (int j = 0; j < Tiles.GetLength(0); j++)
            {
                if(addedTiles >= addedTilesLimit)
                {
                   return;
                }
                if (!Tiles[j, i].Active)
                {
                    bool hasAdjAvailableTile = false;
                    if (i != 0)
                    {
                        // Check down
                        hasAdjAvailableTile |= Tiles[j, i - 1].Active;
                    }
                    if (j != 0)
                    {
                        // Check left
                        hasAdjAvailableTile |= Tiles[j - 1, i].Active;
                    }
                    if (i < Tiles.GetLength(1) - 1)
                    {
                        // Check up
                        hasAdjAvailableTile |= Tiles[j, i + 1].Active;
                    }
                    if (j < Tiles.GetLength(0) - 1)
                    {
                        // Check right
                        hasAdjAvailableTile |= Tiles[j + 1, i].Active;
                    }

                    if (hasAdjAvailableTile)
                    {
                        // Check for probability to make this tile availble
                        float prob = Random.Range(0.0f, 1.0f);
                        if (prob < 0.5f)
                        {
                            Tiles[j, i].Active = true;
                            Tiles[j, i].GetComponent<Image>().color = Color.white;
                            addedTiles++;
                        }
                    }
                } 
                
            }
        }
    }

    public virtual void GenerateGrid()
    {
        for (int i = 0; i < Tiles.GetLength(1); i++)
        {
            // Get Row
            string row = tilesString[i];
            for (int j = 0; j < Tiles.GetLength(0); j++)
            {
                if (row[j] == '0')
                {
                    Color invisibleColor = new Color(1, 1, 1, 0);
                    Tiles[j, i].GetComponent<Image>().color = invisibleColor;
                    Tiles[j, i].Active = false;
                }
                Tiles[j, i].CalculateCenterPoint();
                Tiles[j, i].GridCoords = new Vector2(j, i);
            }
        }
    }

    public virtual void GenerateBaseGrid()
    {
        Tiles = new BagGridTile[MaximumWidth, MaximumHeight];
        for (int i = 0; i < MaximumHeight; i++)
        {
            for (int j = 0; j < MaximumWidth; j++)
            {
                GameObject tile = GameObject.Instantiate(GridTilePrefab);
                tile.transform.SetParent(this.transform);
                tile.transform.localScale = Vector3.one;
                tile.GetComponent<RectTransform>().sizeDelta = new Vector2(TileWidth, TileHeight);
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector3(j * TileWidth, -i * TileHeight);
                Tiles[j, i] = tile.GetComponent<BagGridTile>();
            }
        }
    }

    public bool CheckIfFits(Item item, Vector2 PosInGrid)
    {
        int[,] itemTiles = item.Tiles;
        Vector2[,] itemTilesInGrid = item.TilesInGrid;

        print(itemTiles.GetLength(0));
        print(itemTiles.GetLength(1));

        for (int i = 0; i < itemTiles.GetLength(0); i++)
        {
            for (int j = 0; j < itemTiles.GetLength(1); j++)
            {
                // Project item's tiles in grid
                Vector2 tileLocalCoords = new Vector2(j, i); // Tile's local coords
                Vector2 tileGridCoords = tileLocalCoords + PosInGrid - item.DragPivotCoords;

                print(tileGridCoords + " = " + tileLocalCoords + " + " + PosInGrid + " - " + item.DragPivotCoords);


                if (itemTiles[(int)tileLocalCoords.y, (int)tileLocalCoords.x] != 0) // If this item's tile is not blank
                {

                    bool underflow = (tileGridCoords.x < 0) || (tileGridCoords.y < 0);
                    bool overflow = (tileGridCoords.x > Tiles.GetLength(1)) || (tileGridCoords.y > Tiles.GetLength(0));

                    // Check if the tile projection is out of the grid's bounds
                    if (underflow || overflow)
                    {
                        return false;
                    }

                    print(Tiles[(int)tileGridCoords.x, (int)tileGridCoords.y].Active);
                    // Check if target grid tile is not active
                    if (!Tiles[(int)tileGridCoords.x, (int)tileGridCoords.y].Active)
                    {
                        return false;
                    }
                    // Check if target grid tile is not available
                    if (!Tiles[(int)tileGridCoords.x, (int)tileGridCoords.y].Available)
                    {
                        return false;
                    }

                    // Save this tile's grid coords
                    itemTilesInGrid[i, j] = tileGridCoords;
                }
                else
                {
                    itemTilesInGrid[i, j] = -Vector2.one;
                }
            }
        }

        OccupyInGrid(itemTilesInGrid);
        item.TilesInGrid = itemTilesInGrid;

        return true;
    }

    public void OccupyInGrid(Vector2[,] coords)
    {
        for (int i = 0; i < coords.GetLength(0); i++)
        {
            for (int j = 0; j < coords.GetLength(1); j++)
            {
                Vector2 coord = coords[i, j];
                if (coord != -Vector2.one)
                    Tiles[(int)coord.x, (int)coord.y].Available = false;
            }
        }
    }

    public void ClearItem(Item item)
    {
        for (int i = 0; i < item.TilesInGrid.GetLength(0); i++)
        {
            for (int j = 0; j < item.TilesInGrid.GetLength(1); j++)
            {
                Vector2 coord = item.TilesInGrid[i, j];
                if (coord != -Vector2.one)
                {
                    Tiles[(int)coord.x, (int)coord.y].Available = true;
                    item.TilesInGrid[i, j] = -Vector2.one;
                }
            }
        }
    }


    public int BaseHeight
    {
        get { return _baseHeight; }
        set { _baseHeight = value; }
    }

    public int BaseWidth
    {
        get { return _baseWidth; }
        set { _baseWidth = value; }
    }

    public int MaximumHeight
    {
        get { return _maximumHeight; }
        set { _maximumHeight = value; }
    }

    public int MaximumWidth
    {
        get { return _maximumWidth; }
        set { _maximumWidth = value; }
    }

    public float TileWidth
    {
        get { return tileWidth; }
        set { tileWidth = value; }
    }

    public float TileHeight
    {
        get { return tileHeight; }
        set { tileHeight = value; }
    }

    public BagGridTile[,] Tiles
    {
        get { return _tiles; }
        set { _tiles = value; }
    }
}
