using UnityEngine;
using System.Collections;

public class LoadWebsite : MonoBehaviour {
	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}	
}
