using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour {

    public GameObject GridTilePrefab;

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private void GenerateBaseGrid()
    {
        for (int i = 0; i < BaseWidth; i++)
        {
            for (int j = 0; j < BaseHeight; j++)
            {
                GameObject tile = SpawnGridSquare(new Vector3(i*TileWidth,j*TileHeight));
                tile.transform.SetParent(this.transform);
                tile.transform.localScale = Vector3.one;
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * TileWidth, j * TileHeight);
            }
        }
    }

    private GameObject SpawnGridSquare(Vector3 position)
    {
        GameObject tile = GameObject.Instantiate(GridTilePrefab, position, Quaternion.identity) as GameObject;
        return tile;
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
