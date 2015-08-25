using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>Editor Script that provides a quick access menu to the most important scenes.</summary>
/// <para>Attached to:</para>
/// <list type="table">
/// <listheader>
/// <term>Scene</term>
/// <description>GameObject</description>
/// </listheader>
/// <item><term>Editor</term><description>GameObject</description></item>
/// </list>
public class SceneQuickAccess : Editor
{
	/// <summary>Open the Title Screen Scene.</summary>
	[MenuItem("Open Scene/Title")]
	public static void OpenTitleScreen()
	{
		OpenScene("Title");
	}

	/// <summary>Open the Story Screen Scene.</summary>
	[MenuItem("Open Scene/Story")]
	public static void OpenStoryScreen()
	{
		OpenScene("Story");
	}

	/// <summary>Open the Game Screen Scene.</summary>
	[MenuItem("Open Scene/Game")]
	public static void OpenGameScreen()
	{
		OpenScene("Game");
	}

	/// <summary>Open the MapCreationUtil Screen Scene.</summary>
	[MenuItem("Open Scene/MapCreationUtil")]
	public static void OpenMapCreationUtilScreen()
	{
		OpenScene("MapCreationUtil");
	}

	static void OpenScene(string name)
	{
		if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
		{
			EditorApplication.OpenScene("Assets/Scenes/" + name + ".unity");
		}
	}
}