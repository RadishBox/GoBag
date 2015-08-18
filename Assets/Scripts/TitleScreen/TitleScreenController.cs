using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Title screen controller, plus screen selection
/// </summary>
public class TitleScreenController : MonoBehaviour
{

	public GameObject StageSelectionPanel;

	public GameObject GameLibraries;

	void Awake()
	{
		StageSelectionPanel.SetActive(false);
		//DontDestroyOnLoad(GameLibraries);	
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void TriggerStageSelection(bool value)
	{
		StageSelectionPanel.SetActive(value);
	}

	public void LoadLevel(int levelId)
	{
		Level selectedLevel = LevelLibrary.Instance.GetLevel(levelId);
		GameConfiguration.Instance.Level = selectedLevel;

		Application.LoadLevel("Explore");
	}
}
