using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[System.Serializable]
public abstract class Item : MonoBehaviour {
    protected int _id;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected Sprite _itemSprite;
    [SerializeField]
    protected string _description;
    
    [SerializeField]
    protected string[] _stringTiles;
    protected int[,] _tiles;
    protected Vector2[,] _tilesInGrid;
    [SerializeField]
    private Vector2 _dragPivotCoords;
    private Vector2 _dragPivot;

    protected RectTransform _rectTrans;


    protected bool _isInBag = false;

    private float nativeSizeFactor = 1.2f;

    protected virtual void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        BuildTiles();
        ChangePivot();
    }

    protected virtual void Start()
    {
        //PrintTiles();
    }

    private void ChangePivot()
    {
        // Transform pivot coords to pivot values
        float width = (float)Tiles.GetLength(1);
        float height = (float)Tiles.GetLength(0);
        float pivotX = (1 / width) * DragPivotCoords.x + (1/width)*0.5f;
        float pivotY = 1 - ( (1 / height) * DragPivotCoords.y + (1/height)*0.5f );
        DragPivot = new Vector2(pivotX, pivotY);

        RectTrans.pivot = DragPivot;
    }

    protected void PrintTiles()
    {
        string result = gameObject.name+" tiles \n";
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                result += Tiles[i, j] + " ";
            }
            result += "\n";
        }
        //print(result);
    }

    private void BuildTiles()
    {
        Tiles = new int[StringTiles.Length,StringTiles[0].Length];
        TilesInGrid = new Vector2[StringTiles.Length, StringTiles[0].Length];
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            // Get Row
            string row = StringTiles[i];
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j] = (int)Char.GetNumericValue(row[j]);
            }
        }        
    }

    public abstract void Use(MapEntity entity);

    public void SetSize(bool isRealSize)
    {
        RectTransform imageRect = this.transform.GetChild(0).GetComponent<RectTransform>();
        if(!isRealSize)
        {           
            // Adjust parent to stretch
            /*
            RectTrans.offsetMin = Vector2.zero;
            RectTrans.offsetMax = Vector2.one;
            RectTrans.anchorMin = Vector2.zero;
            RectTrans.anchorMax = Vector2.one;
             * /
            // Stretch image rect
            /*
            imageRect.offsetMin = Vector2.zero;
            imageRect.offsetMax = Vector2.zero;
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
             * */
        }
        else
        {
            // Set image native size and center it
            this.GetComponentInChildren<Image>().SetNativeSize();
            imageRect.sizeDelta = imageRect.sizeDelta*nativeSizeFactor;
            imageRect.anchorMin = new Vector2(0.5f, 0.5f);
            imageRect.anchorMax = new Vector2(0.5f, 0.5f);

            // Adjust parent to child
            RectTrans.sizeDelta = new Vector2(imageRect.rect.width,imageRect.rect.height);
        }
        
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Sprite ItemSprite
    {
        get { return _itemSprite; }
        set { _itemSprite = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public bool InBag
    {
        get { return _isInBag; }
        set { _isInBag = value; }
    }

    public Vector2 DragPivot
    {
        get { return _dragPivot; }
        set { _dragPivot = value; }
    }

    public Vector2 DragPivotCoords
    {
        get { return _dragPivotCoords; }
        set { _dragPivotCoords = value; }
    }

    public string[] StringTiles
    {
        get { return _stringTiles; }
        set { _stringTiles = value; }
    }

    public int[,] Tiles
    {
        get { return _tiles; }
        set { _tiles = value; }
    }

    public Vector2[,] TilesInGrid
    {
        get { return _tilesInGrid; }
        set { _tilesInGrid = value; }
    }

    public RectTransform RectTrans
    {
        get { return _rectTrans; }
        set { _rectTrans = value; }
    }


}
