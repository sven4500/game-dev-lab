using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour {

    private void OnCollisionEnter2D (Collision2D other) {
        BallBehaviour ball = other.gameObject.GetComponent<BallBehaviour>();
        if (ball != null && other.contacts.Length > 0) {
            ball.Push(other.contacts[0].normal);
        }
    }

}
