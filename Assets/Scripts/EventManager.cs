using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public static class EventManager 
{
	public static Action<float> EventPlay;
	public static Action<bool> Animation_Play;
	public static UnityEvent PickUpCoinEvent = new UnityEvent();
	public static Action<string> ChangeNameEvent;
	 
}
