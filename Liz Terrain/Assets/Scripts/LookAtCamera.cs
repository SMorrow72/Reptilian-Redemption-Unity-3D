using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	//define a GameObject to refer to the character
	public GameObject target;

	//LateUpdate is called after Update, so stuff will be rendered already
	void LateUpdate(){
		transform.LookAt(target.transform);
	}
}
