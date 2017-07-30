using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent<NavMeshAgent> ();
	}


	
	// Update is called once per frame
	void Update () {
		//if the player and enemy have health left
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			//set the nav mesh agent to follow the player
			nav.SetDestination (player.position);
		} else {
			nav.enabled = false;
		}
	}
}
