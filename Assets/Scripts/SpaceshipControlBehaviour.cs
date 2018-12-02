using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControlBehaviour : MonoBehaviour {

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}
	
	void Update() {
        System.Single inpX = -1.0F * Input.GetAxis("Horizontal");
        System.Single inpZ = -1.0F * Input.GetAxis("Vertical");
        
        System.Single acc = _acceleration;

        if(inpX == 0.0F && inpZ == 0)
            acc = -1.0F * _slowing;

        if(inpX != 0.0F) {
            _dir.x += Mathf.Sign(inpX) * _reaction * Time.deltaTime;
            _dir.x = Mathf.Clamp(_dir.x, -1.0F, 1.0F);
        } else if(Mathf.Abs(_dir.x) > 0.0F) {
            System.Single val = Mathf.Abs(_dir.x) - _reaction * Time.deltaTime;
            val = Mathf.Clamp(val, 0.0F, 1.0F);
            _dir.x = Mathf.Sign(_dir.x) * val;
        }

        if(inpZ != 0.0F) {
            _dir.z += Mathf.Sign(inpZ) * _reaction * Time.deltaTime;
            _dir.z = Mathf.Clamp(_dir.z, -1.0F, 1.0F);
        } else if(Mathf.Abs(_dir.z) > 0.0F) {
            System.Single val = Mathf.Abs(_dir.z) - _reaction * Time.deltaTime;
            val = Mathf.Clamp(val, 0.0F, 1.0F);
            _dir.z = Mathf.Sign(_dir.z) * val;
        }

        //_dir.Normalize();
        Debug.Log(_dir);
        
        _speed += acc * Time.deltaTime * Time.deltaTime;
        _speed = Mathf.Clamp(_speed, 0.0F, _maxSpeed);
        //Debug.Log(_speed);

        if(_speed != 0.0F) {
            _rb.MovePosition(_rb.position + _dir * _speed);
        }
	}
    
    private Rigidbody _rb = null;
    private System.Single _speed = 0.0F;
    private Vector3 _dir = new Vector3();

    public System.Single _reaction = 1.0F;
    public System.Single _slowing = 1.5F;
    public System.Single _acceleration = 1.0F;
    public System.Single _maxSpeed = 10.0F;
    
}
