using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Set correct size
        this.GetComponentInChildren<Image>().SetNativeSize();
        this.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        this.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

        BagPrepController.Instance.SelectedItem = this.GetComponent<Item>();
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("ïts onenddrag");
        BagPrepController.Instance.SelectedItem = null;
        transform.position = Input.mousePosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;        
    }
}
