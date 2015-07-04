using UnityEngine;
using System.Collections;

public class WebContentImage : MonoBehaviour
{
	public string ImageAdress = "http://www.uni-weimar.de/typo3temp/pics/4ddd2378e0.jpg";

	public float Width = 1.0f;

	public float PosX = 0.0f;

	public float PosY = 0.0f;

	public float PosZ = 0.0f;

	// Used to allow real-time update in unity-editor while running.
	private GameObject mVisualization = null;

	// Use this for initialization
	void Start ()
	{
		// Make sure the script object is arranged with it's parent.
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		this.transform.localScale = Vector3.one;

		StartCoroutine(this.LoadImage());
	}

	public void Update()
	{
		if(this.mVisualization != null)
		{
			float ratio = this.mVisualization.transform.localScale.z / this.mVisualization.transform.localScale.x;
			this.mVisualization.transform.localPosition = new Vector3(this.PosX, this.PosY, this.PosZ);
			this.mVisualization.transform.localScale = new Vector3(this.Width, 1.0f, this.Width * ratio);
		}
	}

	// Necessary because of Vuforia behavior.
	public void OnEnable()
	{
		foreach(Transform t in this.transform)
		{
			t.gameObject.SetActive(true);
		}
	}

	// Necessary because of Vuforia behavior.
	public void OnDisable()
	{
		foreach(Transform t in this.transform)
		{
			t.gameObject.SetActive(false);
		}
	}

	private IEnumerator LoadImage()
	{
		// Load image data.
		WWW imageData = new WWW(this.ImageAdress);
		yield return imageData;

		float imageRatio = (float)imageData.texture.height / (float)imageData.texture.width;

		// Create plane to visualize image.
		GameObject visualization = GameObject.CreatePrimitive(PrimitiveType.Plane);
		visualization.transform.parent = this.transform;

		visualization.transform.localPosition = new Vector3(this.PosX, this.PosY, this.PosZ);
		visualization.transform.localRotation = Quaternion.identity;
		visualization.transform.Rotate(0.0f, 180.0f, 0.0f);
		visualization.transform.localScale = new Vector3(this.Width, 1.0f, this.Width * imageRatio);

		// Set texture of plane to loaded image.
		visualization.GetComponent<Renderer>().material.mainTexture = imageData.texture;
		this.mVisualization = visualization;
	}
}
