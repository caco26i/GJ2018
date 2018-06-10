using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
	private static ColorManager instance;
	public enum Colors { Red, Green, Blue, Normal };
	public Material[] materials;
	public Color[] colors;

	// Use this for initialization
	void Start () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
	}

	public static Material GetMaterial(int color)
	{
		return instance.materials[color];
	}
	public static Color GetColor(int color)
	{
		return instance.colors[color];
	}
}
