using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour: MonoBehaviour {

    void Awake() {

    }
    
    // Принимает на вход положение объекта в пространстве за которым
    // следит камера и ограничивающие пространство точки.
    public void TargetCamera(Vector3 pos) {
        pos.x = Mathf.Clamp(pos.x, -_targetAmplitude, _targetAmplitude);
        pos.y = Mathf.Clamp(pos.y, -_targetAmplitude, _targetAmplitude);

        System.Single factorX = ((_targetAmplitude - pos.x) - _targetAmplitude) / _targetAmplitude;
        System.Single factorY = ((_targetAmplitude - pos.y) - _targetAmplitude) / _targetAmplitude;

        Vector3 cameraPos = transform.position;
        cameraPos.x = -_cameraAmplitude * factorX;
        cameraPos.y = _cameraAmplitude * factorY;
        cameraPos.z = -10.0F + _cameraAmplitude * _curvatureStrength * Mathf.Abs(factorX) * Mathf.Abs(factorX);

        transform.position = cameraPos;
        LookAtPoint();
    }

    private void LookAtPoint() {
        Quaternion rot = Quaternion.LookRotation(_targetPoint - transform.position, Vector3.up);
        transform.rotation = rot;
    }

    // Максимальное значение на которое может двигаться объект.
    public System.Single _targetAmplitude = 1.0F;

    // Насколько далеко камера может двигаться.
    public System.Single _cameraAmplitude = 1.0F;
    
    // Радиус по которому скользит камера в горизонтальном положении.
    public Vector3 _targetPoint;

    public System.Single _curvatureStrength = 1.0F;
    
}
