using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Title screen controller, plus screen selection
/// </summary>
public class TitleScreenController : MonoBehaviour
{

	public GameObject StageSelectionPanel;

	public AudioClip TitleBgMusic;
	public AudioClip LevelButtonFX;

	// Singletons
	public GameObject AudioManagerPrefab;
	public GameObject GameLibrariesPrefab;

	void Awake()
	{
		StageSelectionPanel.SetActive(false);
	}

	// Use this for initialization
	void Start ()
	{
		if (!GameObject.FindWithTag("AudioManager"))
		{
			Instantiate(AudioManagerPrefab);
		}

		if (!GameObject.FindWithTag("GameLibraries"))
		{
			GameObject GameLibraries = Instantiate(GameLibrariesPrefab);
			DontDestroyOnLoad(GameLibraries);
		}
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

		AudioManager.Instance.Fade(AudioManager.AudioType.BgMusic, 0.0f, 0.5f);

		Application.LoadLevel("Story");

		AudioManager.Instance.Play(AudioManager.AudioType.FX, LevelButtonFX);
	}
}
