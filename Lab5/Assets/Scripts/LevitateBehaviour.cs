using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateBehaviour: MonoBehaviour {

    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate() {
        System.Single pingPong = Mathf.PingPong(Time.time, _period) / _period;
        System.Single amplitude = Mathf.Lerp(-_amplitude, +_amplitude, pingPong);

        Vector3 position = _rb.position;
        position.y += amplitude;

        _rb.MovePosition(position);
	}
        
    public System.Single _amplitude = 0.01F;
    public System.Single _period = 1.0F;
    
    private Rigidbody _rb;

}
