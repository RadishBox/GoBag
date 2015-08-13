using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagGridTile : DropArea
{
    private bool _isAvailable = true;
    private bool _isActive = true;


    private Vector2 _gridCoords;

    public override void OnDrop(PointerEventData eventData)
    {
        if (isInArea)
        {
            Item draggedItem = BagPrepController.Instance.DraggedItem;

            if (draggedItem.InBag)
            {
                BagPrepController.Instance.BagGrid.ClearItem(draggedItem);
            }
            // Check if it fits in space
            if(BagPrepController.Instance.BagGrid.CheckIfFits(draggedItem, this.GridCoords))
            {
                
                draggedItem.InBag = true;

                draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true; 
                // Snap to grid
                draggedItem.RectTrans.anchoredPosition = CenterPoint;

                draggedItem.transform.SetParent(BagPrepController.Instance.GridItemsParentGO.transform, false);

                // Add special collider if needed (irregular form)
                if (draggedItem.GetType() == typeof(RainBoots))
                {
                    if (draggedItem.GetComponent<Collider2DRaycastFilter>() == null)
                    {
                        draggedItem.gameObject.AddComponent<Collider2DRaycastFilter>();
                    }
                }

                if (!BagPrepController.Instance.HasSelectedItem)
                {
                    // Spawn new selected item
                    BagPrepController.Instance.SpawnNewSelectedItem();
                }
            }
            else
            {
                draggedItem.GetComponent<DragHandler>().AnimateBackToStartPosition();  
            }
            
        }        
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (Active)
        {
            isInArea = true;
            BagPrepController.Instance.IsInDropArea = true;
            if (BagPrepController.Instance.DraggedItem && !Active)
            {
                //Color target = image.color / 2;
                //image.CrossFadeColor(target, 0.5f, true, true);
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        isInArea = false;
        BagPrepController.Instance.IsInDropArea = false;
        if (BagPrepController.Instance.DraggedItem)
        {
            //if(!Unavailable)
                //DeselectArea();
        }
    }

    public bool Available
    {
        get { return _isAvailable; }
        set { _isAvailable = value; }
    }

    public bool Active
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public Vector2 GridCoords
    {
        get { return _gridCoords; }
        set { _gridCoords = value; }
    }
}

