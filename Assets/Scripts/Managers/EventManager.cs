using System;
using System.Collections.Generic;
using Enums;
using Utility;

namespace Managers
{
	public class EventManager : PersistentSingleton<EventManager>
	{
		private readonly Dictionary<GameEvents, HashSet<Action>> _actionHandlers = new Dictionary<GameEvents, HashSet<Action>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object>>> _actionHandlersWithOneArg = new Dictionary<GameEvents, HashSet<Action<object>>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object, object, object>>> _actionHandlersWithThreeArgs = new Dictionary<GameEvents, HashSet<Action<object, object, object>>>();
		private readonly Dictionary<GameEvents, HashSet<Action<object, object>>> _actionHandlersWithTwoArgs = new Dictionary<GameEvents, HashSet<Action<object, object>>>();

		public void AddHandler(GameEvents gameEvent, Action handler)
		{
			if (!_actionHandlers.ContainsKey(gameEvent)) _actionHandlers[gameEvent] = new HashSet<Action>();

			_actionHandlers[gameEvent].Add(handler);
		}

		public void AddHandler<T>(GameEvents gameEvent, Action<T> handler)
		{
			if (!_actionHandlersWithOneArg.ContainsKey(gameEvent))
			{
				_actionHandlersWithOneArg[gameEvent] = new HashSet<Action<object>>();
			}

			_actionHandlersWithOneArg[gameEvent].Add(arg => handler((T) arg));
		}

		public void AddHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
		{
			if (!_actionHandlersWithTwoArgs.ContainsKey(gameEvent))
			{
				_actionHandlersWithTwoArgs[gameEvent] = new HashSet<Action<object, object>>();
			}

			_actionHandlersWithTwoArgs[gameEvent].Add((arg1, arg2) => handler((T) arg1, (T1) arg2));
		}

		public void AddHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
		{
			if (!_actionHandlersWithThreeArgs.ContainsKey(gameEvent))
			{
				_actionHandlersWithThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();
			}

			_actionHandlersWithThreeArgs[gameEvent].Add((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
		}

		public void RemoveHandler(GameEvents gameEvent, Action handler)
		{
			if (_actionHandlers.ContainsKey(gameEvent))
			{
				if (_actionHandlers[gameEvent].Count == 1)
				{
					_actionHandlers[gameEvent] = new HashSet<Action>();
				}

				_actionHandlers[gameEvent].Remove(handler);

			}
		}

		public void RemoveHandler<T>(GameEvents gameEvent, Action<T> handler)
		{
			if (_actionHandlersWithOneArg.ContainsKey(gameEvent))
			{
				if (_actionHandlersWithOneArg[gameEvent].Count == 1)
				{
					_actionHandlersWithOneArg[gameEvent] = new HashSet<Action<object>>();
				}

				_actionHandlersWithOneArg[gameEvent].Remove(arg => handler((T) arg));

			}
		}

		public void RemoveHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
		{
			if (_actionHandlersWithTwoArgs.ContainsKey(gameEvent))
			{
				if (_actionHandlersWithTwoArgs[gameEvent].Count == 1)
				{
					_actionHandlersWithTwoArgs[gameEvent] = new HashSet<Action<object, object>>();
				}

				_actionHandlersWithTwoArgs[gameEvent].Remove((arg1, arg2) => handler((T) arg1, (T1) arg2));
			}
		}

		public void RemoveHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
		{
			if (_actionHandlersWithThreeArgs.ContainsKey(gameEvent))
			{
				if (_actionHandlersWithThreeArgs[gameEvent].Count == 1)
				{
					_actionHandlersWithThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();
				}

				_actionHandlersWithThreeArgs[gameEvent]
					.Remove((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
			}
		}

		public void Broadcast(GameEvents gameEvent)
		{
			if (_actionHandlers.TryGetValue(gameEvent, out HashSet<Action> handlers))
			{
				foreach (Action handler in handlers)
				{
					handler();
				}
			}
		}

		public void Broadcast<T>(GameEvents gameEvent, T arg)
		{
			if (_actionHandlersWithOneArg.TryGetValue(gameEvent, out HashSet<Action<object>> handlers))
			{
				foreach (Action<object> handler in handlers)
				{
					handler(arg);
				}
			}
		}

		public void Broadcast<T, T1>(GameEvents gameEvent, T arg1, T1 arg2)
		{
			if (_actionHandlersWithTwoArgs.TryGetValue(gameEvent, out HashSet<Action<object, object>> handlers))
			{
				foreach (Action<object, object> handler in handlers)
				{
					handler(arg1, arg2);
				}
			}
		}

		public void Broadcast<T, T1, T2>(GameEvents gameEvent, T arg1, T1 arg2, T2 arg3)
		{
			if (_actionHandlersWithThreeArgs.TryGetValue(gameEvent, out HashSet<Action<object, object, object>> handlers))
			{
				foreach (Action<object, object, object> handler in handlers)
				{
					handler(arg1, arg2, arg3);
				}
			}
		}
	}
}
