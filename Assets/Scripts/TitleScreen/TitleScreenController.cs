using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Title screen controller, plus screen selection
/// </summary>
public class TitleScreenController : MonoBehaviour
{

	public GameObject StageSelectionPanel;
	public RectTransform TitleGraphic;

	public AudioClip TitleBgMusic;
	public AudioClip LevelButtonFX;

	// Singletons
	public GameObject AudioManagerPrefab;
	public GameObject GameLibrariesPrefab;

	void Awake()
	{
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

		TitleGraphic.DOAnchorPos(new Vector2(0, 100), 1).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

		LockUnpassedLevels();
		StageSelectionPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			Application.Quit(); 
		}
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

	public void UnlockPassedLevels()
	{

	}

	private void LockUnpassedLevels()
	{
		GameObject[] LevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
		print(LevelButtons.Length);

		Dictionary<int, Level> Levels = LevelLibrary.Instance.GetLevelsDictionary();
		foreach (GameObject levelButton in LevelButtons) 
		{
			int nameId = int.Parse(levelButton.name);
			if(Levels.ContainsKey(nameId))
			{
				if(Levels[(nameId)].isLocked)
				{
					levelButton.GetComponent<Button>().interactable = false;
				}
			}			
		}
	}
}
