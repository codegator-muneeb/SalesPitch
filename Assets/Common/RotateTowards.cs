using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

	public Transform target;

	public bool lookAtTarget;

	void OnEnable(){
		lookAtTarget = false;
	}

	void Update(){
		if(!lookAtTarget){
			transform.LookAt(target.position);
			lookAtTarget = true;
		}
	}
}
