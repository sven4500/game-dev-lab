using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour: MonoBehaviour {

    private void rotateAzimuth(System.Single input) {
        // Получаем текущее расположение камеры относительно точки в пространстве.
        Vector3 pos = transform.position - _pinPoint;

        // Делаем проекцию трёхмерного вектора на двухмерную плоскость вращения.
        // В силу того го система координат в Unity не совсем стандартная, то
        // для нас значение "z" является значением "y".
        Vector2 posXZ = new Vector2(pos.x, pos.z);

        System.Single radius = posXZ.magnitude;

        System.Single phi = Vector2.SignedAngle(Vector2.right, posXZ) * Mathf.Deg2Rad;
        System.Single phiToAdd = input * _azimuthSpeed * Time.deltaTime;

        phi = phi + phiToAdd;

        pos.x = radius * Mathf.Cos(phi);
        pos.z = radius * Mathf.Sin(phi);

        // Возвращаемся обраьно в мировую систему координат.
        transform.position = pos + _pinPoint;
    }

    private void changeHeight(System.Single input) {
        Vector3 pos = transform.position;

        pos.y += input * _heightSpeed * Time.deltaTime;

        if(pos.y > _heightMax)
            pos.y = _heightMax;

        if(pos.y < _heightMin)
            pos.y = _heightMin;

        transform.position = pos;
    }

    private void turnToPoint() {
        Vector3 dir = _pinPoint - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);
    }
	
	void LateUpdate() {
        // Здесь логика такая: если нам разрешено вращаться, то вращаемся, если
        // нет то на нет и суда нет. Если вращаемся то сперва поправляем
        // азимутальный угол, потом угол элевации а уже в самом конце поправляем
        // нормаль камеры чтобы мы всегда смотрели на заданую нами точку.
        if(_allowRotation) {
            System.Single mouseInputX = Input.GetAxis("Mouse X");
            System.Single mouseInputY = Input.GetAxis("Mouse Y");

            rotateAzimuth(mouseInputX);
            changeHeight(mouseInputY);

            turnToPoint();
        }
    }

    // Глобальный параметр которые разрешает либо запрещает вращение камеры.
    // Во время работы приложения этот параметр то включается, то выключается
    // в зависимости от состояния ПКМ.
    public System.Boolean _allowRotation = false;

    // Скорость вращения камеры.
    public System.Single _azimuthSpeed = 1.0F;
    public System.Single _heightSpeed = 1.0F;

    // Точка на котору направлена камера.
    public Vector3 _pinPoint = new Vector3(0.0F, 0.0F, 0.0F);

    // На какую величину камера может менять свою высоту относительно
    // начального значения.
    public System.Single _heightMin = 0.0F;
    public System.Single _heightMax = 1.0F;

}
