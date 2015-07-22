using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    private int _id;
    private float _bagTime;


    private Item[] _availableItems;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public float BagTime
    {
        get { return _bagTime; }
        set { _bagTime = value; }
    }

    public Item[] AvailableItems
    {
        get { return _availableItems; }
        set { _availableItems = value; }
    }
}
