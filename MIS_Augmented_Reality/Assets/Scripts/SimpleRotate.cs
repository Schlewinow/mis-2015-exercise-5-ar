using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour
{
	// x-rotation in degrees per second
	public float x;

	// y-rotation in degrees per second
	public float y;

	// z-rotation in degrees per second
	public float z;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	}
}
