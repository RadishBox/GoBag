using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item : MonoBehaviour {
    protected int _id;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected Sprite _itemSprite;

    protected virtual void Start()
    {

    }

    protected abstract void Use();

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
}
