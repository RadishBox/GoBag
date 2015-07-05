using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteDropArea : DropArea {

    public override void OnDrop(PointerEventData eventData)
    {
        if (BagPrepController.Instance.DraggedItem)
        {
            print("its ondrop");
            // TO DO: Delete item
            DeselectArea();
            BagPrepController.Instance.SpawnNewSelectedItem();
            Destroy(BagPrepController.Instance.DraggedItem.gameObject);
        }
    }
}
