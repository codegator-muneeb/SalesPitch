using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
	using UnityEditor;
#endif

public enum GameState{
	Paused,
	Running
}
public class AppManager : MonoBehaviour {

	public static AppManager instance;
	public GuiManager guiManager;
	public GameObject player;

	[HideInInspector]
	public GameState gameState;

	void awake(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if(AppManager.instance == null){
			AppManager.instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		Init();
	}
	
	private void Init(){
		
	}
	
	public void StartNew(){
		guiManager.Reset();
	}

	public void EndGame(){
		guiManager.ShowEndGame();
	}

	public void ExitApplication(){

		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	} 

}
