using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;

public class member : MonoBehaviour {

	public GameObject Login_btn;
	public GameObject Login_screen;
	public GameObject Join_member;
	public GameObject Login_plug_chk;
	public GameObject Login_plug_link;
	public GameObject Login_MSG;
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
		Login_MSG.SetActive (false);
		Btn_close.SetActive (false);
	}

	public void btn_connect_clean()
	{
		Login_screen.SetActive (false);
		Join_member.SetActive (false);
		Login_plug_chk.SetActive (false);
		Login_plug_link.SetActive (false);
		Login_MSG.SetActive (false);
	}

	public void SendLoginInfo()
	{
		login_email = Login_screen.transform.Find ("login_id").GetComponent<UIInput>().value;
		login_pw = Login_screen.transform.Find ("login_pw").GetComponent<UIInput>().value;

		string result = covertMd5 (login_pw);
		Debug.Log ("id = " + login_email + "; pw = " + result + ";");

		btn_connect_clean ();

		Login_MSG.SetActive (true);
		Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = "成 功 登 入!!";
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

	public void BTN_MemberJoin()
	{
		btn_connect_clean ();
		Join_member.SetActive (true);
	}

	public void BTN_MemberChk()
	{
		btn_connect_clean ();
		Login_plug_link.SetActive (true);
	}

	public void SendJoinInfo()
	{
		join_name = Join_member.transform.Find ("login_nick").GetComponent<UIInput>().value;
		join_email = Join_member.transform.Find ("login_id").GetComponent<UIInput>().value;
		join_pw = Join_member.transform.Find ("login_pw").GetComponent<UIInput>().value;

		string result = covertMd5 (join_pw);
		Debug.Log ("name = " + join_name + "; email = " + join_email + "; pw = " + result + ";");

		btn_connect_clean ();

		Login_MSG.SetActive (true);
		Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = "成功加入會員，請重新登入";
	}





	private string covertMd5(string value)
	{
		MD5 md5 = MD5.Create();//建立一個MD5
		byte[] source = Encoding.Default.GetBytes(value);//將字串轉為Byte[]
		byte[] crypto = md5.ComputeHash(source);//進行MD5加密
		string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
		return result;
	}
}
