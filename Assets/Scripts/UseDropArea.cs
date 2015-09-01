using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseDropArea : DropArea {

    public override void OnDrop(PointerEventData eventData)
    {
        if (BagPrepController.Instance.DraggedItem)
        {
            print("its ondrop");
            // TO DO: Delete item
            DeselectArea();
            // Free space in grid
            Item draggedItem = BagPrepController.Instance.DraggedItem;
            draggedItem.Use(PlayerEntity.Instance);

            if (draggedItem.InBag)
            {
                BagPrepController.Instance.BagGrid.ClearItem(draggedItem);
            }            
        }
    }
}
