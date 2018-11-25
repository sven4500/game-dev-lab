using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour: MonoBehaviour {

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {
        Vector3 position = _rigidbody.position;
        position.z -= _speed * Time.deltaTime;
        _rigidbody.MovePosition(position);

        // Если булыжник улетел слишком далеко за камеру то он самоуничтожается.
        if(position.z < _deadEnd)
            Object.Destroy(gameObject);
	}

    public System.Single _speed = 1.0F;

    // Устанавливает границу по оси Y после которой камень умирает.
    public System.Single _deadEnd = -10.0F;

    public Rigidbody _rigidbody;

}
