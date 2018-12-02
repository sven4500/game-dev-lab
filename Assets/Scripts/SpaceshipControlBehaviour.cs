using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControlBehaviour: MonoBehaviour {

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
        System.Single inpX = -1.0F * Input.GetAxis("Horizontal");
        System.Single inpZ = -1.0F * Input.GetAxis("Vertical");
        
        // Делаем вращение если было задано значение по горизонтальной оси.
        if(inpX != 0.0F) {
            Vector3 euler = _rb.rotation.eulerAngles;
            euler.z += -1.0F * Mathf.Sign(inpX) * _angularReaction * Time.deltaTime;
            _rb.MoveRotation(Quaternion.Euler(euler));
        }
        
        if(inpZ != 0.0F) {
            _speed += Mathf.Sign(inpZ) * _acceleration * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, -_maxSpeed, _maxSpeed);
        } else {
            _speed += -1.0F * Mathf.Sign(_speed) * _slowing * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, 0.0F, _maxSpeed);
        }

        if(_speed > 0.0F) {
            // Так как blender имеет немного отличные координаты, то здесь используем
            // up вместо forward.
            _rb.MovePosition(_rb.position + transform.up * _speed * Time.deltaTime);
        }
	}
    
    private Rigidbody _rb = null;
    private System.Single _speed = 0.0F;

    public System.Single _angularReaction = 10.0F;
    public System.Single _reaction = 1.0F;
    public System.Single _slowing = 1.5F;
    public System.Single _acceleration = 1.0F;
    public System.Single _maxSpeed = 10.0F;
    
}
