using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

	private Rect lookRect;
	private int lookTouchId = -1;
	private Vector2 touchOrigin;

	public float maxRotationPerSecond = 75f;
	public float mouseRotationSpeed = 100f;
	public float gyroRotationSpeed = 70f;

	[Range (0, 45)]
	public float maxPitchUpAngle = 45f;

	[Range (0, 45)]
	public float maxPitchDownAngle = 5f;

	// Use this for initialization
	void Start () {
		lookRect = new Rect(0, 0, Screen.width / 2, Screen.height * 0.75f);

		#if UNITY_EDITOR
		Cursor.visible = false;
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		// if(AppManager.instance.gameState == GameState.Running){
			if(Input.gyro.enabled){
				GyroInput();
			} else{
				TouchInput();
			}
			
			#if UNITY_EDITOR
			MouseInput();
			#endif
		// }
	}

	private void TouchInput(){
		if(Input.touchCount > 0){
			//Save 1st touch input
			if(lookTouchId == -1){
				foreach(Touch touch in Input.touches){
					//Register only beginning of touches
					if(touch.phase != TouchPhase.Began)				
						continue;
					//Define touches inside lookRect - Rectangle of screen defined for usage
					if(!lookRect.Contains(touch.position))
						continue;
					
					lookTouchId = touch.fingerId;
					touchOrigin = touch.position;
					break;
				}
			}

			foreach(Touch touch in Input.touches){
				if(touch.fingerId != lookTouchId)
					continue;
				
				Vector3 touchDistance = touch.position - touchOrigin;
				// Limit rotation on touch
				Vector3 clampedRotation = Vector3.ClampMagnitude(new Vector3(-touchDistance.y, touchDistance.x), maxRotationPerSecond);

				//Rotated view
				RotateView(clampedRotation);

				//Clear Saved TouchId on Release
				if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
					lookTouchId = -1;
				}

				//Exit loop
				break;
			}
		}
	}

	private void GyroInput(){
		Vector3 rotation = Input.gyro.rotationRate * gyroRotationSpeed;

		RotateView (new Vector3 (-rotation.x, -rotation.y, 0));
	}

	#if UNITY_EDITOR
	private void MouseInput(){
		Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
		RotateView(rotation * mouseRotationSpeed);
	}
	#endif

	private void RotateView(Vector3 rotation)
    {
		//Make rotation independent of frameRate
		transform.Rotate(rotation * Time.deltaTime);

		if (Input.gyro.enabled) {
			// Reset local z rotation
			Vector3 localEuler = transform.localEulerAngles;
			transform.localRotation = Quaternion.Euler (localEuler.x, localEuler.y, 0);
		}

		//Limit the rotation
		float playerPitch = LimitPitch();

		//Update and clean rollover
		transform.rotation = Quaternion.Euler(playerPitch, transform.eulerAngles.y, 0);

	}

	private float LimitPitch(){
		float playerPitch = transform.eulerAngles.x;
		float maxPitchUp = 360 - maxPitchUpAngle;
		float maxPitchDown = maxPitchDownAngle;

		if(playerPitch > 180 && playerPitch < maxPitchUp){
			playerPitch = maxPitchUp;
		} else if(playerPitch < 180 && playerPitch > maxPitchDown){
			playerPitch = maxPitchDown;
		}

		return playerPitch;
	}
}
