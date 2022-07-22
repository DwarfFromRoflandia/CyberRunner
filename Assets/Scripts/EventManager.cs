using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class EventManager 
{
	public static Action<float> EventPlay;
	public static Action<bool> Animation_Play;
	public static UnityEvent<int> PickUpCoin = new UnityEvent<int>();

	public static void CoinAdd(int _coin)
    {
        if (PickUpCoin != null)
            PickUpCoin.Invoke(_coin);
    }

}
