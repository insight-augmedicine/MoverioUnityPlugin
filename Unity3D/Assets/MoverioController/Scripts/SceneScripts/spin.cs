using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {
	private Transform myTransform;
	public float speed= 5f;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.Rotate (0f, speed * Time.deltaTime, 0f );

	}
}
