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
				if(carrouselItem.GetComponentInChildren<Text>())
				{	
				/*	
					Text[] texts = carrouselItem.GetComponentsInChildren<Text>();	
					if(texts.Length > 1)
					{
						foreach (Text text in texts)
						{
							if(text.transform.parent.gameObject.name == "BigBalloon")
							{
								carrouselItem.GetComponentInChildren<Text>().text = item.text;
							}
						}
					}		
					else 
					{
						carrouselItem.GetComponentInChildren<Text>().text = item.text;
					}	*/
					if(carrouselItem.transform.Find("BigBalloon"))
						carrouselItem.transform.Find("BigBalloon").GetComponentInChildren<Text>().text = item.text;
				}
				if(item.isVideo)
				{
					// Play video
					Handheld.PlayFullScreenMovie (item.text+".mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
				}
			}
			else
			{
				// Panel is an image with text
				carrouselItem = Instantiate(ItemPrefab);
				carrouselItem.GetComponent<Image>().sprite = item.sprite;
				carrouselItem.GetComponentInChildren<Text>().text = item.text;
			}
			carrouselItem.transform.SetParent(ItemsParent.transform, false);
			carrouselItem.transform.SetAsLastSibling();

			
			if(carrouselItem.GetComponent<LayoutElement>())
			{
				// Adjust preferred width to screen size to scaler reference resolution
				carrouselItem.GetComponent<LayoutElement>().preferredWidth = canvasScaler.referenceResolution.x;
			}
			else
			{
				print(carrouselItem.gameObject.name);
			}
		}
	}

	

}
