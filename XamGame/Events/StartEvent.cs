using System;

namespace XamGame.Events
{
	public class StartEvent : AbstractEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}
	}
}
