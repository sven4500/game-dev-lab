using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    private void rotateAzimuth(System.Single input)
    {
        // Получаем текущее расположение камеры относительно точки в пространстве.
        Vector3 pos = transform.position - _pinPoint;

        // Делаем проекцию трёхмерного вектора на двухмерную плоскость вращения.
        // В силу того го система координат в Unity не совсем стандартная, то
        // для нас значение "z" является значением "y".
        Vector2 posXZ = new Vector2(pos.x, pos.z);

        System.Single radius = posXZ.magnitude;

        System.Single phi = Vector2.SignedAngle(Vector2.right, posXZ) * Mathf.Deg2Rad;
        System.Single phiToAdd = input * _speed * Time.deltaTime;

        phi = phi + phiToAdd;

        pos.x = radius * Mathf.Cos(phi);
        pos.z = radius * Mathf.Sin(phi);

        // Возвращаемся обраьно в мировую систему координат.
        transform.position = pos + _pinPoint;
    }

    private void rotateElevation(System.Single input)
    {

    }

    private void lookAtPoint()
    {
        Vector3 dir = _pinPoint - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);
    }
	
	void LateUpdate()
    {
        // Здесь логика такая: если нам разрешено вращаться, то вращаемся, если
        // нет то на нет и суда нет. Если вращаемся то сперва поправляем
        // азимутальный угол, потом угол элевации а уже в самом конце поправляем
        // нормаль камеры чтобы мы всегда смотрели на заданую нами точку.
        if(_allowRotation)
        {
            System.Single mouseInputX = Input.GetAxis("Mouse X");
            System.Single mouseInputY = Input.GetAxis("Mouse Y");

            rotateAzimuth(mouseInputX);
            rotateElevation(mouseInputY);

            lookAtPoint();
        }
    }

    // Глобальный параметр которые разрешает либо запрещает вращение камеры.
    // Пока он всегда true однако немного погодя он быдет включаться только
    // по требованию игрока нажатием ПКМ.
    public System.Boolean _allowRotation = true;

    // Скорость вращения камеры.
    public System.Single _speed = 1.0F;

    // Точка на котору направлена камера.
    public Vector3 _pinPoint = new Vector3(0.0F, 0.0F, 0.0F);

}
