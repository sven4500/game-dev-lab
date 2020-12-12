using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonTurnAround: MonoBehaviour {

    void Awake() {
        if(_turnAroundObject != null) {
            _radius = (transform.position - _turnAroundObject.transform.position).magnitude;

            // Получаем координаты объекта относительно начала родителя. Если использовать
            // просто InverseTransformPoint то мы получим координату относительно себя, а
            // это не то что нам нужно.
            _pinPoint = transform.parent.InverseTransformPoint(_turnAroundObject.transform.position);
        }
    }

    void LateUpdate() {
        // На какой угол должен был повернуться спутник (движение происходит только в плоскости XZ).
        System.Single thetaToAdd = Mathf.PI * Time.deltaTime * _turnAroundSpeed;
        
        // Получаем текущее положение в локальной СК и локальной плоскости XZ.
        Vector3 position = transform.localPosition;
        Vector2 positionXZ = new Vector2(position.x - _pinPoint.x, position.z - _pinPoint.z);

        System.Single theta = Vector2.SignedAngle(Vector2.right, positionXZ) * Mathf.Deg2Rad + thetaToAdd;

        position.x = _pinPoint.x + _radius * Mathf.Cos(theta);
        position.z = _pinPoint.z + _radius * Mathf.Sin(theta);

        transform.localPosition = position;
    }

    // Поле задаёт точку вокруг которой бкдет кружиться луна.
    public GameObject _turnAroundObject = null;

    // Скорость с которой вращается спутник.
    public System.Single _turnAroundSpeed = 1.0F;

    private System.Single _radius = 0.0F;
    private Vector3 _pinPoint = new Vector3(0.0F, 0.0F, 0.0F);

}
