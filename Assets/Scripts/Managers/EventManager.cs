using System;
using System.Collections.Generic;
using Enums;
using Utility;

namespace Managers
{
	public class EventManager : PersistentSingleton<EventManager>
	{
		private readonly Dictionary<GameEvents, HashSet<Action>> actionHandlers = new Dictionary<GameEvents, HashSet<Action>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object>>> actionHandlersWithOneArg = new Dictionary<GameEvents, HashSet<Action<object>>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object, object, object>>> actionHandlersWithThreeArgs = new Dictionary<GameEvents, HashSet<Action<object, object, object>>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object, object>>> actionHandlersWithTwoArgs = new Dictionary<GameEvents, HashSet<Action<object, object>>>();

		public void AddHandler(GameEvents gameEvent, Action handler)
		{
			if (!actionHandlers.ContainsKey(gameEvent)) actionHandlers[gameEvent] = new HashSet<Action>();

			actionHandlers[gameEvent].Add(handler);
		}

		public void AddHandler<T>(GameEvents gameEvent, Action<T> handler)
		{
			if (!actionHandlersWithOneArg.ContainsKey(gameEvent))
			{
				actionHandlersWithOneArg[gameEvent] = new HashSet<Action<object>>();
			}

			actionHandlersWithOneArg[gameEvent].Add(arg => handler((T) arg));
		}

		public void AddHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
		{
			if (!actionHandlersWithTwoArgs.ContainsKey(gameEvent))
			{
				actionHandlersWithTwoArgs[gameEvent] = new HashSet<Action<object, object>>();
			}

			actionHandlersWithTwoArgs[gameEvent].Add((arg1, arg2) => handler((T) arg1, (T1) arg2));
		}

		public void AddHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
		{
			if (!actionHandlersWithThreeArgs.ContainsKey(gameEvent))
			{
				actionHandlersWithThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();
			}

			actionHandlersWithThreeArgs[gameEvent].Add((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
		}

		public void RemoveHandler(GameEvents gameEvent, Action handler)
		{
			if (actionHandlers.ContainsKey(gameEvent))
			{
				if (actionHandlers[gameEvent].Count == 1)
				{
					actionHandlers[gameEvent] = new HashSet<Action>();
				}

				actionHandlers[gameEvent].Remove(handler);

			}
		}

		public void RemoveHandler<T>(GameEvents gameEvent, Action<T> handler)
		{
			if (actionHandlersWithOneArg.ContainsKey(gameEvent))
			{
				if (actionHandlersWithOneArg[gameEvent].Count == 1)
				{
					actionHandlersWithOneArg[gameEvent] = new HashSet<Action<object>>();
				}

				actionHandlersWithOneArg[gameEvent].Remove(arg => handler((T) arg));

			}
		}

		public void RemoveHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
		{
			if (actionHandlersWithTwoArgs.ContainsKey(gameEvent))
			{
				if (actionHandlersWithTwoArgs[gameEvent].Count == 1)
				{
					actionHandlersWithTwoArgs[gameEvent] = new HashSet<Action<object, object>>();
				}

				actionHandlersWithTwoArgs[gameEvent].Remove((arg1, arg2) => handler((T) arg1, (T1) arg2));
			}
		}

		public void RemoveHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
		{
			if (actionHandlersWithThreeArgs.ContainsKey(gameEvent))
			{
				if (actionHandlersWithThreeArgs[gameEvent].Count == 1)
				{
					actionHandlersWithThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();
				}

				actionHandlersWithThreeArgs[gameEvent]
					.Remove((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
			}
		}

		public void Broadcast(GameEvents gameEvent)
		{
			if (actionHandlers.TryGetValue(gameEvent, out HashSet<Action> handlers))
			{
				foreach (Action handler in handlers)
				{
					handler();
				}
			}
		}

		public void Broadcast<T>(GameEvents gameEvent, T arg)
		{
			if (actionHandlersWithOneArg.TryGetValue(gameEvent, out HashSet<Action<object>> handlers))
			{
				foreach (Action<object> handler in handlers)
				{
					handler(arg);
				}
			}
		}

		public void Broadcast<T, T1>(GameEvents gameEvent, T arg1, T1 arg2)
		{
			if (actionHandlersWithTwoArgs.TryGetValue(gameEvent, out HashSet<Action<object, object>> handlers))
			{
				foreach (Action<object, object> handler in handlers)
				{
					handler(arg1, arg2);
				}
			}
		}

		public void Broadcast<T, T1, T2>(GameEvents gameEvent, T arg1, T1 arg2, T2 arg3)
		{
			if (actionHandlersWithThreeArgs.TryGetValue(gameEvent, out HashSet<Action<object, object, object>> handlers))
			{
				foreach (Action<object, object, object> handler in handlers)
				{
					handler(arg1, arg2, arg3);
				}
			}
		}
	}
}
