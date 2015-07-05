using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Image image;
    protected bool isInArea;
    [SerializeField]
    private Vector2 _centerPoint;

    private RectTransform _rectTrans;
    private float _width;
    private float _height;

    protected virtual void Awake()
    {
        RectTrans = GetComponent<RectTransform>();
        Width = RectTrans.rect.width;
        Height = RectTrans.rect.height;       
    }

    public abstract void OnDrop(PointerEventData eventData);

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isInArea = true;
        BagPrepController.Instance.IsInDropArea = true;
        if (BagPrepController.Instance.DraggedItem)
        {
            Color target = image.color / 2;
            image.CrossFadeColor(target, 0.2f, true, true);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isInArea = false;
        BagPrepController.Instance.IsInDropArea = false;
        if (BagPrepController.Instance.DraggedItem)
        {
            DeselectArea();
        }
    }

    protected virtual void DeselectArea()
    {
        Color target = image.color * 2;
        image.CrossFadeColor(target, 0.2f, true, true);
    }

    public void CalculateCenterPoint()
    {
        Vector2 tilePosition = GetComponent<RectTransform>().anchoredPosition;
        CenterPoint = tilePosition + (new Vector2(Width*0.5f, -Height*0.5f));
    }

    public RectTransform RectTrans
    {
        get { return _rectTrans; }
        set { _rectTrans = value; }
    }

    public float Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public float Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public Vector2 CenterPoint
    {
        get { return _centerPoint; }
        set { _centerPoint = value; }
    }
}
