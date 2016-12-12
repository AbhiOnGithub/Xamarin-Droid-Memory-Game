using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Java.Lang;

namespace XamGame.Events
{
	public class EventBus
	{
		private Handler mHandler;
		private readonly object syncLock = new object();
		private static EventBus mInstance = null;
		private ConcurrentDictionary<string, List<IEventObserver>> events = new ConcurrentDictionary<string, List<IEventObserver>>();
		private long delay;
		private EventBus()
		{
			mHandler = new Handler();
		}

		public static EventBus getInstance()
		{
			if (mInstance == null)
			{
				mInstance = new EventBus();
			}
			return mInstance;
		}

		public void listen(string eventType, IEventObserver eventObserver)
		{
			lock (syncLock)
			{
				List<IEventObserver> observers;
				if (events.ContainsKey(eventType))
				{
					observers = events[eventType];
				}
				else
				{
					observers = new List<IEventObserver>();
				}
				observers.Add(eventObserver);
				events.TryAdd(eventType, observers);
			}
		}

		public void unlisten(string eventType, IEventObserver eventObserver)
		{
			lock (syncLock)
			{
				List<IEventObserver> observers = events[eventType];
				if (observers != null)
				{
					observers.Remove(eventObserver);
				}
			}
		}

		public void Notify(IEvent evt)
		{
			lock (syncLock)
			{
				List<IEventObserver> observers;
				if (events.ContainsKey(evt.GetEventType()))
				{
					observers = events[evt.GetEventType()];
					if (observers != null)
					{
						foreach (IEventObserver observer in observers)
						{
							AbstractEvent abstractEvent = (AbstractEvent)evt;
							abstractEvent.Fire(observer);
						}
					}
				}
			}
		}

		public async Task Notify(IEvent evt, long delay)
		{
			await NotifyWithDelay(evt);
		}

		async Task NotifyWithDelay(IEvent evt)
		{
			await Task.Delay(1000);
			EventBus.mInstance.Notify(evt);
		}
	}
}