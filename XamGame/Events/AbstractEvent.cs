using System;
namespace XamGame.Events
{
	public abstract class AbstractEvent
	{
		public abstract void Fire(IEventObserver eventObserver);
	}
}
