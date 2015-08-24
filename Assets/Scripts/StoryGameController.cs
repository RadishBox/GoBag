using UnityEngine;
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
	
	// Update is called once per frame
	void Update () {
	
	}
}