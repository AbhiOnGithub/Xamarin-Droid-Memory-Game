using System;
namespace XamGame.Events
{
	public class HidePairCardsEvent : AbstractEvent , IEvent
	{
		public int Id1
		{
			get;
			set;
		}

		public int Id2
		{
			get;
			set;
		}
		public HidePairCardsEvent(int id1, int id2)
		{
			this.Id1 = id1;
			this.Id2 = id2;
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(HidePairCardsEvent);
		}
	}
}
