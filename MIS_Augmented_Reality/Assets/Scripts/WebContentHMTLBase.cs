using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class WebContentHTMLBase : MonoBehaviour
{
	public string WebPage = "";
	
	public float TextScale = 0.05f;
	
	public float PosX = 0.0f;
	
	public float PosY = 0.0f;
	
	public float PosZ = 0.0f;
	
	// Used for real-time editor update at runtime.
	protected GameObject mTextObject = null;
	
	// Use this for initialization
	void Start ()
	{
		// Make sure the script object is arranged with it's parent.
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		this.transform.localScale = Vector3.one;
		
		this.StartCoroutine(this.LoadText());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(this.mTextObject != null)
		{
			this.mTextObject.transform.localPosition = new Vector3(this.PosX, this.PosY, this.PosZ);
			this.mTextObject.transform.localScale = new Vector3(this.TextScale, this.TextScale, this.TextScale);
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
	
	private IEnumerator LoadText()
	{
		WWW pageData = new WWW(this.WebPage);
		yield return pageData;
		
		// Read HTML-code and parse it.
		string html = this.NormalizeText(pageData.text);
		string specificText = this.GetSpecificText(html);
		
		// Create text mesh to show text in 3D scene.
		this.mTextObject = this.MakeTextObject(specificText);
	}
	
	// Put text into some order for further processing.
	private string NormalizeText(string input)
	{
		string output = input.Replace("\n", string.Empty);
		output = output.Replace("\r", string.Empty);
		output = output.Replace("\t", string.Empty);
		output = output.Replace("  ", " ");
		return output;
	}
	
	// Read current functions of a person from a BISON web page.
	protected virtual string GetSpecificText(string html)
	{
		return string.Empty;
	}
	
	// Generate 3D-Text object to be placed in virtual scene.
	protected GameObject MakeTextObject(string text)
	{
		GameObject meshObject = new GameObject();
		TextMesh textMesh = meshObject.AddComponent<TextMesh>();
		textMesh.text = text;
		
		meshObject.name = "TextMesh";
		meshObject.transform.parent = this.transform;
		
		meshObject.transform.localPosition = new Vector3(this.PosX, this.PosY, this.PosZ);
		meshObject.transform.localRotation = Quaternion.identity;
		meshObject.transform.Rotate(90.0f, 0.0f, 0.0f);
		meshObject.transform.localScale = new Vector3(this.TextScale, this.TextScale, this.TextScale);

		textMesh.color = Color.white;

		return meshObject;
	}
}
