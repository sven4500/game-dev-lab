using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBehaviour: MonoBehaviour {

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider col) {
        _blockMovement = true;
    }

    void turnAround() {
        // Делаем вращение если было задано значение по горизонтальной оси.
        if(_blockMovement == false) {
            System.Single inpX = -1.0F * Input.GetAxis("Horizontal");
            if(inpX != 0.0F) {
                Vector3 euler = _rb.rotation.eulerAngles;
                euler.z += -1.0F * Mathf.Sign(inpX) * _angularReaction * Time.deltaTime;
                _rb.MoveRotation(Quaternion.Euler(euler));
            }
        }
    }

    void moveForward() {
        System.Single inpZ = -1.0F * Input.GetAxis("Vertical");

        if(inpZ != 0.0F) {
            _speed += -1.0F * Mathf.Sign(inpZ) * _acceleration * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, -_maxSpeed, _maxSpeed);
        } else {
            _speed += -1.0F * Mathf.Sign(_speed) * _slowing * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0.0F, _maxSpeed);
        }

        if(_speed != 0.0F && (_blockMovement == false || _speed < 0.0F)) {
            // Так как blender имеет немного отличные координаты, то здесь используем up вместо forward.
            _rb.MovePosition(_rb.position + transform.up * _speed * Time.deltaTime);
            _blockMovement = false;
        }
    }
	
	void FixedUpdate() {
        turnAround();
        moveForward();
	}
    
    private Rigidbody _rb = null;
    private System.Single _speed = 0.0F;
    private System.Boolean _blockMovement = false;

    public System.Single _angularReaction = 10.0F;
    public System.Single _slowing = 1.5F;
    public System.Single _acceleration = 1.0F;
    public System.Single _maxSpeed = 10.0F;
    
}
