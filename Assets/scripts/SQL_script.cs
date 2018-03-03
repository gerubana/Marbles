using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;

public class SQL_script {

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

		}
		catch (SqlException ex)
		{
			Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
			return("fail");
		}
	}

	public DataSet GetMazeRecord(string filter_sort, string search_keyword, string filter_type, int row_max){


		if (filter_type == "全部顯示") {
			filter_type = "";
		}

		try
		{
			SqlDataAdapter sqlComm = new SqlDataAdapter("lp_get_MazeData", con);
			sqlComm.SelectCommand.CommandType = CommandType.StoredProcedure;
			//參數設定部份
			sqlComm.SelectCommand.Parameters.Add("@reserve_case", SqlDbType.NVarChar, 100).Value = filter_sort;
			sqlComm.SelectCommand.Parameters.Add("@search_case", SqlDbType.NVarChar, 100).Value = search_keyword;
			sqlComm.SelectCommand.Parameters.Add("@filter_case", SqlDbType.NVarChar, 100).Value = filter_type;
			sqlComm.SelectCommand.Parameters.Add("@row_case", SqlDbType.Int).Value = row_max;

			DataSet myDS = new DataSet();

			con.Open();
			sqlComm.Fill(myDS,"lp_get_MazeData");
			con.Close();

			return myDS;

		}
		catch (SqlException ex)
		{
			Debug.Log("Error " + ex.Number + " has occurred: " + ex.Message);
			return null;
		}
	}
}
