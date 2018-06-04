using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	private Animator myAmimator;
	private Rigidbody myRigibody;
	private float forwardForce = 800.0f;
	private float turnForce = 500.0f;
	private float upForce = 500.0f;
	private float movableRange = 3.4f;
	private float coefficient = 0.95f;

	private bool isEnd = false;

	private GameObject stateText;

	private GameObject scoreText;
	private int score = 0;

	private bool isLButtonDown = false;
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {
		
		this.myAmimator = GetComponent<Animator> ();

		this.myAmimator.SetFloat ("Speed", 1);

		this.myRigibody = GetComponent<Rigidbody>();

		this.stateText = GameObject.Find ("GameResultText");

		this.scoreText = GameObject.Find ("ScoreText");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAmimator.speed *= this.coefficient;
		}

		this.myRigibody.AddForce (this.transform.forward * this.forwardForce);

		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown)&& -this.movableRange < this.transform.position.x) {
			this.myRigibody.AddForce (-this.turnForce, 0, 0);
		} else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange){
			this.myRigibody.AddForce (this.turnForce, 0, 0);
		}

		if (this.myAmimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			    this.myAmimator.SetBool ("Jump", false);
		}

		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			    this.myAmimator.SetBool ("Jump", true);
			    this.myRigibody.AddForce (this.transform.up * this.upForce);
		}



	}

	void OnTriggerEnter (Collider other){


		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			this.stateText.GetComponent<Text> ().text = "GAME OVER";
		}
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			this.stateText.GetComponent<Text> ().text = "CLEAR!!";
		}


		if (other.gameObject.tag == "CoinTag") {

			this.score += 10;
			this.scoreText.GetComponent<Text> ().text = "Score" + this.score + "pt";

			GetComponent<ParticleSystem> ().Play ();
			Destroy (other.gameObject);

		}
	}

	public void GetJumpButtonDown(){
		if(this.transform.position.y < 0.5f){
			this.myAmimator.SetBool("Jump",true);
			this.myRigibody.AddForce(this.transform.up * this.upForce);
		}
	}

	public void GetMyLeftButtonDown(){
		this.isLButtonDown = true;
	}

	public void GetMyLeftBottonUp(){
		this.isLButtonDown = false;
	}
	public void GetMyRightButtonDown(){
		this.isRButtonDown = true;
	}
	public void GetMyRightButtonUp(){
		this.isRButtonDown = false;
	}
}
