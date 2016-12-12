using System;
namespace XamGame.Events
{
	public class FlipDownCardsEvent : AbstractEvent ,IEvent
	{
		public FlipDownCardsEvent()
		{
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(FlipDownCardsEvent);
		}
	}
}
