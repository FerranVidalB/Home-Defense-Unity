using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public float speed = 70f;
	private bool impacted = false;
	//public GameObject impactEffect;
	//hola
	public void Seek(Transform _target) {

		target = _target;
	}

	// Update is called once per frame
	void Update() {
		//once the target is killed, the bullet dissapear
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		//the bullet goes to the target
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;


		//when its very close to the target, goes to the hitTarget method to destroy the target
		if (dir.magnitude <= distanceThisFrame && !impacted) {
			impacted = true;
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget() {

		MonsterController enemy = (MonsterController)target.GetComponent<MonsterController>();
		enemy.GetShooted();
		gameObject.Destroy();
	}
}
