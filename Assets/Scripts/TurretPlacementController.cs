using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacementController : MonoBehaviour {

	[SerializeField]
	private Turret placeableObjectPrefab;

	[SerializeField]
	private int actualTurretCost;
	private KeyCode newObjectHotkey = KeyCode.A;
	private int LayerGround;
	private Turret currentPlaceableObject;
	private Material materialBaseTurret;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		HandleNewObjectHotkey();
		if (currentPlaceableObject != null) {
			MoveCurrentPlaceableObjectToMouse();
			ReleaseIfClicked();
		}
	}

	private void ReleaseIfClicked() {

		if (Input.GetMouseButtonUp(0)) {
			Time.timeScale = 1f;
			if (currentPlaceableObject.CanBuild()) {
				GameObject.FindObjectOfType<Base>().PayTurret(actualTurretCost);
				Debug.Log("Can");
				currentPlaceableObject.setPlaced(true);
				currentPlaceableObject = null;
			} else {
				Debug.Log("Destoy");
				Destroy(currentPlaceableObject.gameObject);
			}
			Camera.main.GetComponent<CameraController>().EnableCameraMove();
		}

	}
	public void PlaceNewTurret(int turretCost) {
		Time.timeScale = 0.25f;
		actualTurretCost = turretCost;
		Camera.main.GetComponent<CameraController>().DisableCameraMove();
		if (currentPlaceableObject == null) {
			currentPlaceableObject = Instantiate(placeableObjectPrefab);
			// materialBaseTurret = currentPlaceableObject.transform.Find("BaseTurret").GetComponent<MeshRenderer>().material;
		}
	}

	private void MoveCurrentPlaceableObjectToMouse() {


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo)) {

			/*if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Roads"))
            {
			}
            else
            {
            
            }*/
			currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z + 20);

		}
	}

	private void HandleNewObjectHotkey() {
		//when you click,
		if (Input.GetKeyDown(newObjectHotkey)) {
			if (currentPlaceableObject == null) {
				currentPlaceableObject = Instantiate(placeableObjectPrefab);
				//materialBaseTurret = currentPlaceableObject.transform.Find("BaseTurret").GetComponent<MeshRenderer>().material;
			} else {
				Destroy(currentPlaceableObject);
			}

		}
	}
}
