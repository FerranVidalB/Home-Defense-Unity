using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum TurretStatusPlacement {
	ABLE, UNABLE, PLACED
}

public class changeColor : MonoBehaviour {

    public Transform body;
    public Transform head;

    private bool collided;
	private bool placed = false;
	private int actualCollisions;
	private bool hitNavmesh;
	private bool preventAccidentalClick;
	// Start is called before the first frame update

	void Start() {

		//this is used to prevent autoplace in a wrong position when collides with other turrets
		preventAccidentalClick = true;
		ChangeColor(TurretStatusPlacement.UNABLE);
		actualCollisions = 0;
		hitNavmesh = false;

	}

	// Update is called once per frame

	void Update() {
		if (preventAccidentalClick) {
			preventAccidentalClick = false;
			if (actualCollisions == 0)
				ChangeColor(TurretStatusPlacement.ABLE);
		}


		//puts in color red the base if collides the road
		NavMeshHit hit;
		if (NavMesh.SamplePosition(transform.position, out hit, (GetComponent<Collider>().bounds.size.x / 2) + 1, NavMesh.AllAreas)) {
			if (!hitNavmesh) {
				hitNavmesh = true;
			}

			ChangeColor(TurretStatusPlacement.UNABLE);
		} else {
			if (hitNavmesh) {

				hitNavmesh = false;

				if (actualCollisions == 0) {
					if (!placed) {
						if (!collided) {
							ChangeColor(TurretStatusPlacement.ABLE);
						}
					}
				}
			}


		}
	}
	private void OnTriggerEnter(Collider col) {
		//is is not placed, enters in base will be unplaceable
		if (!placed) {
			if (col.gameObject.tag == "BaseTurret")// || col.gameObject.tag == "Base" col.gameObject.tag == "Enemy" || 
			{
                Debug.Log("Unable");
                Debug.Log("in");
				actualCollisions++;
				ChangeColor(TurretStatusPlacement.UNABLE);
				collided = true;

			}
		}
	}
	void OnTriggerExit(Collider col) {
		if (!placed) {
			if (col.gameObject.tag == "BaseTurret")// || col.gameObject.tag == "Base" col.gameObject.tag == "Enemy" ||
			{
				actualCollisions--;
				if (actualCollisions == 0) {
					ChangeColor(TurretStatusPlacement.ABLE);
					collided = false;
				}

			}
		}
	}

	internal bool CanPlace() {
        return GetComponent<MeshRenderer>().material.GetFloat("_Fresnel") == 0;
		
	}

	public void ChangeColor(TurretStatusPlacement status) {
		switch (status) {
			case TurretStatusPlacement.UNABLE:
                GetComponent<MeshRenderer>().material.SetFloat("_Fresnel" , 1);
                body.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 1);
                head.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 1);

                //GetComponent<MeshRenderer>().material.color = Color.red;
                break;
			case TurretStatusPlacement.ABLE:
                GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                body.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                head.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                //GetComponent<MeshRenderer>().material.color = Color.white;
                break;
			case TurretStatusPlacement.PLACED:
				Color color = Color.white;
				color.a = 0.0f;
                //GetComponent<MeshRenderer>().material.color = Color.clear;
                body.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                head.GetComponent<MeshRenderer>().material.SetFloat("_Fresnel", 0);
                break;
			default:
				break;
		}

	}
	public void setPlaced(bool status) {
		StandardShaderUtils.ChangeRenderMode(GetComponent<MeshRenderer>().material, StandardShaderUtils.BlendMode.Fade);
		placed = status;
		if (placed) {
			ChangeColor(TurretStatusPlacement.PLACED);
		}
	}

}
