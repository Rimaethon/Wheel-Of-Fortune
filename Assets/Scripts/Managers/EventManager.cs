using System;
using System.Collections.Generic;
using Event_System;

namespace Managers
{
	public static class EventManager
	{
		public static void RegisterHandler<TEventType>(Action<TEventType> handler) where TEventType : EventBase
		{
			EventHandlers<TEventType>.Register(handler);
		}

		public static void UnregisterHandler<TEventType>(Action<TEventType> handler) where TEventType : EventBase
		{
			EventHandlers<TEventType>.Unregister(handler);

		}

		public static void Send<TEventType>(TEventType eventData) where TEventType : EventBase, new()
		{
			EventHandlers<TEventType>.Handle(eventData);
		}
	}

	public class EventHandlers<EventType> where EventType : EventBase
	{
		private static EventHandlers<EventType> _instance;
		private List<Action<EventType>> _handlers = new List<Action<EventType>>();

		private static EventHandlers<EventType> Instance
		{
			get {
				if (_instance == null)
				{
					_instance = new EventHandlers<EventType>();
				}

				return _instance;
			}
		}

		public static void Register(Action<EventType> handler)
		{
			if (!Instance._handlers.Contains(handler))
			{
				Instance._handlers.Add(handler);
			}
		}

		public static void Unregister(Action<EventType> handler)
		{
			if (Instance._handlers.Contains(handler))
			{
				Instance._handlers.Remove(handler);
			}
		}

		public static void Handle(EventType eventData)
		{
			List<Action<EventType>> handlers = new List<Action<EventType>>(Instance._handlers);

			foreach (Action<EventType> handler in handlers)
			{
				handler.Invoke(eventData);
			}
		}
	}
}
