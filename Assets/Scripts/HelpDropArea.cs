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
            itemInfoController.ShowItemInfoGroup(BagPrepController.Instance.DraggedItem);
        }
    }
}
