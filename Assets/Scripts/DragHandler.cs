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
        if (!BagPrepController.Instance.IsInDropArea) // if not valid drop area
        {
            AnimateBackToStartPosition();
        }
        else // if is in valid drop area
        {
            // handled by drop event            
        }
    }

    public void AnimateBackToStartPosition()
    {
        StartCoroutine(AnimateBackToStartPositionRoutine(0.15f));
    }

    private IEnumerator AnimateBackToStartPositionRoutine(float duration)
    {
        transform.DOMove(startPosition, duration, false);
        //transform.position = startPosition;
        //this.transform.GetChild(0).GetComponent<RectTransform>().
        
        yield return new WaitForSeconds(duration);

        if (BagPrepController.Instance.DraggedItem.InBag) // If item is in bag already
        {
            BagPrepController.Instance.DraggedItem = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true; 
        }
        else // Item is not in bag
        {
            BagPrepController.Instance.DraggedItem = null;
            BagPrepController.Instance.SpawnNewSelectedItem(); // Spawn new one
            Destroy(gameObject); // Destroy this object
        }
        
    }
}
