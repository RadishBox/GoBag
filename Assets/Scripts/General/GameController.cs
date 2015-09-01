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
