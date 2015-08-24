using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class StoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public GameObject ItemsParent;
	public CanvasScaler canvasScaler;

	private Vector2 currentSwipeDirection = Vector2.zero;

	//Do this when the user starts dragging the element this script is attached to..
	public void OnBeginDrag (PointerEventData data)
	{
		Debug.Log("They started dragging " + this.name);
	}

	//Do this while the user is dragging this UI Element.
	public void OnDrag (PointerEventData data)
	{
		if (data.delta.x < 0)
		{
			// swipe to left, move to right
			currentSwipeDirection = Vector2.right;
		}
		else if(data.delta.x > 0)
		{
			currentSwipeDirection = Vector2.left;
		}
		else
		{
			currentSwipeDirection = Vector2.zero;
		}
	}

	//Do this when the user stops dragging this UI Element.
	public void OnEndDrag (PointerEventData data)
	{
		if (currentSwipeDirection.x > 0)
		{
			// swipe to left, move to right
			Next();
		}
		else if(currentSwipeDirection.x < 0)
		{
			Previous();
		}
	}



	public void Next()
	{
		Vector2 currentPos = ItemsParent.GetComponent<RectTransform>().anchoredPosition;
		Vector2 targetPos = new Vector2(currentPos.x - canvasScaler.referenceResolution.x, currentPos.y);

		// Animate
		ItemsParent.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.5f, false);
	}

	public void Previous()
	{
		Vector2 currentPos = ItemsParent.GetComponent<RectTransform>().anchoredPosition;
		Vector2 targetPos = new Vector2(currentPos.x + canvasScaler.referenceResolution.x, currentPos.y);

		// Animate
		ItemsParent.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.5f, false);
	}
}
