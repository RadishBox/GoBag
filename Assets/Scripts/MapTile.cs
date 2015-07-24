using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

    /*
    public bool[] GetCardinalBlockage
    {
        return true;
    }
     * */

    private Vector2 posInCanvasGrid;

    void Awake()
    {
        posInCanvasGrid = GetComponent<RectTransform>().anchoredPosition;
    }

    void Start()
    {
        FormatToGameTile();
    }

    public void FormatToGameTile()
    {
        Vector2 targetPosition = posInCanvasGrid;
        print(targetPosition);

        foreach (Transform child in transform)
        {
            child.GetComponent<MapTileUnit>().FormatToGameTile();
        }

        // Detatch from canvas
        transform.SetParent(GameObject.FindGameObjectWithTag("Map").transform,true);
        transform.localScale = Vector3.one;
        GetComponent<RectTransform>().sizeDelta = Vector2.one;

        // Set position
        targetPosition = new Vector2((targetPosition.x/45)-1, (targetPosition.y/45)-1);
        transform.localPosition = targetPosition;

        // Apply displacement
        transform.localPosition = new Vector2(targetPosition.x - 3.5f, targetPosition.y + 18f);
    }



}
