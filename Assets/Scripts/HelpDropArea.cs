using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpDropArea : DropArea {
    public override void OnDrop(PointerEventData eventData)
    {
        if (BagPrepController.Instance.SelectedItem)
        {
            // TO DO: Display help message
            print(BagPrepController.Instance.SelectedItem.gameObject.name);
        }
    }
}
