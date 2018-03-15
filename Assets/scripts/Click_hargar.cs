using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Click_hargar : MonoBehaviour {

    private hangar_controller con;

	void Awake()
	{
		EventListener.Get (gameObject).onClick += ObjectOnClick;
		EventListener.Get (gameObject).onPress += ObjectOnPress;
        EventListener.Get (gameObject).onHover += ObjectOnHover;
	}

	void Start()
	{
        con = GameObject.Find ("controller").GetComponent<hangar_controller> ();
	}

	private void ObjectOnClick(GameObject go)
	{
		ObjectOnHover (go, false);
		switch(gameObject.name)
		{
            case "attack+":
                con.attack_add_change("add");
                break;
            case "attack-":
                con.attack_add_change("minus");
                break;
            case "HP+":
                con.HP_add_change("add");
                break;
            case "HP-":
                con.HP_add_change("minus");
                break;
            case "moveSpeed+":
                con.MS_add_change("add");
                break;
            case "moveSpeed-":
                con.MS_add_change("minus");
                break;
            case "shootSpeed+":
                con.SS_add_change("add");
                break;
            case "shootSpeed-":
                con.SS_add_change("minus");
                break;
            case "fillingSpeed+":
                con.FS_add_change("add");
                break;
            case "fillingSpeed-":
                con.FS_add_change("minus");
                break;
            case "max_marble_ball+":
                con.MMB_add_change("add");
                break;
            case "max_marble_ball-":
                con.MMB_add_change("minus");
                break;
    		
		
		}
		Debug.Log (gameObject.name + " : Click");
	}

	private void ObjectOnPress(GameObject go, bool isPress)
	{
        Debug.Log (gameObject.name + " : Press : " + isPress);
        UITexture tmpUI = go.GetComponent<UITexture> ();
        if (tmpUI != null && isPress)
            tmpUI.color = new Color(255.0f, 0f, 0f);
        else if (tmpUI != null && !isPress)
        {
            foreach (GameObject btn in GameObject.FindGameObjectsWithTag("btn"))
            {
                btn.GetComponent<UITexture> ().color = new Color(255.0f, 255.0f, 255.0f);
            }
            tmpUI.color =  new Color(255.0f, 255.0f, 255.0f);
        }
	}

	private void ObjectOnHover(GameObject go, bool isHover)
	{
		UITexture tmpUI = go.GetComponent<UITexture> ();
        if (tmpUI != null && isHover)
            tmpUI.color = new Color(255.0f, 225.0f, 0f);
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
