using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour: MonoBehaviour
{
    /*private class MouseState
    {
        // Конструктор защищённый поэтому мы запрещаем таким образом
        // самостоятельно создавать объекты этого класса.
        protected MouseState()
        {
        
        }

        public bool LMB()
        {
            return _butDown[0] == true || _butPressed[0] == true;
        }

        public bool RMB()
        {
            return _butDown[1] == true || _butPressed[1] == true;
        }

        public static void getState(out MouseState state)
        {
            state = new MouseState();

            for (int i = 0; i < 2; ++i)
            {
                state._butDown[i] = Input.GetMouseButtonDown(i);
                state._butPressed[i] = Input.GetMouseButton(i);
            }
        }

        public bool[] _butDown = { false, false };
        public bool[] _butPressed = { false, false };
    };*/
    
    void Awake()
    {
        _cameraBehaviour = (_cam) ? _cam.GetComponent<CameraBehaviour>() : null;
    }

    void LateUpdate()
    {
        handleInput();
        //changeColor();
    }

    private void handleInput()
    {
        // Делаем трассировку луча только если кнопка мыши была нажата единожды.
        if (Input.GetMouseButtonDown(0) == true || Input.GetMouseButtonDown(1) == true)
        {
            Debug.Log("Doing raycast...");

            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction * 25.0F, out hit) == true)
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "PlayableBall":
                        Debug.Log("PlayableBall");
                        break;
                    case "FirmGround":
                        Debug.Log("FirmGound");
                        break;
                    default:
                        break;
                }
            }
        }

        _cameraBehaviour._allowRotation = Input.GetMouseButton(1);
    }
    
    private void changeColor()
    {
        Color col = new Color();
        col.r = Random.Range(0.0F, 1.0F);
        col.g = Random.Range(0.0F, 1.0F);
        col.b = Random.Range(0.0F, 1.0F);
        _cam.backgroundColor = col;
    }

    // Основная камера фон которой мы будем менять.
    public Color _col = new Color();
    public Camera _cam = null;
    public System.Single _changeSpeed = 1.0F;
    private CameraBehaviour _cameraBehaviour;

}
