using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEvents : MonoBehaviour {

	[SerializeField]private GameObject uiClick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onPlay () {
		SceneManager.LoadScene ("play");
	}

	public void onClick () {
		uiClick.SetActive (true);
	}
}
