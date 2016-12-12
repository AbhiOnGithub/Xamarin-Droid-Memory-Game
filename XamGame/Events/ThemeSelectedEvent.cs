using System;
using XamGame.Themes;

namespace XamGame.Events
{
	public class ThemeSelectedEvent : AbstractEvent,IEvent
	{
		public Theme theme;

		public ThemeSelectedEvent(Theme theme)
		{
			this.theme = theme;
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(ThemeSelectedEvent);
		}
	}
}
