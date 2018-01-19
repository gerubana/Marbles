using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	private GameObject player;
	public float smooth = 3f;		// カメラモーションのスムーズ化用変数
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform upPos;			// Front Camera locater
	Transform jumpPos;			// Jump Camera locater
	
	// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	bool isPressCtrl = false;

	string nowPos; 
	
	void Start()
	{
		upPos = GameObject.Find ("Player/pos_up").transform;
		// 各参照の初期化
		//Debug.Log(upPos);
		//Invoke ("setStartPos",2.5f);
	}

	void getGoalPos(){
		this.transform.position =  GameObject.Find("Goal").transform.position;
	}

	void setStartPos(){
		this.GetComponent<Animator> ().applyRootMotion = true;
		//カメラをスタートする
		transform.position = standardPos.position;	
		transform.forward = standardPos.forward;
	}
	
	
	void FixedUpdate ()	// このカメラ切り替えはFixedUpdate()内でないと正常に動かない
	{
		
		if(Input.GetButton("Fire2"))	//Alt
		{	
			nowPos = "UP";
		}

		switch(nowPos)
		{
		case "UP":
			setCameraPositionUpView();
			return;
		default :
			return;
		}
	}
	
	void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false){
			// the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
		}
		else{
			// the camera to standard position and direction / Quick Change
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;
			bQuickSwitch = false;
		}
	}

	
	public void setCameraPositionUpView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position =  upPos.position;	
		transform.forward = upPos.forward;		
	}
}
