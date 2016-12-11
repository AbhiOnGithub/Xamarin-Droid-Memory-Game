using System;
namespace XamGame.Events
{
	public class HidePairCardsEvent : AbstractEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}
	}
}
