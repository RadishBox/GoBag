using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapGenerationUtil : MonoBehaviour {

    public GameObject Map;
    public GridLayoutGroup GridLayout;
    public string MapName;
    public Vector3 MapDisplacement;

    public bool Generate = false;


    private int _columnNumber;

    void Awake()
    {
        GridLayout.enabled = false;
        _columnNumber = GridLayout.constraintCount;
    }

	// Use this for initialization
	void Start () {
        GenerateMapPrefab();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void GenerateMapPrefab()
    {
        int x, y = 0;

        MapTile[] mapTiles = transform.GetComponentsInChildren<MapTile>();
        for (int i = 0; i < mapTiles.Length; i++)
        {
            x = i % _columnNumber;
            y = i / _columnNumber;            
            mapTiles[i].Name = x + "," + y;
            mapTiles[i].FormatToGameTile();
        }

        // Displace map
        Map.transform.position += MapDisplacement;

        // Set Map Width and Height
        Map.GetComponent<MapController>().Width = _columnNumber;
        Map.GetComponent<MapController>().Height = y+1;


#if UNITY_EDITOR
        if (Generate)
            PrefabUtility.CreatePrefab("Assets/Prefabs/Maps/" + MapName + ".prefab", Map, ReplacePrefabOptions.Default);
#endif
        


        GridLayout.gameObject.SetActive(false);
    }
}
