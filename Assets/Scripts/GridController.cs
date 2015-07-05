using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridController : MonoBehaviour {

    public GameObject GridTilePrefab;
    public string[] tilesString;
    public GameObject[,] tilesGO;

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
	void Start () {
        GenerateBaseGrid();
        GenerateGrid();
	}

    private void GenerateGrid()
    {
        for (int i = 0; i < tilesGO.GetLength(1); i++)
        {
            // Get Row
            string row = tilesString[i];
            for (int j = 0; j < tilesGO.GetLength(0); j++)
            {
                if(row[j]=='0')
                {
                    tilesGO[j, i].GetComponent<Image>().CrossFadeAlpha(0, 0, true);
                    tilesGO[j, i].GetComponent<BagGridTile>().Unavailable = true;
                }
                tilesGO[j, i].GetComponent<BagGridTile>().CalculateCenterPoint();
            }
        }
    }

    private void GenerateBaseGrid()
    {
        tilesGO = new GameObject[BaseWidth, BaseHeight];
        for (int i = 0; i < BaseHeight; i++)
        {
            for (int j = 0; j < BaseWidth; j++)
            {
                GameObject tile = GameObject.Instantiate(GridTilePrefab);
                tile.transform.SetParent(this.transform);
                tile.transform.localScale = Vector3.one;
                tile.GetComponent<RectTransform>().sizeDelta = new Vector2(TileWidth, TileHeight);
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector3(j * TileWidth, -i * TileHeight);
                tilesGO[j, i] = tile;
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
}
