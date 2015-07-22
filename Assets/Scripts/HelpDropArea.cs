using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpDropArea : DropArea {
    public ItemInfoController itemInfoController;

    public override void OnDrop(PointerEventData eventData)
    {
        if (BagPrepController.Instance.DraggedItem)
        {
            BagPrepController.Instance.DraggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true; 
            itemInfoController.ShowItemInfoGroup(BagPrepController.Instance.DraggedItem);
        }
    }
}
