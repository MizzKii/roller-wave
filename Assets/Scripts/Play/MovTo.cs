using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovTo : MonoBehaviour {

	[SerializeField]private GameObject to;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (to != null) {
			transform.position = Vector3.Slerp (transform.position, new Vector3 (to.transform.position.x, 1.75f, -10f), 0.5f);
		}
	}
}
