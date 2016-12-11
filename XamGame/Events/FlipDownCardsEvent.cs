using System;
namespace XamGame.Events
{
	public class FlipDownCardsEvent : AbstractEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}
	}
}
