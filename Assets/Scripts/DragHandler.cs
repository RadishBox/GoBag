﻿using UnityEngine;
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
        BagPrepController.Instance.DragAreaGroupSlider.Slide(true);
        BagPrepController.Instance.DraggedItem = this.GetComponent<Item>();
        // Set correct size
        BagPrepController.Instance.DraggedItem.SetSize(true);
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!BagPrepController.Instance.IsInDropArea) // if not valid drop area
        {
            AnimateBackToStartPosition(0.15f);
            print("Not on valid drop area, reset item position");
            BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
        }
        else // if is in valid drop area
        {
            // handled by drop event            
        }
        BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
    }

    public void AnimateBackToStartPosition(float time)
    {
        if(time > 0)
        {
            StartCoroutine(AnimateBackToStartPositionRoutine(time));
        }
        else
        {
            transform.DOMove(startPosition, 0, false);
            CompleteBackAnim();
        }
    }

    private IEnumerator AnimateBackToStartPositionRoutine(float duration)
    {
        transform.DOMove(startPosition, duration, false);
        //transform.position = startPosition;
        //this.transform.GetChild(0).GetComponent<RectTransform>().
        
        yield return new WaitForSeconds(duration);

        CompleteBackAnim();
        
    }

    private void CompleteBackAnim()
    {
        if(BagPrepController.Instance.DraggedItem)
        {
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
}
