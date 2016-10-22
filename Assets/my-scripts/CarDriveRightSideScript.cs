using UnityEngine;
using System.Collections;

public class CarDriveRightSideScript : MonoBehaviour {

	private GameObject startingPoint;
	private GameObject endingPoint;

	public float speed = 23.0f;

	// Use this for initialization
	void Start () {
		startingPoint = GameObject.Find ("starting-point");
		endingPoint = GameObject.Find ("ending-point");
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + speed * Time.deltaTime);
		if (this.transform.position.z > endingPoint.transform.position.z) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, startingPoint.transform.position.z);
		}
	
	}
}
