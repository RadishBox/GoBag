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

	void Start()
	{
		Initialize();
	}

	public void Initialize()
	{
		foreach (CarrouselItem item in items)
		{
			GameObject carrouselItem = Instantiate(ItemPrefab);
			carrouselItem.transform.SetParent(ItemsParent.transform, false);
			carrouselItem.transform.SetAsLastSibling();

			carrouselItem.GetComponent<Image>().sprite = item.sprite;
			carrouselItem.GetComponentInChildren<Text>().text = item.text;

			// Adjust preferred width to screen size to scaler reference resolution
			carrouselItem.GetComponent<LayoutElement>().preferredWidth = canvasScaler.referenceResolution.x;

		}
	}

	

}
