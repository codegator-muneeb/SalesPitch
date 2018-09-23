using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

	private AppManager appManager;

	// Use this for initialization
	void Start () {
		appManager = AppManager.instance;
		ShowMainMenu();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
      
    }

    public void ShowEndGame()
    {
       
    }

	public void ShowMainMenu(){
		appManager.gameState = GameState.Paused;
	}

	public void ShowInApp(){
		appManager.gameState = GameState.Running;
	}
}
