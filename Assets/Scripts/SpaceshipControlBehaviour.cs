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
        
        Vector3 euler = _rb.rotation.eulerAngles;

        if(inpX != 0.0F) {
            euler.z += Mathf.Sign(inpX) * Mathf.Sign(inpZ) * _angularReaction * Time.deltaTime;
            if(inpZ != 0.0F) {
                _dir.x += Mathf.Sign(inpX) * _reaction * Time.deltaTime;
                _dir.x = Mathf.Clamp(_dir.x, -1.0F, 1.0F);
            } else {
                euler.z *= -1.0F;
            }
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

        // Здесь в зависимости от наличия управления задаём тормозящее ускорение.
        // В случае если кнопки нажаты, то используется обычное ускорение.
        System.Single acc = (inpX != 0.0F || inpZ != 0) ? _acceleration : -1.0F * _slowing;
        _speed += acc * Time.deltaTime * Time.deltaTime;
        _speed = Mathf.Clamp(_speed, 0.0F, _maxSpeed);

        if(_speed != 0.0F) {
            //_rb.MovePosition(_rb.position + _speed);
            _rb.MoveRotation(Quaternion.Euler(euler));
        }
	}
    
    private Rigidbody _rb = null;
    private System.Single _speed = 0.0F;
    private Vector3 _dir = new Vector3();

    public System.Single _angularReaction = 10.0F;
    public System.Single _reaction = 1.0F;
    public System.Single _slowing = 1.5F;
    public System.Single _acceleration = 1.0F;
    public System.Single _maxSpeed = 10.0F;
    
}
