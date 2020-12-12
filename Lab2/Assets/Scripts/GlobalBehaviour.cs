using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour : MonoBehaviour {

	void Awake () {
		// Здесь бкдем конструировать сетку состоящую из кирпичиков.
		for (System.UInt32 i = 0; i < 10; ++i) {
			for (System.UInt32 j = 0; j < 8; ++j) {
				Vector3 pos = new Vector3();
				pos.x = -7.0F + 2.0F * j;
				pos.y = 3.5F - 0.4F * i;
				pos.z = 0.0F;
				GameObject obj = Instantiate(Resources.Load("Brick"), pos, Quaternion.identity) as GameObject;
				if (obj != null) {
					BrickBehaviour brick = obj.GetComponent<BrickBehaviour>();
                    if (brick != null) {
                        System.UInt32 health = i % 4 + 1;
                        brick.Health = health;
                    }
				}
			}
		}
	}
}
