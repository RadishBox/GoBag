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
    [SerializeField]
    protected Vector2 _dragPivot;

    protected RectTransform _rectTrans;


    protected bool _isInBag = false;

    protected virtual void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        ChangePivot();
        BuildTiles();
    }

    protected virtual void Start()
    {
    }

    private void ChangePivot()
    {
        _rectTrans.pivot = DragPivot;
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
        print(result);
    }

    private void BuildTiles()
    {
        Tiles = new int[StringTiles.Length,StringTiles[0].Length];
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

    protected abstract void Use();

    public void SetSize(bool isRealSize)
    {
        RectTransform imageRect = this.transform.GetChild(0).GetComponent<RectTransform>();
        if(!isRealSize)
        {
            // Stretch image rect
            imageRect.offsetMin = Vector2.zero;
            imageRect.offsetMax = Vector2.zero;
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
        }
        else
        {
            // Set image native size and center it
            this.GetComponentInChildren<Image>().SetNativeSize();
            imageRect.anchorMin = new Vector2(0.5f, 0.5f);
            imageRect.anchorMax = new Vector2(0.5f, 0.5f);
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

    public RectTransform RectTrans
    {
        get { return _rectTrans; }
        set { _rectTrans = value; }
    }
}
