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

	public AudioClip TitleBgMusic;
	public AudioClip LevelButtonFX;

	void Awake()
	{
		StageSelectionPanel.SetActive(false);
		DontDestroyOnLoad(GameLibraries);	
	}

	// Use this for initialization
	void Start ()
	{
		AudioManager.Instance.Play(AudioManager.AudioType.BgMusic, TitleBgMusic);
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

		Application.LoadLevel("Story");

		AudioManager.Instance.Play(AudioManager.AudioType.FX, LevelButtonFX);
	}
}
