using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour {

	private RaycastHit hit;
	private bool m_press = false;

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.collider.gameObject.GetComponent<EventListener> ()) 
			{
				GameObject tmpObject = hit.collider.gameObject;
				EventListener tmpListener = tmpObject.GetComponent<EventListener> ();

				tmpObject.SendMessage ("OnHover",true);

				if (Input.GetMouseButtonDown (0)) 
				{
					m_press = true;
					tmpObject.SendMessage ("OnPress",m_press);
				}

				if (Input.GetMouseButtonUp (0)) 
				{
					if(m_press)
					{
						m_press = false;
						tmpObject.SendMessage ("OnClick");
						tmpObject.SendMessage ("OnPress",m_press);
					}
				}
			}
		}
	}
}
