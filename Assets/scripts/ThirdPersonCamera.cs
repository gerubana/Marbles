using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	private GameObject player;
	public float smooth = 3f;		
	Transform backPos;			// the usual position for the camera, specified by a transform in the game
	Transform upPos;			// Front Camera locater
	Transform jumpPos;			// Jump Camera locater
	
	// 切換
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	bool isPressCtrl = false;

	static string nowPos = "Back"; 

	//螢幕搖晃
	private Vector3 axisShakeMin = new Vector3 (-0.2f,0.0f,0f);
	private Vector3 axisShakeMax = new Vector3 (0.2f,0.5f,0.1f);

	private float timeOfShake;
	private float timeOfShakeStore;

	private bool isShake;
	private Vector3 PosStart;
	
	void Start()
	{
		upPos = GameObject.Find (Goble_Player.playerName+"/pos_up").transform;
		backPos = GameObject.Find (Goble_Player.playerName+"/pos_back").transform;
		// 各参照の初期化
		//Debug.Log(upPos);
		//Invoke ("setStartPos",2.5f);
		isShake = false;
		PosStart = upPos.position;
		timeOfShakeStore = timeOfShake;
	}

	void getGoalPos(){
		this.transform.position =  GameObject.Find("Goal").transform.position;
	}

	void setStartPos(){
		this.GetComponent<Animator> ().applyRootMotion = true;
		transform.position = backPos.position;	
		transform.forward = backPos.forward;
	}
	
	
	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{
		
		if(Input.GetButtonDown("Fire2"))	//Alt
		{	
			if (nowPos == "Back")
				nowPos = "Up";
			else
				nowPos = "Back";
		}

		switch(nowPos)
		{
		case "Up":
			setCameraPositionUpView();
			return;
		case "Back":
			setCameraPositionBackView();
			return;
		default :
			return;
		}
	}
	
	void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false){
			// the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, backPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, backPos.forward, Time.fixedDeltaTime * smooth);
		}
		else{
			// the camera to standard position and direction / Quick Change
			transform.position = backPos.position;	
			transform.forward = backPos.forward;
			bQuickSwitch = false;
		}
	}

	
	public void setCameraPositionUpView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position =  upPos.position;	
		transform.forward = upPos.forward;		
		PosStart = upPos.position;
		shakeing ();
	}


	public void setCameraPositionBackView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position =  backPos.position;	
		transform.forward = backPos.forward;	

		PosStart = backPos.position;	
		shakeing ();
	}

	public void shakeCamera(float shakeTime)
	{
		if (shakeTime > 0.0f) {
			timeOfShake = shakeTime;
		} else {
			timeOfShake = timeOfShakeStore;
		}

		isShake = true;
	}

	public void shakeing()
	{
		if (isShake) 
		{
			transform.position = PosStart + new Vector3 (Random.Range (axisShakeMin.x, axisShakeMax.x), Random.Range (axisShakeMin.y, axisShakeMax.y), Random.Range (axisShakeMin.z, axisShakeMax.z));
			timeOfShake -= Time.deltaTime;

			if (timeOfShake <= 0.0f) 
			{
				isShake = false;
				transform.position = PosStart;
			}
		}
	}
}
