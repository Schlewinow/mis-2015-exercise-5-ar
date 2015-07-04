using UnityEngine;
using System.Collections;

public class SimpleMoverUI : MonoBehaviour {

	private bool mRotateL = false;

	private bool mRotateR = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(this.mRotateL)
		{
			this.transform.Rotate(0.0f, 1.0f, 0.0f);
		}

		if(this.mRotateR)
		{
			this.transform.Rotate(0.0f, -1.0f, 0.0f);
		}
	}

	public void OnGUI()
	{
		if(GUI.Button(new Rect(0.0f, 0.0f, Screen.width * 0.1f, Screen.height), "L"))
		{
			this.mRotateL = !mRotateL;
			this.mRotateR = false;
		}

		if(GUI.Button(new Rect(Screen.width * 0.9f, 0.0f, Screen.width * 0.1f, Screen.height), "R"))
		{
			this.mRotateR = !mRotateR;
			this.mRotateL = false;
		}
	}
}
