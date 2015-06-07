using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour {

    public GameObject GridTilePrefab;
    private float tileWidth;
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
        tileWidth = GridTilePrefab.transform.localScale.x;
        tileHeight = GridTilePrefab.transform.localScale.y;
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
                SpawnGridSquare(new Vector3(i,j));
            }
        }
    }

    private void SpawnGridSquare(Vector3 position)
    {
        GameObject.Instantiate(GridTilePrefab, position, Quaternion.identity);
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
}
