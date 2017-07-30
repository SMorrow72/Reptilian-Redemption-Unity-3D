using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	PlayerHealth playerHealth;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			//you win!
			playerHealth.SendMessage("Win");
		}
	}

}
