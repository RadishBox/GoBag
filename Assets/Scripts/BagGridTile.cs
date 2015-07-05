using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagGridTile : DropArea
{
    private bool _isAvailable = true;
    private bool _isUnavailable = false;

    public override void OnDrop(PointerEventData eventData)
    {
        if (isInArea)
        {
            print("Its ondrop tile");
            BagPrepController.Instance.DraggedItem.InBag = true;
            // Snap to grid
            print(CenterPoint);
            BagPrepController.Instance.DraggedItem.RectTrans.anchoredPosition = CenterPoint;

            BagPrepController.Instance.DraggedItem.transform.SetParent(BagPrepController.Instance.GridItemsParentGO.transform, false);

            if(!BagPrepController.Instance.HasSelectedItem)
            {
                // Spawn new selected item
                BagPrepController.Instance.SpawnNewSelectedItem();
            }
            
        }        
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!Unavailable)
        {
            isInArea = true;
            BagPrepController.Instance.IsInDropArea = true;
            if (BagPrepController.Instance.DraggedItem && !Unavailable)
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

    void Update()
    {
        if ((BagPrepController.Instance.DraggedItem != null) && (isInArea))
        {
            print(GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition));
        }
    }

    public bool Available
    {
        get { return _isAvailable; }
        set { _isAvailable = value; }
    }

    public bool Unavailable
    {
        get { return _isUnavailable; }
        set { _isUnavailable = value; }
    }
}

