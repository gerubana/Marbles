﻿using System.Collections;
using System.Collections.Generic;
using System;
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
		ObjectOnHover (go, false);
		switch(gameObject.name)
		{
    		case "Login_btn":
    			member_con.ShowLoginScreen();
    			return;
    		case "Close_member":
    			member_con.CloseAllScreen ();
    			return;
    		case "login_Btn_login":
                member_con.getMemberLoginInfo ();
    			return;
    		case "login_Btn_join":
    			member_con.BTN_MemberJoin ();
    			return;
    		case "FB" :
    		case "Google":
    			member_con.OutsideLogin (gameObject.name);
    			return;
    		case "chk_Btn_yes":
    			member_con.BTN_MemberChk ();
    			return;
    		case "chk_Btn_no":
    			member_con.BTN_MemberJoin ();
    			return;
            case "join_Btn_ok":
                member_con.SendJoinInfo ();
                return;
            case "link_Btn_ok":
                member_con.getMemberLinkInfo ();
                return;
            case "Logout_btn":
                member_con.SignOut ();
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
            tmpUI.color = new Color(255.0f, 195.0f, 0f);
        else if (tmpUI != null && !isHover)
        {
            foreach (GameObject btn in GameObject.FindGameObjectsWithTag("btn"))
            {
                btn.GetComponent<UITexture> ().color = new Color(255.0f, 255.0f, 255.0f);
            }
            tmpUI.color =  new Color(255.0f, 255.0f, 255.0f);
        }
    }
}
