using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class member : MonoBehaviour {

	public GameObject Login_btn;
	public GameObject Login_screen;
	public GameObject Join_member;
	public GameObject Login_plug_chk;
	public GameObject Login_plug_link;
	public GameObject Login_success;
	public GameObject Btn_close;

	private GameObject login_Btn_FB;
	private GameObject login_Btn_Google;
	private GameObject login_Btn_login; 
	private GameObject login_Btn_join; 
	private string login_email; 
	private string login_pw; 

	private string join_name;
	private string join_email; 
	private string join_pw; 
	private GameObject join_Btn_ok; 

	private GameObject chk_Btn_yes; 
	private GameObject chk_Btn_no; 

	private string member_line_email; 
	private string member_line_pw; 
	private GameObject link_Btn_ok; 


	private string member_type;


	public void ShowLoginScreen()
	{
		Login_screen.SetActive (true);
		Btn_close.SetActive (true);
	}

	public void CloseAllScreen()
	{
		Login_screen.SetActive (false);
		Join_member.SetActive (false);
		Login_plug_chk.SetActive (false);
		Login_plug_link.SetActive (false);
		Login_success.SetActive (false);
		Btn_close.SetActive (false);
	}

	public void btn_connect_clean()
	{
		Login_screen.SetActive (false);
		Join_member.SetActive (false);
		Login_plug_chk.SetActive (false);
		Login_plug_link.SetActive (false);
		Login_success.SetActive (false);
	}

	public void SendLoginInfo()
	{
		login_email = Login_screen.transform.Find ("login_id").GetComponent<UIInput>().value;
		login_pw = Login_screen.transform.Find ("login_pw").GetComponent<UIInput>().value;
		Debug.Log ("id = " + login_email + "; pw = " + login_pw + ";");
		btn_connect_clean ();
		Login_success.SetActive (true);
	}

	public void OutsideLogin(string type)
	{
		btn_connect_clean ();
		Login_plug_chk.SetActive (true);
		switch(type)
		{
		case "FB":
			//Login_plug_chk.SetActive (true);
			return;
		case "Google":
			//Login_plug_chk.SetActive (true);
			return;

		}
	}

	public void MemberJoin()
	{
		btn_connect_clean ();
		Join_member.SetActive (true);
	}

	public void MemberChk()
	{
		btn_connect_clean ();
		Login_plug_link.SetActive (true);
	}
}
