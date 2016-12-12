using System;
namespace XamGame.Events
{
	public class NextGameEvent : AbstractEvent , IEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(NextGameEvent);
		}
	}
}