using System;
namespace XamGame.Events
{
	public class ThemeSelectedEvent : AbstractEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}
	}
}
