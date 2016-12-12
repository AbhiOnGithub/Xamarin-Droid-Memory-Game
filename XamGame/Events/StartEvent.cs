using System;

namespace XamGame.Events
{
	public class StartEvent : AbstractEvent,IEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(StartEvent);
		}
	}
}
