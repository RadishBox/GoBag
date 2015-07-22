using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class BagPrepController : MonoBehaviour {
    public static BagPrepController Instance;

    public GameObject GridItemsParentGO;
    public GameObject SelectedItemParentGO;
    public Item SelectedItem;

    public Item DraggedItem;

    private bool _isInDropArea = false;

    // Bag grid
    public GridController BagGrid;

    // Civil Guy Group
    public CivilGuyController civilGuyController;


	// Use this for initialization
	void Awake () {
        Instance = this;
        Input.multiTouchEnabled = false;
	}

    void Start()
    {
        civilGuyController.ShowCivilGuyGroup();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ItemSelect(Item selectedItem)
    {
        this.SelectedItem = selectedItem;
        SpawnNewSelectedItem();
    }

    public void SpawnNewSelectedItem()
    {
        //DraggedItem.SetSize(false);
        //GameObject item = Instantiate(DraggedItem.gameObject);
        //item.transform.SetParent(SelectedItemParentGO.transform, false);
        //item.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Detatch selected object children
        if (SelectedItemParentGO.transform.childCount > 0)
            Destroy(SelectedItemParentGO.transform.GetChild(0).gameObject);

        if (SelectedItem.gameObject.GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(SelectedItem.gameObject);
            Destroy(item.GetComponent<Toggle>());
            Destroy(item.transform.FindChild("Selected").gameObject);
            //Destroy(item.GetComponent<LayoutElement>());
            item.transform.SetParent(SelectedItemParentGO.transform, false);
            item.AddComponent<DragHandler>();
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    /* Getters and Setters */
    public bool IsInDropArea
    {
        get { return _isInDropArea; }
        set { _isInDropArea = value; }
    }

    public bool HasSelectedItem
    {
        get { return SelectedItemParentGO.transform.childCount > 0; }
    }
}
