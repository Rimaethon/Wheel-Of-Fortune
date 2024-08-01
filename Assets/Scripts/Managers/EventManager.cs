using System;
using System.Collections.Generic;
using Event_System;
using UnityEngine;

namespace Managers
{
	public static class EventManager
	{
		public static void RegisterHandler<EventType>(Action<EventType> handler) where EventType : EventBase
		{
			EventHandlers<EventType>.Register(handler);
		}

		public static void UnregisterHandler<EventType>(Action<EventType> handler) where EventType : EventBase
		{
			EventHandlers<EventType>.Unregister(handler);

		}
		public static void Send<EventType>(EventType eventData) where EventType : EventBase, new()
		{
			Debug.Log("event send"+ eventData.GetType().Name);
			EventHandlers<EventType>.Handle(eventData);
		}

		private class EventHandlers<EventType> where EventType : EventBase
		{
			private static List<Action<EventType>> handlers;
			private static EventHandlers<EventType> instance;

			private static EventHandlers<EventType> Instance => (EventHandlers<EventType>) null;

			public static void Register(Action<EventType> handler)
			{
				handlers.Add(handler);
			}

			public static void Unregister(Action<EventType> handler)
			{
				handlers.Remove(handler);
			}

			public static void Handle(EventType eventData)
			{
				foreach (var handler in handlers)
				{
					handler(eventData);
				}
			}
		}
	}

}
