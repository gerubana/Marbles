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
            con.Close();
            return("success");

		}
		catch (SqlException ex)
		{
			Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
			return("fail");
		}
	}

    public string get_check_login(string type, string id, string user_mail, string user_pw){

		try
		{
			SqlCommand sqlComm = new SqlCommand("lp_checkLogin", con);
			sqlComm.CommandType = CommandType.StoredProcedure;
            //參數設定部份
            sqlComm.Parameters.Add("@type", SqlDbType.NVarChar, 10).Value = type;
            sqlComm.Parameters.Add("@id", SqlDbType.NVarChar, 30).Value = id;
			sqlComm.Parameters.Add("@Member_mail", SqlDbType.NVarChar, 30).Value = user_mail;
			sqlComm.Parameters.Add("@Member_pw", SqlDbType.NVarChar, 256).Value = user_pw;

			SqlParameter myret = new SqlParameter("@result", SqlDbType.VarChar, 30);
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

            SqlParameter myret = new SqlParameter("@result", SqlDbType.VarChar, 30);
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

    public string member_login_from_outside(string type, string id){

        try
        {
            SqlCommand sqlComm = new SqlCommand("lp_memberLoginPlus", con);
            sqlComm.CommandType = CommandType.StoredProcedure;
            //參數設定部份
            sqlComm.Parameters.Add("@type", SqlDbType.NVarChar, 30).Value = type;
            sqlComm.Parameters.Add("@id", SqlDbType.NVarChar, 30).Value = id;

            SqlParameter myret = new SqlParameter("@result", SqlDbType.VarChar, 6);
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


    public DataSet get_machine_data(string member, string rank, string type, string isfinal)
    {
        try
        {
            SqlDataAdapter sqlcomm = new SqlDataAdapter("get_machine", con);
            sqlcomm.SelectCommand.CommandType = CommandType.StoredProcedure;

            sqlcomm.SelectCommand.Parameters.Add("@member_id", SqlDbType.VarChar, 6).Value = member;
            sqlcomm.SelectCommand.Parameters.Add("@rank", SqlDbType.VarChar, 3).Value = rank;
            sqlcomm.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar, 2).Value = type;
            sqlcomm.SelectCommand.Parameters.Add("@isfinal", SqlDbType.VarChar, 1).Value = isfinal;

            DataSet ds = new DataSet();
            con.Open();
            sqlcomm.Fill(ds,"get_machine");
            con.Close();

            return ds;
        }
        catch (SqlException ex)
        {
            Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
            return null;
        }
    }


    public string set_machine_data(string member_id, string machine_type, string attack, string attack_R, string HP, string HP_R, string move_speed, string move_speed_R, string shoot_speed
        , string shoot_speed_R,string filling_speed, string filling_speed_R, string max_marble_ball, string max_marble_ball_R, string skill1, string skill2, string skill3, string AI
        , string aim, string total_rank, string is_use, string is_lock){

        try
        {
            SqlCommand sqlComm = new SqlCommand("update_machine", con);
            sqlComm.CommandType = CommandType.StoredProcedure;
            //參數設定部份
            sqlComm.Parameters.Add("@isNew", SqlDbType.NVarChar, 1).Value = "N";
            sqlComm.Parameters.Add("@member_id", SqlDbType.NVarChar, 6).Value = member_id;
            sqlComm.Parameters.Add("@machine_type", SqlDbType.NVarChar, 2).Value = machine_type;
            sqlComm.Parameters.Add("@attack", SqlDbType.NVarChar, 5).Value = attack;
            sqlComm.Parameters.Add("@attack_R", SqlDbType.NVarChar, 3).Value = attack_R;
            sqlComm.Parameters.Add("@HP", SqlDbType.NVarChar, 5).Value = HP;
            sqlComm.Parameters.Add("@HP_R", SqlDbType.NVarChar, 3).Value = HP_R;
            sqlComm.Parameters.Add("@move_speed", SqlDbType.NVarChar, 5).Value = move_speed;
            sqlComm.Parameters.Add("@move_speed_R", SqlDbType.NVarChar, 3).Value = move_speed_R;
            sqlComm.Parameters.Add("@shoot_speed", SqlDbType.NVarChar, 5).Value = shoot_speed;
            sqlComm.Parameters.Add("@shoot_speed_R", SqlDbType.NVarChar, 3).Value = shoot_speed_R;
            sqlComm.Parameters.Add("@filling_speed", SqlDbType.NVarChar, 5).Value = filling_speed;
            sqlComm.Parameters.Add("@filling_speed_R", SqlDbType.NVarChar, 3).Value = filling_speed_R;
            sqlComm.Parameters.Add("@max_marble_ball", SqlDbType.NVarChar, 5).Value = max_marble_ball;
            sqlComm.Parameters.Add("@max_marble_ball_R", SqlDbType.NVarChar, 3).Value = max_marble_ball_R;
            sqlComm.Parameters.Add("@skill1", SqlDbType.NVarChar, 1).Value = skill1;
            sqlComm.Parameters.Add("@skill2", SqlDbType.NVarChar, 1).Value = skill2;
            sqlComm.Parameters.Add("@skill3", SqlDbType.NVarChar, 1).Value = skill3;
            sqlComm.Parameters.Add("@AI", SqlDbType.NVarChar, 1).Value = AI;
            sqlComm.Parameters.Add("@aim", SqlDbType.NVarChar, 1).Value = aim;
            sqlComm.Parameters.Add("@move_range", SqlDbType.NVarChar, 1).Value = "1";
            sqlComm.Parameters.Add("@total_rank", SqlDbType.NVarChar, 1).Value = total_rank;
            sqlComm.Parameters.Add("@is_use", SqlDbType.NVarChar, 1).Value = is_use;
            sqlComm.Parameters.Add("@is_lock", SqlDbType.NVarChar, 1).Value = is_lock;


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


    public string get_and_set_money(string type, string member_id, string money){

        try
        {
            SqlCommand sqlComm = new SqlCommand("get_and_set_money", con);
            sqlComm.CommandType = CommandType.StoredProcedure;
            //參數設定部份
            sqlComm.Parameters.Add("@type", SqlDbType.NVarChar, 10).Value = type;
            sqlComm.Parameters.Add("@member_id", SqlDbType.NVarChar, 6).Value = member_id;
            sqlComm.Parameters.Add("@money", SqlDbType.NVarChar, 10).Value = money;

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
