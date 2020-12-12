using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOInputBehaviour: MonoBehaviour {

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _camb = _cam.GetComponent<CameraBehaviour>();
    }
    
	void FixedUpdate() {
	    System.Single inpX = Input.GetAxis("Horizontal");
        System.Single inpY = Input.GetAxis("Vertical");

        Vector3 pos = _rb.position;

        pos.x += inpX * _speed * Time.deltaTime;
        pos.y += inpY * _speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -_amplitude, _amplitude);
        pos.y = Mathf.Clamp(pos.y, -_amplitude, _amplitude);
        
        _rb.MovePosition(pos);
        _camb.TargetCamera(_rb.position);
	}

    public System.Single _speed = 1.0F;
    public System.Single _amplitude = 1.0F;

    public Camera _cam;

    private Rigidbody _rb;
    private CameraBehaviour _camb;

}
