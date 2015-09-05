﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryGameController : MonoBehaviour {

	public StoryDragHandler storyDragHandler;
	private ImageCarrousel storyElementGroup;

	public GameObject ItemsParent;
	public CanvasScaler canvasScaler;

	// Use this for initialization
	void Start () {
		// Obtain scenario story group
		GameObject ScenarioStory = Instantiate(GameConfiguration.Instance.Level.CurrentScenario.ScenarioStory);
        storyElementGroup = ScenarioStory.GetComponent<ImageCarrousel>();

        // Initialize story element group
        storyElementGroup.Initialize(ItemsParent, canvasScaler);

        // Initialize drag handler
        storyDragHandler.Initialize();
	}
	
	public void LoadGame()
	{
		//AudioManager.Instance.Fade(AudioManager.AudioType.BgMusic, 1.0f, 0.5f);
		AudioManager.Instance.Play(AudioManager.AudioType.BgMusic, GameConfiguration.Instance.Level.BgMusic, 0.5f);
		Application.LoadLevel("Game");
	}
}