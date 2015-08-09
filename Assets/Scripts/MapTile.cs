using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

    /*
    public bool[] GetCardinalBlockage
    {
        return true;
    }
     * */
    private Vector2 _canvasDisplacement;
    private Vector2 posInCanvasGrid;

    void Awake()
    {
        
    }

    void Start()
    {
        //FormatToGameTile();
    }

    public void FormatToGameTile()
    {
        posInCanvasGrid = GetComponent<RectTransform>().anchoredPosition;
        Vector2 targetPosition = posInCanvasGrid;

        GameObject Tile = new GameObject();

        foreach (Transform child in transform)
        {
            GameObject childGO = new GameObject();
            childGO.transform.SetParent(Tile.transform, true); // Add child to tile
            childGO.transform.localScale = Vector3.one;

            // Copy components
            childGO.AddComponent<MapTileUnit>(child.GetComponent<MapTileUnit>());
            SpriteRenderer originalSpriteRenderer = child.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = childGO.AddComponent<SpriteRenderer>(originalSpriteRenderer);
            spriteRenderer.sortingLayerID = originalSpriteRenderer.sortingLayerID;
            spriteRenderer.sortingLayerName = originalSpriteRenderer.sortingLayerName;
            spriteRenderer.sortingOrder = originalSpriteRenderer.sortingOrder;

            childGO.transform.localRotation = child.transform.localRotation;

            childGO.name = child.gameObject.name;
            //childGO.GetComponent<MapTileUnit>().FormatToGameTile();
        }

        // Detatch from canvas
        Tile.transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform.FindChild("Tiles"),true);
        Tile.transform.localScale = Vector3.one;

        // Set position
        targetPosition = new Vector2((targetPosition.x/45)-1, (targetPosition.y/45)-1);
        Tile.transform.localPosition = targetPosition;

        // Apply displacement
        string[] pos = Name.Split(',');
        int xPos = int.Parse(pos[0]);
        int yPos = int.Parse(pos[1]);
        Tile.transform.localPosition = new Vector2(xPos, yPos);
        //Tile.transform.localPosition = new Vector2(targetPosition.x - 3.5f, targetPosition.y + 28.92222f + 0.5f);

        // Add Tile Component
        Tile.gameObject.AddComponent<MapTile>(this);

        // Set GameObject Name
        Tile.gameObject.name = Name;
    }

    public Vector2 CanvasDisplacement
    {
        get { return _canvasDisplacement; }
        set { _canvasDisplacement = value; }
    }

    public string Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value; }
    }

    public bool Passable
    {
        get
        {
            foreach (Transform child in transform)
            {
                MapTileUnit tileUnit = child.GetComponent<MapTileUnit>();
                if (!tileUnit.Passable)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
