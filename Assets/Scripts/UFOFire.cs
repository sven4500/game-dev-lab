using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFire: MonoBehaviour {

	void Update() {
		if((_time += Time.deltaTime) > _fireRate && Input.GetKeyDown(KeyCode.Space) == true) {
            Vector3 pos = transform.position;
            Instantiate(Resources.Load("LaserCharge"), pos, Quaternion.identity);
        }
	}

    private System.Single _time = 0.0F;
    public System.Single _fireRate = 1.0F;

}
