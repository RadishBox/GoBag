using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

    
    private int _height;
    private int _width;

    private static MapController _instance;
    public static MapController Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void GenerateMap()
    {

    }

    public MapTile GetTile(Vector2 coordinates)
    {
        MapTile tile = null;

        string childName = coordinates.x+","+coordinates.y;
        print(childName);

        Transform tileGO = transform.FindChild("Tiles").FindChild(childName);

        if (tileGO)
        {
            tile = tileGO.gameObject.GetComponent<MapTile>();
        }

        return tile;
    }

    /// <summary>Returns a random tile from the map, which is passable.</summary>
    public MapTile GetRandomTile(bool passable = true)
    {

    }

#region Properties

    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }

#endregion

}
