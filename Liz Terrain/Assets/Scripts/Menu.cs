using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent (typeof (AudioSource))]

public class Menu : MonoBehaviour {

	public string levelToLoad;
	public Texture2D normalTexture;
	public Texture2D rollOverTexture;
	public AudioClip beep;
	public bool quitButton = false;
	public bool instructionsButton = false;
	private GameObject instructions;
	private GUIText instructGUI;

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
		GetComponent<GUITexture>().texture = rollOverTexture;
	}

	void OnMouseExit(){
		GetComponent<GUITexture>().texture = normalTexture;
	}

	IEnumerator OnMouseUp(){
		GetComponent<AudioSource>().PlayOneShot (beep);
		yield return new WaitForSeconds (.35f);

		if (quitButton) {
			Application.Quit ();
		} else if (instructionsButton) {
			instructions = GameObject.Find ("instructions");
			instructGUI = instructions.GetComponent<GUIText> ();
			if (instructGUI.enabled)
				instructGUI.enabled = false;
			else
				instructGUI.enabled = true;
		} else {
			SceneManager.LoadScene (levelToLoad);
		}
	}
}
