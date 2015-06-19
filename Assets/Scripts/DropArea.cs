using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Image image;

    public abstract void OnDrop(PointerEventData eventData);

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (BagPrepController.Instance.SelectedItem)
        {
            Color target = image.color / 2;
            image.CrossFadeColor(target, 0.2f, true, true);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (BagPrepController.Instance.SelectedItem)
        {
            DeselectArea();
        }
    }

    protected void DeselectArea()
    {
        Color target = image.color * 2;
        image.CrossFadeColor(target, 0.2f, true, true);
    }
}
