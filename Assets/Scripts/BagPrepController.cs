using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BagPrepController : MonoBehaviour {

    public GameObject SelectedItemGO;

	// Use this for initialization
	void Start () {
	
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
            item.GetComponent<Toggle>().isOn = false;
            //Destroy(item.GetComponent<Toggle>());
            //Destroy(item.GetComponent<LayoutElement>());
            item.transform.SetParent(SelectedItemGO.transform);
            item.transform.localScale = Vector3.one;
        }
    }
}
