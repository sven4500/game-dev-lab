using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	
	void LateUpdate()
    {
        // Получаем вход от мыши.
        System.Single mouseX = Input.GetAxis("Mouse X");
        //System.Single mouseY = Input.GetAxis("Mouse Y");

        // Получаем текущее расположение камеры относительно точки в пространстве.
        // Также делаем проекции трёхмерного вектора на двухмерные плоскости.
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        // Сперва работаем с вращением в плоскости XZ.
        {
            // Работаем с системе координат относительно заданной точки.
            pos -= _pinPoint;

            // В силу того го система координат в Unity не совсем стандартная, то
            // для нас значение "z" является значением "y".
            Vector2 posXZ = new Vector2(pos.x, pos.z);

            System.Single radius = posXZ.magnitude;

            System.Single phi = Vector2.SignedAngle(Vector2.right, posXZ) * Mathf.Deg2Rad;
            System.Single phiToAdd = mouseX * _speed * Time.deltaTime;

            phi = phi + phiToAdd;

            pos.x = radius * Mathf.Cos(phi);
            pos.z = radius * Mathf.Sin(phi);

            // Возвращаемся обраьно в мсировую систему координат.
            pos += _pinPoint;
        }

        // Поправляем нормаль камеры чтобы мы всегда смотрели на заданную точку.
        {
            Vector3 dir = _pinPoint - pos;
            dir.Normalize();
            rot = Quaternion.LookRotation(dir);
        }

        transform.position = pos;
        transform.rotation = rot;
    }

    public System.Single _speed = 1.0F;

    // Точка на котору направлена камера.
    public Vector3 _pinPoint = new Vector3(0.0F, 0.0F, 0.0F);

}
