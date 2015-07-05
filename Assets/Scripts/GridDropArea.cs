using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GridDropArea : DropArea
{
    public override void OnDrop(PointerEventData eventData)
    {
        if (BagPrepController.Instance.SelectedItem)
        {
            print("its ondrop");
            // TO DO: Delete item
            BagPrepController.Instance.SpawnNewSelectedItem();
            Destroy(BagPrepController.Instance.SelectedItem.gameObject);
        }
    }
}
