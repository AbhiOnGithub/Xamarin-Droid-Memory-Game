using System;
namespace XamGame.Events
{
	public class DifficultySelectedEvent : AbstractEvent,IEvent
	{
		public int Difficulty { get; set; }

		public DifficultySelectedEvent(int difficulty)
		{
			this.Difficulty = difficulty;
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public override string ToString()
		{
			return nameof(DifficultySelectedEvent);
		}

		public string GetEventType()
		{
			return nameof(DifficultySelectedEvent);
		}
	}
}
