using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteDropArea : DropArea {

    public override void OnDrop(PointerEventData eventData)
    {
        BagPrepController.Instance.DragAreaGroupSlider.Slide(false);
        if (BagPrepController.Instance.DraggedItem)
        {
            print("its ondrop");
            // TO DO: Delete item
            DeselectArea();
            BagPrepController.Instance.SpawnNewSelectedItem();
            // Free space in grid
            Item draggedItem = BagPrepController.Instance.DraggedItem;

            if (draggedItem.InBag)
            {
                BagPrepController.Instance.BagGrid.ClearItem(draggedItem);
            }
            Destroy(draggedItem.gameObject);
        }
    }
}
