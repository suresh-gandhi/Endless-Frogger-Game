using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCharacterScript : MonoBehaviour {

	bool isJumpingUp;
	bool isJumpingDown;
	bool isJumpingLeft;
	bool isJumpingRight;

	bool gameIsPlaying = false;
	public GameObject startPanel;
	public GameObject gameOverPanel;

	public GameObject strip1;
	public GameObject strip2;
	public GameObject strip3;
	public GameObject strip4;
	public GameObject strip5;
	public GameObject strip6;
	public GameObject strip7;
	public GameObject strip8;
	public GameObject strip9;
	public GameObject strip10;
	public GameObject strip11;
	public GameObject strip12;
	public GameObject strip13;
	public GameObject strip14;
	public GameObject strip15;
	public GameObject strip16;
	public GameObject strip17;

	public int score = 0;
	public Text scoreText;
	public int indexOfTheHighestRoadStrip = 0;

	public GameObject BoundaryLeft;
	public GameObject BoundaryRight;

	public float jumpDistanceZ = 2.0f;

	private List<GameObject> strips;
	int stripsCurrentIndex;

	public GameObject[] poolOfStripsPrefabs;

	public GameObject mesh;

	float jumpOffsetX = 2.5f;

	public Vector3 JumpTargetLocation;
	public float movingSpeed = 100.0f;

	private float midWayPointX;
	public float jumpHeightIncrement = 2.0f;

	private float initialPositionY;

	private bool isDead = false;
	private bool isPlayingDeathAnimation = false;


	// Use this for initialization
	void Start () {
		strips = new List<GameObject>();
		isJumpingUp = isJumpingDown = isJumpingRight = isJumpingLeft = false;

		strips.Add (strip1); //strip1.name = "1";
		strips.Add (strip2); //strip2.name = "2";
		strips.Add (strip3); //strip3.name = "3";
		strips.Add (strip4); //strip4.name = "4";
		strips.Add (strip5); //strip5.name = "5";
		strips.Add (strip6); //strip6.name = "6";
		strips.Add (strip7); //strip7.name = "7";
		strips.Add (strip8); //strip8.name = "8";
		strips.Add (strip9); //strip9.name = "9";
		strips.Add (strip10); //strip10.name = "10";
		strips.Add (strip11); //strip11.name = "11";
		strips.Add (strip12); //strip12.name = "12";
		strips.Add (strip13); //strip13.name = "13";
		strips.Add (strip14); //strip14.name = "14";
		strips.Add (strip15); //strip15.name = "15";
		strips.Add (strip16); //strip16.name = "16";
		strips.Add (strip17); //strip17.name = "17";

		stripsCurrentIndex = 0;
		initialPositionY = this.transform.position.y;

		HideGameOverPanel ();

		int isGameReloaded = PlayerPrefs.GetInt ("reloaded");
		if (isGameReloaded == 1) {
			PlayerPrefs.SetInt("reloaded", 0);
			ButtonStartPressed();

		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (gameIsPlaying == false) {
			return;
		}

		if (isDead == true) {
			return;
		}
		/*if (Input.GetMouseButtonDown (0)) {
			if(isJumping == false )
			{
				isJumping = true;
				Jump ();
			}
		}*/

		if (isJumpingUp) {
			if (this.transform.position.x > JumpTargetLocation.x) {
				this.transform.position = new Vector3 (this.transform.position.x - (movingSpeed * Time.deltaTime), this.transform.position.y, this.transform.position.z);
				if (this.transform.position.x > midWayPointX) {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				} else {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				}
			} else {
				isJumpingUp = false;
				this.transform.position = new Vector3 (transform.position.x, initialPositionY, transform.position.z);
			}
		} else if (isJumpingDown) {
			// todo
			if (this.transform.position.x < JumpTargetLocation.x) {
				this.transform.position = new Vector3 (this.transform.position.x + (movingSpeed * Time.deltaTime), this.transform.position.y, this.transform.position.z);
				if (this.transform.position.x < midWayPointX) {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				} else {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				}
			} else {
				isJumpingDown = false;
				this.transform.position = new Vector3 (transform.position.x, initialPositionY, transform.position.z);
			}
		} else if (isJumpingLeft) {
			if (this.transform.position.z > JumpTargetLocation.z) {
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - (movingSpeed * Time.deltaTime));
				if (this.transform.position.z > midWayPointX) {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				} else {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				}
			} else {
				isJumpingLeft = false;
				this.transform.position = new Vector3 (transform.position.x, initialPositionY, transform.position.z);
			}
		} else if (isJumpingRight) {
			if (this.transform.position.z < JumpTargetLocation.z) {
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + (movingSpeed * Time.deltaTime));
				if (this.transform.position.z < midWayPointX) {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				} else {
					this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - jumpHeightIncrement * Time.deltaTime, this.transform.position.z);
				}
			} else {
				isJumpingRight = false;
				this.transform.position = new Vector3 (transform.position.x, initialPositionY, transform.position.z);
			}
		}

		if (isPlayingDeathAnimation == true) {
			UpdateDeathAnimation();
		}

	
	}


	void JumpUp() {
		if(gameIsPlaying == false)
			return;
		// we want to move to the next strip on the field
		//this.transform.position = new Vector3 (strip5.transform.position.x, transform.position.y, transform.position.z);

		// A) move the strips current index by 1 (increment)
		// B) get the strip at this index within the strips list
		// C) get the x position of this new stip and apply it to the chicken

		// A)
		stripsCurrentIndex += 1;
		if (stripsCurrentIndex > indexOfTheHighestRoadStrip) {
			score += 1;
			scoreText.text = "score: " + score.ToString();
			indexOfTheHighestRoadStrip = stripsCurrentIndex;
			Debug.Log("new  score: " + indexOfTheHighestRoadStrip.ToString());
		}

		// B)
		GameObject nextStrip = strips [stripsCurrentIndex] as GameObject;

		// C)
		JumpTargetLocation = new Vector3 (nextStrip.transform.position.x - jumpOffsetX, transform.position.y, transform.position.z);
		midWayPointX = JumpTargetLocation.x + ((this.transform.position.x - JumpTargetLocation.x) / 2);
		mesh.transform.localEulerAngles = new Vector3 (0, 0, 0);

		SpawnNewStrip ();

		// todo: move boundy up
		// we need to to figure out the distance that the chicken will travel as it jumps
		float distanceX = this.transform.position.x - JumpTargetLocation.x;
		BoundaryLeft.transform.position -= new Vector3 (distanceX, 0, 0);
		BoundaryRight.transform.position -= new Vector3 (distanceX, 0, 0);
	}

	void SpawnNewStrip() {
		// A) we take a strip from the pool of strips prefabs at random
		// B) then we instantiate it at the location of the last item of the "strips" list and add the width of this item as an offset (X axis)

		// A)
		int stripsPrefabCount = poolOfStripsPrefabs.Length;
		int randomNumber = Random.Range (0, stripsPrefabCount);
		GameObject item = poolOfStripsPrefabs [randomNumber] as GameObject;
		Transform itemChildTransform = item.transform.GetChild(0) as Transform;
		Transform itemChildOfChildTranform = itemChildTransform.GetChild (0) as Transform;
		float itemWidth = itemChildOfChildTranform.gameObject.GetComponent<Renderer> ().bounds.size.x;
		//float itemWidth = 10.2f; // static width
		Debug.Log("strip width : " + itemWidth.ToString() );

		// get location of the last item:
		GameObject lastStrip = strips [strips.Count - 1] as GameObject;

		GameObject newStrip = Instantiate (item, lastStrip.transform.position, lastStrip.transform.rotation) as GameObject;
		newStrip.transform.position = new Vector3 (newStrip.transform.position.x -  itemWidth, newStrip.transform.position.y, newStrip.transform.position.z);
		strips.Add (newStrip);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy") {
			//Debug.Log("collision");
			DeathAnimation();
		}

		if (other.gameObject.tag == "obstacle") {
			Debug.Log ("hit an obstacle");


			float offsetUpDown = 0;
			float offsetLeftRight = 0;
			// todo:
			if(isJumpingDown){
				offsetUpDown = -2.0f;
			}
			else if(isJumpingUp){
				offsetUpDown = 2.0f;
			}
			else if(isJumpingRight){
				offsetLeftRight = -2.0f;
			}
			else if(isJumpingLeft){
				offsetLeftRight = 2.0f;
			}

			transform.position = new Vector3(transform.position.x + offsetUpDown, initialPositionY, transform.position.z + offsetLeftRight);

			isJumpingUp = isJumpingRight = isJumpingLeft = isJumpingDown = false;
		}
	}

	void DeathAnimation(){
		isPlayingDeathAnimation = true;
	}

	void UpdateDeathAnimation(){
		if (mesh.transform.localScale.z > 0.1) {
			mesh.transform.localScale -= new Vector3 (0, 0, 0.02f);
		} else {
			isPlayingDeathAnimation = false;
			isDead = true;
			BringUpGameOverPanel();
		}
		if (mesh.transform.rotation.eulerAngles.x == 0 || mesh.transform.rotation.eulerAngles.x > 270) {
			mesh.transform.Rotate(-4.0f, 0, 0);
		}

	}

	void SwipeUp(){
		Debug.Log ("consuming swipe up");
		// todo: move chicken up
		if(isJumpingUp == false )
		{
			isJumpingUp = true;
			JumpUp ();
		}
	}

	void SwipeDown(){
		Debug.Log ("consuming swipe down");
		// todo move chicken down
		if(isJumpingDown == false )
		{
			isJumpingDown = true;
			JumpDown ();
		}
	}

	void SwipeLeft(){
		Debug.Log ("consuming swipe left");
		// todo: chicken moving left
		if(isJumpingLeft == false )
		{
			isJumpingLeft = true;
			JumpLeft ();
		}
	}

	void SwipeRight(){
		Debug.Log ("consuming swipe right");
		// todo: chicken moving right
		if(isJumpingRight == false )
		{
			isJumpingRight = true;
			JumpRight ();
		}
	}
	
	void JumpDown(){
			if(gameIsPlaying == false)
				return;
		// A)
		stripsCurrentIndex -= 1;
		if (stripsCurrentIndex < 0) {
			stripsCurrentIndex = 0;
			return;
		}

		
		// B)
		GameObject previousStrip = strips [stripsCurrentIndex] as GameObject;
		
		// C)
		JumpTargetLocation = new Vector3 (previousStrip.transform.position.x - jumpOffsetX, transform.position.y, transform.position.z);
		midWayPointX = JumpTargetLocation.x - ((JumpTargetLocation.x - this.transform.position.x) / 2);

		mesh.transform.localEulerAngles = new Vector3 (0, 180, 0);

		// todo: move boundary down
		float distanceX = JumpTargetLocation.x - this.transform.position.x;
		BoundaryLeft.transform.position += new Vector3 (distanceX, 0, 0);
		BoundaryRight.transform.position += new Vector3 (distanceX, 0, 0);
	}

	void JumpRight(){
		if(gameIsPlaying == false)
			return;
		JumpTargetLocation = new Vector3 (transform.position.x, transform.position.y, transform.position.z + jumpDistanceZ);
		midWayPointX = JumpTargetLocation.z + ((this.transform.position.z - JumpTargetLocation.z ) / 2);
		mesh.transform.localEulerAngles = new Vector3 (0, 90, 0);
	}

	void JumpLeft(){
		if(gameIsPlaying == false)
			return;
		JumpTargetLocation = new Vector3 (transform.position.x, transform.position.y, transform.position.z - jumpDistanceZ);
		midWayPointX = JumpTargetLocation.z - ((JumpTargetLocation.z - this.transform.position.z ) / 2);
		mesh.transform.localEulerAngles = new Vector3 (0, -90, 0);
	}


	void ButtonStartPressed(){
		Debug.Log ("button start pressed");
		gameIsPlaying = true;
		startPanel.SetActive (false);
	}


	void BringUpGameOverPanel(){
		gameOverPanel.SetActive (true);
	}

	void HideGameOverPanel(){
		gameOverPanel.SetActive (false);
	}

	void PlayAgain(){
		Debug.Log ("play again button pressed");
		PlayerPrefs.SetInt ("reloaded", 1);
		Application.LoadLevel ("LEVEL_1");
	}

}











