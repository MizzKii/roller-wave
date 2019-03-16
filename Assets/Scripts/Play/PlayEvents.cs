using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayEvents : MonoBehaviour {

	[SerializeField]private GameObject uiClick;
	[SerializeField]private GameObject uiPlay;
	[SerializeField]private GameObject uiEnd;
	[SerializeField]private GameObject[] prefabs;
	private RandomBox randomBox;

	// Use this for initialization
	void Start () {
		randomBox = GetComponent<RandomBox> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onSelect2 () {
		randomBox.chooseBox (prefabs[0]);
	}

	public void onSelect4 () {
		randomBox.chooseBox (prefabs[1]);
	}

	public void onSelect6 () {
		randomBox.chooseBox (prefabs[2]);
	}

	public void endGame () {
		uiPlay.SetActive (false);
		uiEnd.SetActive (true);
	}

	public void onReGame () {
		SceneManager.LoadScene ("Play");
	}

	public void onClickReplay () {
		uiClick.SetActive (true);
	}
}
