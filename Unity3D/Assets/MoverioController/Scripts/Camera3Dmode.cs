//This Software is provided free by inSight Augmented Medicine, LLC. 
//Licensed under BSD license.

//This script handles horizontal compression of the stereoscopic camera which is necessary
//for accurate 3D mode viewing. 

using UnityEngine;
using System.Collections;

public class Camera3Dmode : MonoBehaviour {


	public Camera rightCamera;
	public Camera leftCamera;

	[Range(1, 90)] public float FOV = 13.6f;

	private float right;
	private float left; 
	private float top;
	private float bottom;

	private float aspectRatio = 1.7777777f;
	
	// Use this for initialization
	void Start () {

		//Equations for projection matrix determined experimentally. 

		top = (0.0027f * FOV + 0.0006f);  
		right = top * aspectRatio;  //multiply vertical component of projection matrix by Moverio Aspect Ratio

		bottom = -top;
		left = -right;

		//Get new projection matrix.
		Matrix4x4 mr = PerspectiveOffCenter(left, right, bottom, top, rightCamera.nearClipPlane, rightCamera.farClipPlane);
		Matrix4x4 ml = PerspectiveOffCenter(left, right, bottom, top, leftCamera.nearClipPlane, leftCamera.farClipPlane);

		//Apply the projection matrix to both stereoscopic cameras.
		rightCamera.projectionMatrix = mr;
		leftCamera.projectionMatrix = ml;

		//Once the projection matrix is established the horizontal scaling of the camera will 
		//not cause recalculation of a new matrix and the cameras can have horizontal compression.
	}
	
	
	
	
	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far) {
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}
	void OnEnable() {
		MoverioController.Instance.SetDisplay3D (true);
	}
}
