using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotateTowards))]
public class TestObject : MonoBehaviour {
	private RotateTowards rotateTowards;
	private ClickManager clickManager;
	private GameObject player;
	private Vector3 position;

	void update(){
		
	}
	
	public void PrintName(GameObject go){
		print(go.name);
	}
}
