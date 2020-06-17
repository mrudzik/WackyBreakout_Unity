using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EventName
{
	FreezerEvent,
	SpeederEvent,
	AddPointsEvent,
	RemoveBallEvent,
	BallDiesEvent,
	LastBallLostEvent
}


public static class EventManager
{
	#region Fields

	// All Invokers
	static Dictionary<EventName, List<FloatEventInvoker>> invokers =
		new Dictionary<EventName, List<FloatEventInvoker>>();

	// All Listeners
	static Dictionary<EventName, List<UnityAction<float>>> listeners =
		new Dictionary<EventName, List<UnityAction<float>>>();


	#endregion


	#region Dictionary Event Manager

	/// <summary>
	/// Initializes event manager
	/// </summary>
	public static void Initialize()
	{
		// Creates empty list for all Dictionary entries
		foreach (EventName name in System.Enum.GetValues(typeof(EventName)))
		{
			if (!invokers.ContainsKey(name))
			{
				invokers.Add(name, new List<FloatEventInvoker>());
				listeners.Add(name, new List<UnityAction<float>>());
			}
			else
			{
				invokers[name].Clear();
				listeners[name].Clear();
			}
		}
	}

	public static void AddInvoker(EventName eventName, FloatEventInvoker invoker)
	{
		// Add listeners to new Invoker
		foreach (UnityAction<float> listener in listeners[eventName])
		{
			invoker.AddListener(eventName, listener);
		}
		// Add Invoker to a list in dictionary of invokers
		invokers[eventName].Add(invoker);
	}

	public static void AddListener(EventName eventName, UnityAction<float> listener)
	{
		// Add Listener to all Invokers
		foreach (FloatEventInvoker invoker in invokers[eventName])
		{
			invoker.AddListener(eventName, listener);
		}
		// Add Listener to a list in dictionary of listeners
		listeners[eventName].Add(listener);
	}

	public static void RemoveInvoker(EventName eventName, FloatEventInvoker invoker)
	{
		// Remove invoker from dictionary
		invokers[eventName].Remove(invoker);
	}

	#endregion

}
