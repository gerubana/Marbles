using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour {

	public delegate void VoidDelegate(GameObject go);
	public delegate void BoolDelegate(GameObject go, bool enable);

	public VoidDelegate onClick;
	public BoolDelegate onHover;
    public BoolDelegate onPress;

	void OnClick()
	{
		if (onClick != null)
			onClick (gameObject);
	}

	void OnHover(bool isHover)
	{
		if (onHover != null)
			onHover (gameObject, isHover);
	}

	void OnPress(bool isPressed)
	{
		if (onPress != null)
			onPress (gameObject, isPressed);
    }

	public static EventListener Get(GameObject go)
	{
		EventListener listener = go.GetComponent<EventListener> ();
		if (listener == null)
			listener = go.AddComponent<EventListener> ();
		
		return listener;
	}
}
