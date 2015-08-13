using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the map in the exploration game part
/// </summary>
public class MapController : MonoBehaviour
{

    [SerializeField]
    private int _height;
    [SerializeField]
    private int _width;

    private static MapController _instance;
    public static MapController Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    // Use this for initialization
    void Awake ()
    {
        Instance = this;
    }

    public virtual void GenerateMap()
    {

    }

    /// <summary>
    /// Gets a tile from the map, given its coordinates
    /// </summary>
    public MapTile GetTile(Vector2 coordinates)
    {
        MapTile tile = null;

        string childName = coordinates.x + "," + coordinates.y;
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
        int x, y = 0;
        bool validTile = false;
        Vector2 targetCoords;
        MapTile tile = null;

        // Get a random coord
        while (!validTile)
        {
            x = Random.Range(0, Width);
            y = Random.Range(0, Height);

            targetCoords = new Vector2(x, y);

            tile = GetTile(targetCoords);

            if (tile.Passable && !tile.Occupied)
            {
                validTile = true;
            }
        }

        return tile;
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
