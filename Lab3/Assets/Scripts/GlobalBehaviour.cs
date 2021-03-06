﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour: MonoBehaviour {
    
    void Awake() {
        _cameraBehaviour = (_cam) ? _cam.GetComponent<CameraBehaviour>() : null;
        _selectedBalls = new List<GameObject>();
    }

    void LateUpdate() {
        // Получаем состояние кнопок мыши.
        bool[] mouseUp = { Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1) };
        bool[] mouseDown = { Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1) };
        bool[] mouseHold = { Input.GetMouseButton(0), Input.GetMouseButton(1) };

        // Разрешаем камере двигаться если удерживаем ПКМ.
        _cameraBehaviour._allowRotation = mouseHold[1];

        // Делаем курсор невидимым если нажата ПКМ.
        if (mouseDown[1])
        {
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else if (mouseUp[1])
        {
            Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        // Делаем трассировку луча только если ЛКМ или ПКМ была нажата единожды.
        if (mouseDown[0] || mouseDown[1])
        {
            //Debug.Log("Doing raycast...");

            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction * 25.0F, out hit) == true)
            {
                GameObject hitObject = hit.collider.gameObject;
                switch (hitObject.tag)
                {
                    case "PlayableBall":
                        if(mouseDown[0])
                        {
                            BallBehaviour behaviour = hitObject.GetComponent<BallBehaviour>();
                            behaviour.IsSelected = true;
                            _selectedBalls.Add(hitObject);
                        }
                        else if(mouseDown[1])
                        {
                            if(_selectedBalls.Count <= 0)
                                Object.Destroy(hitObject);
                        }
                        break;

                    case "FirmGround":
                        if(mouseDown[0])
                        {
                            if(_selectedBalls.Count > 0)
                            {
                                for (System.Int32 i = 0; i < _selectedBalls.Count; ++i)
                                {
                                    Vector3 force = hit.point - _selectedBalls[i].transform.position;
                                    force.Normalize();
                                    force *= _horsepower;
                                    Rigidbody body = _selectedBalls[i].GetComponent<Rigidbody>();
                                    body.AddForce(force);
                                }
                            }
                            else
                            {
                                // Если мы была нажата ЛКМ на поле и при этом ни один шар не выбран,
                                // то создаём новый шар.
                                Vector3 position = hit.point;
                                position.y += 2;
                                Instantiate(Resources.Load("Ball"), position, Quaternion.identity);
                            }
                        }
                        else if(mouseDown[1])
                        {
                            if(_selectedBalls.Count > 0)
                            {
                                // Если ранее были выбраны шари, то делаем их невыбранными и удаляем из списка.
                                for (System.Int32 i = 0; i < _selectedBalls.Count; ++i)
                                {
                                    BallBehaviour behaviour = _selectedBalls[i].GetComponent<BallBehaviour>();
                                    behaviour.IsSelected = false;
                                }
                                _selectedBalls.Clear();
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
    
    // Основная камера и цвет фона которой мы будем менять.
    public Camera _cam = null;
    private CameraBehaviour _cameraBehaviour;
    public System.Single _horsepower = 1.0F;
    private List<GameObject> _selectedBalls;

}
