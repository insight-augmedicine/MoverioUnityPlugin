using UnityEngine;
using System.Collections;

public class MoverioCameraController : MonoBehaviour {

	private static MoverioCameraController _instance;
	public static MoverioCameraController Instance
	{
		get
		{
			if(_instance == null)
			{
				Debug.Log("Please Add MoverioCameraRig Prefab To Scene!");
			}

			return _instance;
		}
	}

	public GameObject StereoCamera, Cam2D;

	//public float PupillaryDistance = 0.05f;

	MoverioDisplayType _displayState;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		//SetPupillaryDistance(PupillaryDistance);
	}

	//public void SetPupillaryDistance(float pDist)
	//{
		//PupillaryDistance = pDist;

		//StereoCamera.transform.localPosition = new Vector3(-PupillaryDistance, 0.0f, 0.0f);
		//StereoCamera.transform.localPosition = new Vector3(PupillaryDistance, 0.0f, 0.0f);
	//}

	void OnEnable()
	{
		MoverioController.OnMoverioStateChange += HandleOnMoverioStateChange;
	}

	void OnDisable()
	{
		MoverioController.OnMoverioStateChange -= HandleOnMoverioStateChange;
	}

	void HandleOnMoverioStateChange (MoverioEventType type)
	{
		switch(type)
		{
		case MoverioEventType.Display3DOff:
			SetCurrentDisplayType(MoverioDisplayType.Display2D);
			break;
		case MoverioEventType.Display3DOn:
			SetCurrentDisplayType(MoverioDisplayType.Display3D);
			break;
		}

	}

	public MoverioDisplayType GetCurrentDisplayState()
	{
		return _displayState;
	}

	public void SetCurrentDisplayType(MoverioDisplayType type)
	{
		_displayState = type;

		switch(_displayState)
		{
		case MoverioDisplayType.Display2D:
			StereoCamera.SetActive (false);
			Cam2D.SetActive (true);
			break;
		case MoverioDisplayType.Display3D:
			StereoCamera.SetActive (true);
			Cam2D.SetActive (false);
			break;
		}
	}


}
