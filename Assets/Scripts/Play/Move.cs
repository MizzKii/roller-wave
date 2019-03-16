using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	[SerializeField]private PlayEvents playEvents;
	[SerializeField]private float velocity = 2f;
	[SerializeField]private GameObject fx;
	private Rigidbody2D rigid;
	private int frame = 0;
	private Vector3 oldPos;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (++frame > 5) {
			Vector3 moveDirection = transform.position - oldPos; 
			if (moveDirection != Vector3.zero) {
				float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}
			frame = 0;
			oldPos = transform.position;
		}
		rigid.velocity = Vector2.right * velocity;
		rigid.AddForce (Vector2.down * 40f);
	}

	public void addSpeed () {
		velocity += 0.3f;
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject.Instantiate (fx, transform.position, fx.transform.rotation);
		playEvents.endGame ();
		Destroy (gameObject);
	}
}
