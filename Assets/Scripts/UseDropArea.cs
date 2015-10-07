using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseDropArea : DropArea {

    private Item currentItem;

    public override void OnDrop(PointerEventData eventData)
    {
        BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
        if (BagPrepController.Instance.DraggedItem)
        {
            BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
            if(currentItem != null)
            {
                currentItem.CompleteAnimation();
            }
            print("its ondrop");
            // Item is deleted after usage has been processed and handled by the item itself
            DeselectArea();

            // Free space in grid
            currentItem = BagPrepController.Instance.DraggedItem;

            // UnblockRaycasts from this item
            currentItem.GetComponent<CanvasGroup>().blocksRaycasts = false;

            currentItem.BeginUsage(PlayerEntity.Instance);

            // Remove item from bag
            if (currentItem.InBag)
            {
                BagPrepController.Instance.BagGrid.ClearItem(currentItem);
            }  
        }
    }
}
