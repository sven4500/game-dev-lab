using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthWallBehaviour: MonoBehaviour {

    private void OnCollisionEnter2D (Collision2D other) {
        BallBehaviour ball = other.gameObject.GetComponent<BallBehaviour>();
        if (ball != null) {
            // К нам попал шарик. Всё очень плохо. Игрок ни на что не способен.
            // Дальше нам делать нечего. Убиваемся от позора и стыда...
            Debug.Log("Are you absolutely positively sure you know how to play this game?");
            Application.Quit();
        }
    }

}
