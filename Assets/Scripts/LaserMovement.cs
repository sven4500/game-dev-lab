using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement: MonoBehaviour {

    void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

	void Update() {
        if(_rb == null) {
            return;
        }
        Vector3 pos = _rb.position;
        pos.z += _speed * Time.deltaTime;
        if(pos.z > _distance) {
            // Убиваем себя так как мы вышли за допустимую границу.
            // Здесь важно уничтожить не себя (this), а именно
            // игровой объект связанный с this.
            GameObject.Destroy(gameObject);
            return;
        }
        _rb.MovePosition(pos);
	}

    private Rigidbody _rb = null;

    public System.Single _distance = 1.0F;
    public System.Single _speed = 1.0F;

}
