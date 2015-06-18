using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemListDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private Vector3 startPosition;
    private float timePressed = 0;

    void Update()
    {
        timePressed += Time.deltaTime;
        //print(timePressed);
    }
        
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        timePressed = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (timePressed > 1.0f)
        //{
        //    transform.position = Input.mousePosition;
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        timePressed = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Spawn Item Object

    }
}
