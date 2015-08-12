using UnityEngine;
using System.Collections;

/// <summary>Contains all different map entities (incidents and events)</summary>
/// <para>Attached to:</para>
/// <list type="table">
/// <listheader>
/// <term>Scene</term>
/// <description>GameObject</description>
/// </listheader>
/// <item><term>Scene</term><description>GameObject</description></item>
/// </list>
public class MapEntityLibrary : MonoBehaviour {

	public GameObject[] Entities;

	private static MapEntityLibrary _instance;

    public static MapEntityLibrary Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    void Awake()
    {
        Instance = this;
    }

}
