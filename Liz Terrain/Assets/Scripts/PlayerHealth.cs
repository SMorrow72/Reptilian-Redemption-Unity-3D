using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip hurtClip;
	public AudioClip deathClip;
	public AudioClip winClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	//GameObject terrain;
	AudioSource terrainAudio;
	AudioSource playerAudio;

	RigidbodyFirstPersonController playerController;
	PlayerShooting playerShooting;
	bool isDead;
	bool damaged;

	GameObject deathGUI;
	GameObject retryBtn;
	GameObject quitBtn;
	GameObject winGUI;
	GUITexture deathTexture;
	GUITexture retryTexture;
	GUITexture quitTexture;
	GUITexture winTexture;

	// Use this for initialization
	void Start () {
		playerAudio = GetComponent<AudioSource> ();
		playerController = GetComponent<RigidbodyFirstPersonController> ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();

		/*TerrainData terrainData = (TerrainData)Resources.Load ("Terrain/TerrainDataName");
		terrain = Terrain.CreateTerrainGameObject (terrainData);
		terrainAudio = terrain.GetComponent<AudioSource> ();*/

		deathGUI = GameObject.Find ("deathGUI");
		deathTexture = deathGUI.GetComponent<GUITexture> ();
		retryBtn = GameObject.Find ("restartBtn");
		retryTexture = retryBtn.GetComponent<GUITexture> ();
		quitBtn = GameObject.Find ("quitBtn");
		quitTexture = quitBtn.GetComponent<GUITexture> ();
		winGUI = GameObject.Find ("winGUI");
		winTexture = winGUI.GetComponent<GUITexture> ();

		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColor;
			playerAudio.clip = hurtClip;
			playerAudio.Play ();
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}


	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}

	/*void OnControllerColliderHit(ControllerColliderHit col){
		if (col.gameObject.tag == "Finish") {
			Win ();
		}
	}
*/

	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;

		// Turn off any remaining shooting effects.
		playerShooting.DisableEffects ();

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		playerAudio.clip = deathClip;
		playerAudio.Play ();

		// Turn off the movement and shooting scripts.
		playerController.mouseLook.SetCursorLock(false);
		playerController.enabled = false;
		playerShooting.enabled = false;

		deathTexture.enabled = true;
		retryTexture.enabled = true;
		quitTexture.enabled = true;
		Cursor.visible = true;

	}       

	void Win(){
		playerController.mouseLook.SetCursorLock(false);
		playerController.enabled = false;
		playerShooting.enabled = false;

		/*
		terrainAudio.clip = winClip;
		terrainAudio.Play ();
		*/

		winTexture.enabled = true;
		retryTexture.enabled = true;
		quitTexture.enabled = true;
		Cursor.visible = true;
	}
}
