using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

	private member member_con;

	void Awake()
	{
		EventListener.Get (gameObject).onClick += ObjectOnClick;
		EventListener.Get (gameObject).onPress += ObjectOnPress;
		EventListener.Get (gameObject).onHover += ObjectOnHover;
	}

	void Start()
	{
		member_con = GameObject.Find ("member_controller").GetComponent<member> ();
	}

	private void ObjectOnClick(GameObject go)
	{
		switch(gameObject.name)
		{
		case "Login_btn":
			member_con.ShowLoginScreen();
			return;
		case "Close_member":
			member_con.CloseAllScreen ();
			return;
		case "login_Btn_login":
			member_con.SendLoginInfo ();
			return;
		case "login_Btn_join":
			member_con.MemberJoin ();
			return;
		case "FB" :
		case "Google":
			member_con.OutsideLogin (gameObject.name);
			return;
		case "chk_Btn_yes":
			member_con.MemberChk ();
			return;
		case "chk_Btn_no":
			member_con.MemberJoin ();
			return;
		
		}
		Debug.Log (gameObject.name + " : Click");
	}

	private void ObjectOnPress(GameObject go, bool isPress)
	{
		Debug.Log (gameObject.name + " : Press : " + isPress);
	}

	private void ObjectOnHover(GameObject go, bool isHover)
	{
		UITexture tmpUI = go.GetComponent<UITexture> ();
		if (tmpUI != null && isHover)
			tmpUI.color = new Color (255.0f, 195.0f, 0f);
		else if(tmpUI != null && !isHover)
			tmpUI.color = new Color (255.0f, 255.0f, 255.0f);
	}
}
