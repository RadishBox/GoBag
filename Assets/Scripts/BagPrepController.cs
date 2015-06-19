using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BagPrepController : MonoBehaviour {
    public static BagPrepController Instance;

    public GameObject SelectedItemGO;
    public Item SelectedItem;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DetectGrid()
    {
        print(Input.mousePosition);
    }

    public void ItemSelect(Item SelectedItem)
    {
        // Detatch selected object children
        if(SelectedItemGO.transform.childCount > 0)
            Destroy(SelectedItemGO.transform.GetChild(0).gameObject);

        if (SelectedItem.gameObject.GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(SelectedItem.gameObject);
            Destroy(item.GetComponent<Toggle>());
            Destroy(item.transform.FindChild("Selected").gameObject);
            //Destroy(item.GetComponent<LayoutElement>());
            item.transform.SetParent(SelectedItemGO.transform);
            item.transform.localScale = Vector3.one;
            item.AddComponent<DragHandler>();
        }
    }

    public void SpawnNewSelectedItem()
    {
        GameObject item = Instantiate(SelectedItem.gameObject);
        item.transform.SetParent(SelectedItemGO.transform, false);
        item.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
