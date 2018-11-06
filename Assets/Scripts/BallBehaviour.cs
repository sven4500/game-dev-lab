using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    private void Awake () {
        float val = Random.Range(-Mathf.PI * 0.5F, Mathf.PI * 0.5F);

        // Здесь выбираем произвольное расположение вектора в верхней полуплоскости.
        _dir.x = Mathf.Cos(val);//Random.Range(-1.0F, 1.0F);
        _dir.y = Mathf.Sin(val);//Random.Range(0.0F, 1.0F);

        // Получаем компонент твердого тела.
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate () {
        Vector2 tr = _dir * _speed * Time.deltaTime;
        _rb.MovePosition(_rb.position + tr);
    }

    // Метод вызывается всякий раз когда шарик натыкается на препядствие.
    public void Bounce (Vector2 norm) {
        if (norm.x != 0.0F)
            _dir.x *= -1.0F;
        if (norm.y != 0.0F)
            _dir.y *= -1.0F;
    }

    public float _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;

}
