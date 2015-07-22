using UnityEngine;
using System.Collections;

public class MapTileUnit : MonoBehaviour {
    private int _layer;
    private bool[] _cardinalFlags;

    public int Layer
    {
        get { return _layer; }
        set { _layer = value; }
    }

    public bool[] CardinalFlags
    {
        get { return _cardinalFlags; }
        set { _cardinalFlags = value; }
    }
}
