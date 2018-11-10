using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour {

	void Awake () {
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		Vector2 input = new Vector2();
		input.x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
		_rb.MovePosition(_rb.position + input);
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		BallBehaviour bb = other.gameObject.GetComponent<BallBehaviour>();
        if (bb != null && other.contacts.Length > 0) {
            //bb.Bounce(other.contacts[0].normal);
        }
	}
	
	public System.Single _speed = 1.0F;
	private Rigidbody2D _rb;
	
}
