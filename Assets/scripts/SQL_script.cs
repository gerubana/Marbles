using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class SQL_script : MonoBehaviour {

	//SqlConnection con = new SqlConnection("Data Source=192.168.1.109;Initial Catalog=Maze;Persist Security Info=True;User ID=sa;Password=Gerubana05240"); 
	SqlConnection con = new SqlConnection("server=192.168.1.111;user=sa;database=Marble;uid=sa;password=Gerubana05240;");

	public string MemberJoin(string nickname, string password, string mail, string type, string outside_id){
		
		try
		{
			SqlCommand sqlComm = new SqlCommand("lp_member_join", con);
			sqlComm.CommandType = CommandType.StoredProcedure;
			//參數設定部份
			sqlComm.Parameters.Add("@Member_name", SqlDbType.NVarChar, 20).Value = nickname;
			sqlComm.Parameters.Add("@Member_password", SqlDbType.NVarChar, 256).Value = password;
			sqlComm.Parameters.Add("@Member_mail", SqlDbType.NVarChar, 30).Value = mail;
			sqlComm.Parameters.Add("@type", SqlDbType.NVarChar, 10).Value = type;
			sqlComm.Parameters.Add("@outside_id", SqlDbType.NVarChar, 30).Value = outside_id;

			con.Open();
			sqlComm.ExecuteNonQuery();
            return("success");
            con.Close();

		}
		catch (SqlException ex)
		{
			Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
			return("fail");
		}
	}

	public string get_check_login(string user_mail, string user_pw){

		try
		{
			SqlCommand sqlComm = new SqlCommand("lp_checkLogin", con);
			sqlComm.CommandType = CommandType.StoredProcedure;
			//參數設定部份
			sqlComm.Parameters.Add("@Member_mail", SqlDbType.NVarChar, 30).Value = user_mail;
			sqlComm.Parameters.Add("@Member_pw", SqlDbType.NVarChar, 256).Value = user_pw;

			SqlParameter myret = new SqlParameter("@result", SqlDbType.VarChar, 12);
			myret.Direction = ParameterDirection.Output;
			sqlComm.Parameters.Add(myret);

			string result = "";
			con.Open();
			sqlComm.ExecuteNonQuery();
			result = (string)myret.Value.ToString();
			con.Close();

			return result;

		}
		catch (SqlException ex)
		{
			Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
			return "error";
		}
	}

    public string get_check_mail(string user_mail){

        try
        {
            SqlCommand sqlComm = new SqlCommand("lp_checkMail", con);
            sqlComm.CommandType = CommandType.StoredProcedure;
            //參數設定部份
            sqlComm.Parameters.Add("@Member_mail", SqlDbType.NVarChar, 30).Value = user_mail;

            SqlParameter myret = new SqlParameter("@result", SqlDbType.VarChar, 12);
            myret.Direction = ParameterDirection.Output;
            sqlComm.Parameters.Add(myret);

            string result = "";
            con.Open();
            sqlComm.ExecuteNonQuery();
            result = (string)myret.Value.ToString();
            con.Close();

            return result;

        }
        catch (SqlException ex)
        {
            Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
            return "error";
        }
    }
}
