using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	private GameObject player;
	public float smooth = 3f;		// カメラモーションのスムーズ化用変数
	Transform backPos;			// the usual position for the camera, specified by a transform in the game
	Transform upPos;			// Front Camera locater
	Transform jumpPos;			// Jump Camera locater
	
	// スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
	bool bQuickSwitch = false;	//Change Camera Position Quickly
	bool isPressCtrl = false;

	static string nowPos = "Back"; 
	
	void Start()
	{
		upPos = GameObject.Find (Goble_Player.playerName+"/pos_up").transform;
		backPos = GameObject.Find (Goble_Player.playerName+"/pos_back").transform;
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
	}


	public void setCameraPositionBackView()
	{
		// Change Jump Camera
		bQuickSwitch = false;
		transform.position =  backPos.position;	
		transform.forward = backPos.forward;		
	}
}
