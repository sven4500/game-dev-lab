using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit: MonoBehaviour {
    
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            // Если попали на камень, а камни у нас имеют тег "Enemy",
            // то уничтожаем этот камень и уничтожаемся сами.
            GameObject.Destroy(other.gameObject);
            GameObject.Destroy(gameObject);
            ++_score;
        }
    }

    static public System.UInt16 _score = 0;

}
