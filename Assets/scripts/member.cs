using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Text.RegularExpressions;
using Facebook.Unity;

public class member : MonoBehaviour {

	public GameObject Login_btn;
	public GameObject Login_screen;
	public GameObject Join_member;
	public GameObject Login_plug_chk;
	public GameObject Login_plug_link;
	public GameObject Login_MSG;
    public GameObject Btn_close;
    public GameObject Msg_join; 
    public GameObject Msg_login; 
    public GameObject Msg_link; 
    public GameObject Logout_btn; 

	private string login_email; 
	private string login_pw; 

	private string join_name;
	private string join_email; 
	private string join_pw; 

	private string member_link_email; 
	private string member_link_pw; 

	private string member_type = "";
    private string outside_id = "";
    private string outside_name;

    //tmp
    //private string member_no;

	SQL_script SQL;

	void Start()
	{
		SQL = GameObject.Find ("SQL").GetComponent<SQL_script> ();
	}

    void Update()
    {
        if (Goble_Player.member_no == null || Goble_Player.member_no == "")
        {
            Login_btn.SetActive(true);
            Logout_btn.SetActive(false);
        }
        else
        {
            Login_btn.SetActive(false);
            Logout_btn.SetActive(true);
        }
    }

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
        Msg_join.SetActive (false);
        Msg_login.SetActive(false);
        Msg_link.SetActive(false);
        login_email = ""; 
        login_pw = ""; 
        join_name = ""; 
        join_email = ""; 
        join_pw = ""; 
        member_link_email = "";  
        member_link_pw = ""; 
        member_type = ""; 
        outside_id = ""; 
        Login_screen.transform.Find("login_id").GetComponent<UIInput>().value = login_email;
        Login_screen.transform.Find("login_pw").GetComponent<UIInput>().value = login_email;
        Join_member.transform.Find("login_id").GetComponent<UIInput>().value = login_email;
        Join_member.transform.Find("login_nick").GetComponent<UIInput>().value = login_email;
        Join_member.transform.Find("login_pw").GetComponent<UIInput>().value = login_email;
        Login_plug_link.transform.Find("login_id").GetComponent<UIInput>().value = login_email;
        Login_plug_link.transform.Find("login_pw").GetComponent<UIInput>().value = login_email;
	}

    #region 關閉所有會員相關畫面
	public void btn_connect_clean()
	{
		Login_screen.SetActive (false);
		Join_member.SetActive (false);
		Login_plug_chk.SetActive (false);
		Login_plug_link.SetActive (false);
        Login_MSG.SetActive (false);
        Msg_join.SetActive (false);
        Msg_login.SetActive(false);
        Msg_link.SetActive(false);
	}
    #endregion

    public void getMemberLoginInfo()
    {
        login_email = Login_screen.transform.Find ("login_id").GetComponent<UIInput>().value;
        login_pw = Login_screen.transform.Find ("login_pw").GetComponent<UIInput>().value;
        SendLoginInfo(login_email, login_pw, Msg_login);
    }

    public void getMemberLinkInfo()
    {
        member_link_email = Login_plug_link.transform.Find ("login_id").GetComponent<UIInput>().value;
        member_link_pw = Login_plug_link.transform.Find ("login_pw").GetComponent<UIInput>().value;
        SendLoginInfo(member_link_email, member_link_pw, Msg_link);
    }

    #region 會員登入
    public void SendLoginInfo(string mail, string password, GameObject tmpObj)
	{
        string pw = covertMd5 (password);

        string result = SQL.get_check_login (member_type,outside_id,mail,pw);
        Debug.Log ("type = " + member_type + "; id = " + outside_id + "; mail = " + mail + "; pw = " + pw + "; result = " + result);

		if (result == "noMember") 
        {
            tmpObj.SetActive(true);
            tmpObj.GetComponent<UILabel>().text = "* 無此會員!!";
            return;
		}
		else if (result == "PWerror") 
        {
            tmpObj.SetActive(true);
            tmpObj.GetComponent<UILabel>().text = "* 密碼錯誤!!";
            return;
		} 
		else if (result == "error") 
        {
            tmpObj.SetActive(true);
            tmpObj.GetComponent<UILabel>().text = "* 連線異常!!";
            return;
		} 
		else
        {
            btn_connect_clean ();
            Login_MSG.SetActive (true);
            Goble_Player.member_no = result;
			Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = "成 功 登 入!!";
		}
	}
    #endregion

    #region 外部會員登入&串接
	public void OutsideLogin(string type)
	{
		switch(type)
		{
            case "FB":
            //Login_plug_chk.SetActive (true);
                this.GetComponent<member_plugs>().CallFBLogin();
                callbackWait("請 稍 後~");
                break;
		    case "Google":
    			//Login_plug_chk.SetActive (true);
                break;

		}
	}

    public void callbackWait(string msg)
    {
        btn_connect_clean ();
        Login_MSG.SetActive (true);
        Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = msg;
    }

    public void outside_callback(string type, string id, string name)
    {
        member_type = type;
        outside_id = id;
        outside_name = name;

        Debug.Log("type : " + member_type + "; id : " + outside_id + "; name : " + outside_name);

        string result = SQL.member_login_from_outside(member_type, outside_id);
        Debug.Log ("result = " + result);

        if (result == "noMem")
        {
            btn_connect_clean();
            Login_plug_chk.SetActive(true);
        }
        else
        {
            btn_connect_clean();
            Login_MSG.SetActive (true);
            Goble_Player.member_no = result;
            Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = "成 功 登 入!!";
        }
    }

    public void ConcatMember()
    {
        member_link_email = Login_plug_link.transform.Find ("login_id").GetComponent<UIInput>().value;
        member_link_pw = Login_plug_link.transform.Find ("login_pw").GetComponent<UIInput>().value;

        string pw = covertMd5 (member_link_pw);

        string result = SQL.get_check_login (member_type,outside_id,login_email,pw);

        if (result == "noMember") 
        {
            Msg_login.SetActive(true);
            Msg_login.GetComponent<UILabel>().text = "* 無此會員!!";
            return;
        }
        else if (result == "PWerror") 
        {
            Msg_login.SetActive(true);
            Msg_login.GetComponent<UILabel>().text = "* 密碼錯誤!!";
            return;
        } 
        else if (result == "error") 
        {
            Msg_login.SetActive(true);
            Msg_login.GetComponent<UILabel>().text = "* 連線異常!!";
            return;
        } 
        else
        {
            btn_connect_clean ();
            Login_MSG.SetActive (true);
            Goble_Player.member_no = result;
            Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = "成 功 登 入!!";
        }
    }

    #endregion

	public void BTN_MemberJoin()
	{
        btn_connect_clean ();
        Join_member.SetActive(true);
    }

    public void BTN_MemberChk()
    {
        btn_connect_clean ();
        Login_plug_link.SetActive (true);
    }

    #region 傳送註冊資料
	public void SendJoinInfo()
	{
		join_name = Join_member.transform.Find ("login_nick").GetComponent<UIInput>().value;
		join_email = Join_member.transform.Find ("login_id").GetComponent<UIInput>().value;
        join_pw = Join_member.transform.Find ("login_pw").GetComponent<UIInput>().value;

        bool mail_chk = chk_mail(join_email);

        if(!mail_chk){
            Msg_join.SetActive(true);
            Msg_join.GetComponent<UILabel>().text = "* Email格式錯誤";
            return;
        }

		string result = covertMd5 (join_pw);
        Debug.Log ("name = " + join_name + "; email = " + join_email + "; pw = " + result + ";" + " mail_chk = " + mail_chk);

        string result_check = SQL.get_check_mail (join_email);

        if (result_check == "OK")
        {
            Msg_join.SetActive(false);
            string result_join = SQL.MemberJoin (join_name,result,join_email,member_type,outside_id);
            string Msg;

            if(result_join == "success")
                Msg = "成功加入會員，請重新登入";
            else
                Msg = "連線異常，請稍後再試試";

            btn_connect_clean ();

            Login_MSG.SetActive (true);
            Login_MSG.transform.Find ("Label").GetComponent<UILabel> ().text = Msg;
        }
        else if(result_check == "error")
        {
            Msg_join.SetActive(true);
            Msg_join.GetComponent<UILabel>().text = "連線異常，請稍後再試試";
        }
        else
        {
            Msg_join.SetActive(true);
            Msg_join.GetComponent<UILabel>().text = "* 此信箱已使用";
        }
    }
    #endregion

    public void SignOut()
    {
        Goble_Player.member_no = "";
    }

	private string covertMd5(string value)
	{
		MD5 md5 = MD5.Create();//建立一個MD5
		byte[] source = Encoding.Default.GetBytes(value);//將字串轉為Byte[]
		byte[] crypto = md5.ComputeHash(source);//進行MD5加密
		string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
		return result;
	}

    private bool chk_mail(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
            return true;
        else
            return false;
    }

}
