using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;

public class Goble_Player {

    public static string member_no;
    public static string machine_no;
    public static int AI_rank;
    public static string AI_machine_no;

    public static System.Data.DataSet ds_play1 = new System.Data.DataSet();
    public static System.Data.DataSet ds_play2 = new System.Data.DataSet();

	public static string playerName;
	public static string player2Name;

	public static bool gameStart = false;
	public static bool gameover = false;

	//public WWW player1_data = new WWW("http://gerubana.byethost4.com/play1.json");

}
