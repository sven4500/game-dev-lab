//using Syatem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour : MonoBehaviour {

    private List<GameObject> _objects;
    public float _velocity = 1.0f;

    void Awake () {
        // Создаём новый список где будем хранить все выбранные шары.
        _objects = new List<GameObject>();
    }

    void ClearSelection()
    {
        for (int i = 0; i < _objects.Count; ++i)
        {
            BallBehaviour bh = _objects[i].GetComponent<BallBehaviour>();
            if (bh != null)
                bh.IsSelected = false;
        }
        _objects.Clear();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        bool[] mouseBut = {Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1)};

        // Здесь логика работает только на кнопках мыши.
        if(mouseBut[0] == true || mouseBut[1] == true)
        {
            // Здесь храним только что выбранный объект (если имеется конечно).
            GameObject hitObject = null;

            {
                // Формируем луч исходящий из нашей основной камеры и уходящий бесконечно далеко и
                // получаем информацию о столкновении нашего луча с двухмерным объектом.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                if(hit.collider != null)
                {
                    // Будем обрабатывать только объекты с пометкой "PlayableBall".
                    if(hit.collider.gameObject.tag == "PlayableBall")
                    {
                        hitObject = hit.collider.gameObject;
                        Debug.Log("PlayableBall detected");
                    }
                }
            }

            if (mouseBut[0] == true)
            {
                // Здесь логика ЛКМ.
                if (hitObject != null)
                {
                    // ЛКМ + шар = добавляем в список.
                    _objects.Add(hitObject);

                    // Получаем компонент (скрипт) от объекта который мы только что выделили и
                    // говорим ему что он выбран.
                    BallBehaviour bh = hitObject.GetComponent<BallBehaviour>();
                    if (bh != null)
                        bh.IsSelected = true;
                }
                else
                {
                    if (_objects.Count == 0)
                    {
                        // ЛКМ + пространство = создание шара
                        // todo: сделать поддержку всех шаров. Пока те шары для которых
                        // не сущестыует префаба не будут созданы.
                        string res = "Ball_" + Random.Range(1, 15).ToString("D2");

                        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        position.z = 0;

                        // Создаём новый объект там куда указывает мышь. Значение
                        // Quaternion.identity значить что вращения нет.
                        Instantiate(Resources.Load(res), position, Quaternion.identity);
                    }
                    else
                    {
                        // ЛКМ + пространство + шары = движение
                        for (int i = 0; i < _objects.Count; ++i)
                        {
                            Vector3 force3 = new Vector3();
                            force3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            force3 = force3 - _objects[i].transform.position;
                            force3 = force3 / force3.magnitude;
                            force3 = force3 * _velocity;

                            Vector2 force2 = new Vector2();
                            force2.x = force3.x;
                            force2.y = force3.y;

                            Rigidbody2D rb2d = _objects[i].GetComponent<Rigidbody2D>();
                            if (rb2d != null)
                                rb2d.AddForce(force2);
                        }

                        //ClearSelection();
                    }
                }
            }
            else if (mouseBut[1] == true)
            {
                // Здесь логика ПКМ.
                if(hitObject != null)
                {
                    // ПКМ + объект (только один) = уничтожить объект
                    if(_objects.Count == 0)
                    {
                        Destroy(hitObject);
                    }
                }
                else
                {
                    // ПКМ + пространство = сбросить выделение
                    if (_objects.Count != 0)
                    {
                        ClearSelection();
                    }
                }
            }
        }
	}
}
