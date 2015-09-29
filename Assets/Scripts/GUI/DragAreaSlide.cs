using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class DragAreaSlide : MonoBehaviour {

	private Tweener sliteTween;

	private Vector2 originalPos = Vector2.up*200; 

	void Start()
	{
		GetComponent<RectTransform>().anchoredPosition = originalPos;
	}

	public void Slide(bool appear)
	{
		Vector2 targetPosition = (appear ? Vector2.zero : originalPos);
		GetComponent<RectTransform>().DOAnchorPos(targetPosition,0.5f,false);
	}	
}
