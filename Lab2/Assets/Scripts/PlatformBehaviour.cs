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
	
    void OnCollisionEnter2D (Collision2D collision) {
        BallBehaviour ball = collision.gameObject.GetComponent<BallBehaviour>();
        if (ball != null && collision.contacts.Length > 0) {
            Vector2 point = collision.contacts[0].point;
            Vector2 min = collision.otherCollider.bounds.min;
            Vector2 max = collision.otherCollider.bounds.max;
            Vector2 center = collision.otherCollider.bounds.center;

            System.Single fact = 0.0F;
            fact = point.x - center.x;  // СК отн. центра
            fact /= max.x - min.x;      // СК отн. центра на [-0.5, 0.5]
            fact *= 2.0F;               // СК отн. центра на [-1.0, 1.0]
            fact = fact * fact;         // квадр. зависимость расстояния

            ball.Push(collision.contacts[0].normal, fact);
        }
	}
	
	public System.Single _speed = 1.0F;

	private Rigidbody2D _rb;
	
}
