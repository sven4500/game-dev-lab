using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour: MonoBehaviour {

    void Awake() {
        _timeout = _respawnTime;
    }

	void LateUpdate() {
        _timeout += Time.deltaTime;

        if(_timeout > _respawnTime) {

            Vector3 pos = new Vector3(
                Random.Range(-_width, _width),
                Random.Range(-_height, _height),
                _distance);

            GameObject obj = (GameObject)Instantiate(Resources.Load("Rock" + Random.Range(1, 4)), pos, Quaternion.identity);

            // Задаём произвольную скорость камню.
            RockBehaviour rock = obj.GetComponent<RockBehaviour>();
            rock._speed = Random.Range(_speedMin, _speedMax);

            _timeout = 0.0F;
        }
	}

    public System.Single _respawnTime = 1.0F;
    public System.Single _distance = 50.0F;

    public System.Single _speedMin = 1.0F;
    public System.Single _speedMax = 10.0F;

    public System.Single _width = 10.0F;
    public System.Single _height = 10.0F;

    private System.Single _timeout = 0.0F;

}
