using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		this.transform.Rotate (0, Random.Range (0, 360), 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, 3, 0);
	}
}
