using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

            if (tile.Passable && !tile.Occupied && !tile.IsObjective) // Tile cant be an objective tile
            {
                validTile = true;
            }
        }

        return tile;
    }

    public List<MapTile> GetRow(int rowNumber)
    {
        List<MapTile> tiles = new List<MapTile>();

        for(int i = 0; i < Width; i++)
        {
            Vector2 coords = new Vector2(i, rowNumber);
            MapTile tile = GetTile(coords);
            tiles.Add(tile);
        }        

        return tiles;        
    }

    // Path finding algorithm

    /// <summary>Will return a set of tiles that make a path from one tile to another.</summary>
    public List<MapTile> GetPath(MapTile fromTile, MapTile toTile)
    { 
    	List<MapTile> pathTiles = new List<MapTile>();
    	List<MapTile> frontier = new List<MapTile>();
    	Dictionary<string, MapTile> CameFrom = new Dictionary<string, MapTile>();
    	Dictionary<string, float> CostSoFar = new Dictionary<string, float>();

    	fromTile.Priority = 0;
    	frontier.Add(fromTile);
    	CameFrom.Add(fromTile.name, null);
    	CostSoFar.Add(fromTile.name, 0);

    	MapTile current = null;

    	while(frontier.Count > 0)
    	{
    		frontier.Sort();
    		current = frontier[0]; // Get first element in list
    		frontier.RemoveAt(0);

    		// Check if current is goal
    		if(current.Position == toTile.Position)
    			break;

    		// Iterate through each neighbor in current
    		foreach (MapTile next in GetNeighbors(current)) 
    		{
    			float newCost = CostSoFar[current.name] + DistanceBetweenTiles(current, next);
    			if((!CostSoFar.ContainsKey(next.name)) || newCost < CostSoFar[next.name] ) 
    			{
    				CostSoFar.Add(next.name, newCost);
    				float priority = newCost + DistanceBetweenTiles(toTile, next);
    				next.Priority = priority;
    				frontier.Add(next);
    				CameFrom.Add(next.name, current);
    			}
    		}
    	}

    	// Get path taken
    	print(current.Position);

    	return pathTiles;
    }

    private float Heuristic(MapTile fromTile, MapTile toTile)
    {

    	return 0;
    }

    private float DistanceBetweenTiles(MapTile fromTile, MapTile toTile)
    {
    	return Mathf.Abs(Vector2.Distance(fromTile.Position, toTile.Position));
    }

    /// <summary>The neighbors of a tile is all tiles around it.</summary>
    private List<MapTile> GetNeighbors(MapTile fromTile)
    {
        List<MapTile> neighbors = new List<MapTile>();
        
        // Up
        MapTile tile;
        tile = GetTile(fromTile.Position + Vector2.up);
        if(tile)
            neighbors.Add(tile);

        // Down
        tile = GetTile(fromTile.Position + Vector2.down);
        if(tile)
            neighbors.Add(tile);

        // Left
        tile = GetTile(fromTile.Position + Vector2.left);
        if(tile)
            neighbors.Add(tile);

        // Right
        tile = GetTile(fromTile.Position + Vector2.right);
        if(tile)
            neighbors.Add(tile);

        return neighbors;
    }

    private float Heuristic(MapTile tile)
    {
    	return 0;
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
