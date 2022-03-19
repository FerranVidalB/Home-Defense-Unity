using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	private bool placed;
	[Header("Attributes")]

	public float range = 15f;
	public float fireRate = 1f;
	public float fireCountdow = 0f;

	[Header("Unity Setup Files")]

	public string enemyTag = "Enemy";
	public GameObject areaSphere;
	public Transform partToRotate;
	public float turnSpeed = 10f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	private LineRenderer line;
	public Material lineRendMaterial;
	// Start is called before the first frame update
	void Start() {
		placed = false;
		InvokeRepeating("UpdateTarget", 0f, 0.5f);

		//set the color of the range circle of turret
		line = gameObject.AddComponent<LineRenderer>();
		line.material = lineRendMaterial;
		line.useWorldSpace = false;
	}
	void UpdateTarget() {
		//get the nearest enemy into the range, once has less than 0hp or out of range it updates to other one
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance && enemy.GetComponent<MonsterController>().health > 0) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
		} else {
			target = null;
		}
	}

	internal bool CanBuild() {
		//return if you can build the turret
		changeColor baseTurret = GetComponentInChildren<changeColor>();
		return baseTurret.CanPlace();
	}
	public void DrawCircle(float radius, float lineWidth) {

		int segments = 360;
		line.widthMultiplier = 0.2f;
		line.positionCount = segments + 1;
		int pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
		Vector3[] points = new Vector3[pointCount];
		for (int i = 0; i < pointCount; i++) {
			var rad = Mathf.Deg2Rad * (i * 360f / segments);
			points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
		}
		line.SetPositions(points);

	}
	// Update is called once per frame
	void Update() {
		//if is not placed will draw the range of the turret
		if (!placed)
			DrawCircle(range / 3.2f, 1f);


		//if you dont have target, ignores update method
		if (target == null || target.GetComponent<MonsterController>().health < 0)
			return;

		//faces the turret to the target
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		//shoot when timer gets to 0
		if (fireCountdow <= 0f) {
			if (placed)
				Shoot();

			fireCountdow = 1f / fireRate;
		}
		fireCountdow -= Time.deltaTime;
	}
	public void setPlaced(bool isPlaced) {
		//once is placed, you clear the area range
		if (isPlaced) {
			line.Destroy();
			placed = isPlaced;

			changeColor baseTurret = (changeColor)GetComponentInChildren<changeColor>();
			baseTurret.setPlaced(true);
			Destroy(areaSphere);
		}
	}
	private void Shoot() {
		//creates a bullet that follows the target
		Debug.Log("shoot");
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null) {
			bullet.Seek(target);
		}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
