using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class TileStyle
{
	public string Number;
	public Color32 TileColor;
	public Color32 TextColor;
}


public class TileStyleHolder : MonoBehaviour {

	// SINGLETON
	public static TileStyleHolder Instance;

	public TileStyle[] TileStyles;

	void Awake()
	{
		Instance = this;
	}
}
