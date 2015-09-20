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
	public GameObject CreditsPanel;
	private bool isCreditsPanelActive = false;

	public AudioClip TitleBgMusic;
	public AudioClip LevelButtonFX;

	// Singletons
	public GameObject AudioManagerPrefab;
	public GameObject GameLibrariesPrefab;

	public bool DebugMode = false;

	// Levels
	public GameObject ClearedSpritePrefab;
	public GameObject UnlockLevelAnimation;

	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
	{
		ManageSavedGames();
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

		if (!DebugMode)
		{
			LockUnpassedLevels();
		}
		MarkClearedLevels();
		MarkUnlockedLevels();
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

	private void LockUnpassedLevels()
	{
		GameObject[] LevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
		print(LevelButtons.Length);

		Dictionary<int, Level> Levels = LevelLibrary.Instance.GetLevelsDictionary();
		foreach (GameObject levelButton in LevelButtons)
		{
			int nameId = int.Parse(levelButton.name);
			if (Levels.ContainsKey(nameId))
			{
				if (Levels[(nameId)].isLocked)
				{
					levelButton.GetComponent<Button>().interactable = false;
					levelButton.GetComponent<CanvasGroup>().alpha = 0;
				}
			}
		}
	}

	private void MarkClearedLevels()
	{
		GameObject[] LevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");

		Dictionary<int, Level> Levels = LevelLibrary.Instance.GetLevelsDictionary();
		foreach (GameObject levelButton in LevelButtons)
		{
			int nameId = int.Parse(levelButton.name);
			if (Levels.ContainsKey(nameId))
			{
				if (Levels[(nameId)].IsClear)
				{
					GameObject ClearedSprite = Instantiate(ClearedSpritePrefab);
					ClearedSprite.transform.SetParent(levelButton.transform, false);
				}
			}
		}
	}

	private void MarkUnlockedLevels()
	{
		GameObject[] LevelButtons = GameObject.FindGameObjectsWithTag("LevelButton");

		Dictionary<int, Level> Levels = LevelLibrary.Instance.GetLevelsDictionary();
		foreach (GameObject levelButton in LevelButtons)
		{
			int nameId = int.Parse(levelButton.name);
			if (Levels.ContainsKey(nameId))
			{
				if (!Levels[(nameId)].IsLocked && !Levels[(nameId)].IsTutorial && !Levels[(nameId)].IsClear)
				{
					GameObject ClearedAnim = Instantiate(UnlockLevelAnimation);
					ClearedAnim.transform.SetParent(levelButton.transform, false);
				}
			}
		}
	}

	/// <summary>
	/// Manages loading a savefile if there is one or creating one if there is none
	/// </summary>
	private void ManageSavedGames()
	{
		if (SaveLoadUtil.LoadGameSave() != null)
		{
			// There is already a save file
			print("Save at: " + Application.persistentDataPath);
			LevelLibrary.Instance.LoadLibrary();
		}
		else
		{
			// It's a new save file
			GameSave.Instance = new GameSave();
			print("New save file!");
		}
	}

	public void TriggerCreditsPanel()
	{
		AudioManager.Instance.Play(AudioManager.AudioType.FX, LevelButtonFX);
		if (!isCreditsPanelActive)
		{
			isCreditsPanelActive = true;
			CreditsPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutCirc);
		}
		else
		{
			isCreditsPanelActive = false;
			CreditsPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, 0), 0.5f).SetEase(Ease.InOutCirc);
		}

	}
}
