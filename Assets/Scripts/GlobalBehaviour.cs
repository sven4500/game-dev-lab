using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour : MonoBehaviour {

	void Awake () {
		// Здесь бкдем конструировать сетку состоящую из кирпичиков.
		for (System.UInt32 i = 0; i < 5; ++i) {
			for (System.UInt32 j = 0; j < 8; ++j) {
				Vector3 pos = new Vector3();
				pos.x = -10.5F + 3.0F * j;
				pos.y = 0.0F + 1.0F * i;
				pos.z = 0.0F;
				GameObject obj = Instantiate(Resources.Load("Brick"), pos, Quaternion.identity) as GameObject;
				if (obj != null) {
					BrickBehaviour brick = obj.GetComponent<BrickBehaviour>();
					if (brick != null)
						brick.Health = i + 1;
				}
			}
		}
	}
}
