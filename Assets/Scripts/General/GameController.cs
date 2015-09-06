using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	private static GameController _instance;

	private bool _paused = false;

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
