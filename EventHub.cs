using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace Bloops
{
	//its a dead simple event bus.
	//EventHub is a basic static singleton implementation of an event system. It can't pass data around with the events.
	//We could do that, write an EventHub<T>. but that wouldnt get us the flexibility of only maybe listening to the objects being passed around, and
	//it gets kind of messy.
	//For that, i'll use a completely different and more sophisticated system that uses Scriptable Objects.
	public class EventHub : Singleton<EventHub>
	{
		private Dictionary<string, UnityEvent> _events;

		public override void Awake()
		{
			base.Awake();
			Init();
		}

		private void Init()
		{
			if (_events == null)
			{
				_events = new Dictionary<string, UnityEvent>();
			}
		}

		public static void StartListening(string eventName, UnityAction listener)
		{
			if (Instance._events.TryGetValue(eventName, out var thisEvent))
			{
				thisEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new UnityEvent();
				thisEvent.AddListener(listener);
				Instance._events.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			if (Instance._events.TryGetValue(eventName, out var thisEvent))
			{
				thisEvent.RemoveListener(listener);
			}
		}

		public static void TriggerEvent(string eventName)
		{
			if (Instance._events.TryGetValue(eventName, out var triggeredEvent))
			{
				triggeredEvent.Invoke();
			}
		}
	}
}