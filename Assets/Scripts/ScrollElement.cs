using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollElement : MonoBehaviour, IDragHandler
{
    public ScrollRect scrollRect;

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.verticalNormalizedPosition -= eventData.delta.y / ((float)Screen.height);
    }
}