using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour {

    private void OnCollisionEnter2D (Collision2D other) {
        BallBehaviour bb = other.gameObject.GetComponent<BallBehaviour>();
        if (bb != null && other.contacts.Length > 0) {
            bb.Bounce(other.contacts[0].normal);
        }
    }

}
