using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpDropArea : DropArea {
    public ItemInfoController itemInfoController;

    public override void OnDrop(PointerEventData eventData)
    {
        BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
        if (BagPrepController.Instance.DraggedItem)
        {
        	BagPrepController.Instance.DraggedItem.GetComponent<DragHandler>().AnimateBackToStartPosition(0.15f);
            BagPrepController.Instance.DraggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true; 
            itemInfoController.ShowItemInfoGroup(BagPrepController.Instance.DraggedItem);
        }
    }
}
