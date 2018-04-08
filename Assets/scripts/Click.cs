using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Click : MonoBehaviour {

	private member member_con;
    private main_controller main_con;

	void Awake()
	{
		EventListener.Get (gameObject).onClick += ObjectOnClick;
		EventListener.Get (gameObject).onPress += ObjectOnPress;
        EventListener.Get (gameObject).onHover += ObjectOnHover;
	}

	void Start()
	{
		member_con = GameObject.Find ("member_controller").GetComponent<member> ();
        main_con = GameObject.Find ("main_controller").GetComponent<main_controller> ();
	}

	private void ObjectOnClick(GameObject go)
	{
		ObjectOnHover (go, false);
		switch(gameObject.name)
		{
    		case "Login_btn":
    			member_con.ShowLoginScreen();
                break;
    		case "Close_member":
    			member_con.CloseAllScreen ();
                break;
    		case "login_Btn_login":
                member_con.getMemberLoginInfo ();
                break;
    		case "login_Btn_join":
    			member_con.BTN_MemberJoin ();
                break;
    		case "FB" :
    		case "Google":
    			member_con.OutsideLogin (gameObject.name);
                break;
    		case "chk_Btn_yes":
    			member_con.BTN_MemberChk ();
                break;
    		case "chk_Btn_no":
    			member_con.BTN_MemberJoin ();
                break;
            case "join_Btn_ok":
                member_con.SendJoinInfo ();
                break;
            case "link_Btn_ok":
                member_con.getMemberLinkInfo ();
                break;
            case "Logout_btn":
                member_con.SignOut ();
                break;
            case "Hangar":
                Globe.loadName = "Hangar";
                Application.LoadLevel ("Loading");
                break;
            case "Game":
                main_con.change_step(1);
                break;
            case "self":
                main_con.change_step(2);
                break;
            case "BOSS":
                main_con.change_step(3);
                break;
            case "Back":
                main_con.step_back();
                break;
            case "Level1":
            case "Level2":
            case "Level3":
            case "Level4":
            case "Level5":
            case "Level6":
            case "Level7":
            case "Level8":
            case "Level9":
                int level = int.Parse(gameObject.name.Split('l')[1]);
                main_con.select_level(level, 99);
                break;
            case "BOSS0":
            case "BOSS1":
            case "BOSS2":
            case "BOSS3":
                int machine_no = int.Parse(gameObject.name.Split('S')[2]);
                main_con.select_level(10, machine_no);
                break;


		
		}
		//Debug.Log (gameObject.name + " : Click");
	}

	private void ObjectOnPress(GameObject go, bool isPress)
	{
		//Debug.Log (gameObject.name + " : Press : " + isPress);
	}

	private void ObjectOnHover(GameObject go, bool isHover)
	{
		UITexture tmpUI = go.GetComponent<UITexture> ();
        if (tmpUI != null && isHover)
            tmpUI.color = new Color(255.0f, 195.0f, 0f, 255.0f);
        else if (tmpUI != null && !isHover)
        {
            foreach (GameObject btn in GameObject.FindGameObjectsWithTag("btn"))
            {
                btn.GetComponent<UITexture> ().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
            }
            tmpUI.color =  new Color(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }
}
