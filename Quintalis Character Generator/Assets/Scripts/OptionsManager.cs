using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour {
	GameObject optionsCanvas;
	// Use this for initialization
	void Start () {
		optionsCanvas = this.gameObject;
	}
	
	public void ExitOptions(){
		optionsCanvas.SetActive(false);
	}
}
