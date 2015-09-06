using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class StoryLoadGame : MonoBehaviour {

	public Image Bag;
	public RectTransform BagRect;

	// Use this for initialization
	void Start () {
		BagRect.DOShakeAnchorPos(1.5f, Vector3.one*50, 10, 50, false).SetLoops(-1);
	}

	public void LoadGame()
	{
		AudioManager.Instance.Play(AudioManager.AudioType.BgMusic, GameConfiguration.Instance.Level.BgMusic, 0.5f);
		Application.LoadLevel("Game");
	}
}
