using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeraMove : MonoBehaviour {

	[SerializeField]private Move move;
	[SerializeField]private Vector3 grap;
	private Vector3 lastPos;

	// Use this for initialization
	void Start () {
		lastPos = transform.position;
		nextPos (new Vector3(38.5f, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, lastPos, 0.05f);
	}

	public void nextPos (Vector3 pos) {
		lastPos.x = pos.x - 14f;
		move.addSpeed ();
	}
}
