using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemLibrary : MonoBehaviour {

    private static ItemLibrary _instance;

    [SerializeField]
    private Item[] _items;

    public ItemLibrary Instance
    {
        get
        {
            if (!Initialized) _instance = new ItemLibrary();
            return _instance;
        }

    }

    public bool Initialized
    {
        get { return _instance == null; }
    }

    
    public Item[] Items
    {
        get { return _items; }
        set { _items = value; }
    }


}
