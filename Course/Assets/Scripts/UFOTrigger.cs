using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTrigger: MonoBehaviour {

    void Awake() {
        _sadText.SetActive(false);
    }

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Enemy") {
            _sadText.SetActive(true);
        }
    }

    public GameObject _sadText;

}
