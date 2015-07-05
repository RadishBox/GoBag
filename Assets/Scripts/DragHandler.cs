using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }    

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        BagPrepController.Instance.DraggedItem = this.GetComponent<Item>();
        // Set correct size
        BagPrepController.Instance.DraggedItem.SetSize(true);
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!BagPrepController.Instance.IsInDropArea) // valid drop area
        {             
            // Let drop handler handle it
            transform.DOMove(startPosition, 0.5f, false);
            //transform.position = startPosition;
            //this.transform.GetChild(0).GetComponent<RectTransform>().
            if (!BagPrepController.Instance.DraggedItem.InBag)
            {
                BagPrepController.Instance.DraggedItem.SetSize(false);
            }
        }
        BagPrepController.Instance.DraggedItem = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true; 
        print("its onenddrag");
    }
}
