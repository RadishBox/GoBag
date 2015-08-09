using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapTileUnit : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    private int _layer;
    [SerializeField]
    private bool _passable;

    public bool UpPass = true;
    public bool RightPass = true;
    public bool DownPass = true;
    public bool LeftPass = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Formats the tile from design format to game format
    /// </summary>
    public void FormatToGameTile()
    {
        Destroy(GetComponent<Image>());
        Destroy(GetComponent<CanvasRenderer>());
    }

    public int Layer
    {
        get { return _layer; }
        set { _layer = value; }
    }

    public bool Passable
    {
        get { return _passable; }
        set { _passable = value; }
    }
}
