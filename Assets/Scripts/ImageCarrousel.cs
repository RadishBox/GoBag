using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// Base class for carrousel consisting of different images
/// </summary>
public class ImageCarrousel : MonoBehaviour
{

	public CarrouselItem[] items;

	public GameObject ItemPrefab;
	public GameObject ItemsParent;

	public CanvasScaler canvasScaler;

	public void Initialize(GameObject itemsParent, CanvasScaler scaler)
	{
		ItemsParent = itemsParent;
		canvasScaler = scaler;

		foreach (CarrouselItem item in items)
		{
			GameObject carrouselItem;
			// Check if item has a gameobject
			if(item.tutorialPrefab != null)
			{
				// Panel is a prefab
				carrouselItem = Instantiate(item.tutorialPrefab);
			}
			else
			{
				// Panel is an image with text
				carrouselItem = Instantiate(ItemPrefab);
			}
			carrouselItem.transform.SetParent(ItemsParent.transform, false);
			carrouselItem.transform.SetAsLastSibling();

			carrouselItem.GetComponent<Image>().sprite = item.sprite;
			carrouselItem.GetComponentInChildren<Text>().text = item.text;

			// Adjust preferred width to screen size to scaler reference resolution
			carrouselItem.GetComponent<LayoutElement>().preferredWidth = canvasScaler.referenceResolution.x;

		}
	}

	

}
