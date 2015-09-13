using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	private static GameController _instance;

	private bool _paused = false;

	public GameObject BagGroup;
	public GameObject PauseMenu;
	public GameObject PauseButton;

	void Awake ()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		// Initialize BagPrepController
		ExploreGameController.Instance.Initialize();
		BagPrepController.Instance.Initialize();
	}

	public void LoadTitle()
    {
        StartCoroutine(LoadTitleRoutine());
    }

    private IEnumerator LoadTitleRoutine()
    {
        AudioManager.Instance.Fade(AudioManager.AudioType.BgMusic, 0, 0.5f);
        AudioManager.Instance.Fade(AudioManager.AudioType.Ambience, 0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Title"); 
    }

    public void ReloadLevel()
    {
        StartCoroutine(ReLoadLevelRoutine());
    }

    private IEnumerator ReLoadLevelRoutine()
    {
        AudioManager.Instance.Fade(AudioManager.AudioType.BgMusic, 0, 0.5f);
        AudioManager.Instance.Fade(AudioManager.AudioType.Ambience, 0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.Play(AudioManager.AudioType.BgMusic, 0.5f);
        AudioManager.Instance.Play(AudioManager.AudioType.Ambience, 0.5f);
        Application.LoadLevel("Game"); 
    }

    public void TriggerPause(bool Pause)
    {
    	if(Pause)
    	{
    		PauseMenu.SetActive(true);
    		Paused = true;
    	}
    	else
    	{
    		PauseMenu.SetActive(false);
    		Paused = false;
    	}
    }

    public void TriggerBagOpen(bool Open)
    {
    	if (!Open)
        {
            BagGroup.SetActive(false);
            PauseButton.SetActive(true);
            // Activate input
            Paused = false;
        }
        else
        {
            BagGroup.SetActive(true);
            PauseButton.SetActive(false);
            // Deactivate input
            Paused = true;
        }
    }

	public static GameController Instance
	{
		get { return _instance; }
		set { _instance = value; }
	}

	public bool Paused
	{
		get { return _paused; }
		set { _paused = value; }
	}
}
