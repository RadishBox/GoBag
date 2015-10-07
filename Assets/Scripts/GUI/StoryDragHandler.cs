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

	public AudioClip PrevNextFX;

	public Button PrevButton;
	public Button NextButton;
	public Button SkipButton;

	public StoryGameController storyGameController;


	private int childIterator = 0;

	void Start()
	{
		PrevButton.interactable = false;
		//SkipButton.interactable = false;
	}

	public void Initialize()
	{
		UpdateButtonInteraction();
	}

	//Do this when the user starts dragging the element this script is attached to..
	public void OnBeginDrag (PointerEventData data)
	{
		Debug.Log("They started dragging " + this.name);
	}

	//Do this while the user is dragging this UI Element.
	public void OnDrag (PointerEventData data)
	{
		Vector2 inputDelta = data.delta;

		// Check input has been big enough
		if (Mathf.Abs(inputDelta.x) > 2)
		{
			if (inputDelta.x < 0)
			{
				// swipe to left, move to right
				currentSwipeDirection = Vector2.right;
			}
			else if (inputDelta.x > 0)
			{
				currentSwipeDirection = Vector2.left;
			}
			else
			{
				currentSwipeDirection = Vector2.zero;
			}
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
		else if (currentSwipeDirection.x < 0)
		{
			Previous();
		}
	}

	public void Next()
	{
		if (NextButton.interactable)
		{
			// Check if next is last one
			if (childIterator == ItemsParent.transform.childCount - 1)
			{
				storyGameController.LoadGame();
			}
			else
			{
				float currentPos = childIterator * canvasScaler.referenceResolution.x;
				Vector2 targetPos = new Vector2(-currentPos - canvasScaler.referenceResolution.x, 0);

				// Animate
				ItemsParent.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.5f, false).SetId("NextTween");

				childIterator++;

				UpdateButtonInteraction();



				// Check if next is movie if it is, play it
				if (storyGameController.storyElementGroup.items[childIterator].isVideo)
				{
					//PlayMovieRoutine();
				}
			}
		}
	}

	private void PlayMovieRoutine()
	{
		storyGameController.storyElementGroup.PlayVideoFromItem(childIterator);
	}

	public void Previous()
	{
		if (PrevButton.interactable)
		{
			float currentPos = childIterator * canvasScaler.referenceResolution.x;
			Vector2 targetPos = new Vector2(-currentPos + canvasScaler.referenceResolution.x, 0);

			// Animate
			ItemsParent.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.5f, false).SetId("PrevTween");

			childIterator--;

			UpdateButtonInteraction();
		}
	}

	public void UpdateButtonInteraction()
	{
		// CheckNext
		//NextButton.interactable = childIterator + 1 < ItemsParent.transform.childCount;

		// CheckPrevious
		PrevButton.interactable = (childIterator > 0);

		AudioManager.Instance.Play(AudioManager.AudioType.FX, PrevNextFX);
	}
}
