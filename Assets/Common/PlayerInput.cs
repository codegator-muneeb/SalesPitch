using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public LayerMask targetLayerMask;
	private TestObject testObject;
	private Rect inputRect;
	// Use this for initialization
	void Start () {
		inputRect = new Rect(Screen.width /2, 0, Screen.width, Screen.height * 0.75f);

	}
	
	// Update is called once per frame
	void Update () {
		
		// if(AppManager.instance.gameState == GameState.Running){

			TouchInput();
			#if UNITY_EDITOR
			KeyboardInput();
			#endif
		// }
	}

	#if UNITY_EDITOR
	private void KeyboardInput(){
		if(Input.GetButtonDown("Fire1")){
			//Do Action
		}
	}
	#endif

	private void TouchInput(){
		if(Input.touchCount > 0){
			foreach(Touch touch in Input.touches){
				if(touch.phase != TouchPhase.Began)
					continue;
				if(!inputRect.Contains(touch.position))
					continue;
				
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				if(Physics.Raycast(ray, out hit, 100f, targetLayerMask)){
					if(hit.transform != null){
						// testObject.PrintName(hit.transform.gameObject);
					}
				}
			}
		}
	}
}
