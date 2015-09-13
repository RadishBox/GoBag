using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used for tips that are given to the player when the game finished. Depending on which events they triggered they will be shown tips associated to them.
/// </summary>
[System.Serializable]
public class Tip
{

	public TipItem Cause;

	public TipItem[] Solutions;

	public string Id;

	[System.Serializable]
	public struct TipItem
	{
		public Sprite Image;
		public string Text;
	}

}
