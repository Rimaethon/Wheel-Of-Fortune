using System;
using UnityEngine;

namespace Utility
{
    #region Static Instance
	public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T sInstance;

		public static T SInstance
		{
			get {
				if (sInstance != null) return sInstance;

				sInstance = FindObjectOfType<T>();

				if (sInstance != null) return sInstance;
				return null;
			}
			protected set => sInstance = value;
		}

		protected virtual void Awake()
		{
			InitializeInstance();
		}

		private void InitializeInstance()
		{
			if (this is T instance)
			{
				SInstance = instance;
			}
			else
			{
				throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
			}
		}
	}
    #endregion

    #region Singleton
	public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();

			if (this is T instance)
			{
				if (SInstance != null && SInstance != this)
				{
					Debug.LogWarning($"Instance of type {typeof(T)} already exists. Destroying {gameObject.name}.");
					Destroy(gameObject);
				}
				else
				{
					SInstance = instance;
				}

			}
			else
			{
				Debug.LogError($"Instance of type {typeof(T)} could not be created.");
				throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
			}
		}
	}
    #endregion

    #region DontDestroyOnLoad
	public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}
	}
    #endregion

    #region Private Singleton
	public abstract class PrivateSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T sInstance;

		protected virtual void Awake()
		{
			if (this is T instance)
			{
				if (sInstance != null && sInstance != this)
				{
					Destroy(gameObject);
				}
				else
				{
					sInstance = instance;
				}
			}
			else
			{
				Debug.LogError($"Instance of type {typeof(T)} could not be created.");
				throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
			}
		}
	}
    #endregion

	public abstract class PrivatePersistentSingleton<T> : PrivateSingleton<T> where T : MonoBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
		}
	}
}
