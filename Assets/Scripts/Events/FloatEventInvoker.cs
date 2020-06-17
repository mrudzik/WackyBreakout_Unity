using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// To work correctly. You need to manualy add events you need to be used by this Invoker
/// </summary>
public class FloatEventInvoker
{
	protected Dictionary<EventName, UnityEvent<float>> unityEvents =
		new Dictionary<EventName, UnityEvent<float>>();




	~FloatEventInvoker()
	{// When Destructor Called
		foreach (KeyValuePair<EventName, UnityEvent<float>> entry in unityEvents)
		{// This will iterate all items in Dictionary
			// Removing All Invoker signs, to free some memory
			EventManager.RemoveInvoker(entry.Key, this);
		}
	}

	/// <summary>
	/// This will try to initialize needed event in dictionary of events.
	/// Also this object will be added to invoker List of Event Manager
	/// </summary>
	/// <param name="eventName"></param>
	public void AddSupportedEvent(EventName eventName)
	{
		if (!unityEvents.ContainsKey(eventName))
		{
			unityEvents.Add(eventName, new UniversalFloatEvent());
			EventManager.AddInvoker(eventName, this);
		}
	}

	/// <summary>
	/// Adds Listener to specific type of event
	/// </summary>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void AddListener(EventName eventName, UnityAction<float> listener)
	{
		// only add listeners for supported events
		if (unityEvents.ContainsKey(eventName))
		{
			unityEvents[eventName].AddListener(listener);
		}
		
	}

	/// <summary>
	/// Invokes Specific Listeners for this event. Specified by event Name
	/// </summary>
	/// <param name="eventName"></param>
	/// <param name="argument"></param>
	public void InvokeEvent(EventName eventName, float argument)
	{
		if (unityEvents.ContainsKey(eventName))
		{
			unityEvents[eventName].Invoke(argument);
		}
	}
}
