using System;
namespace XamGame.Events
{
	public class DifficultySelectedEvent : AbstractEvent
	{
		private int difficulty;

		public DifficultySelectedEvent(int difficulty)
		{
			this.difficulty = difficulty;
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public override string ToString()
		{
			return nameof(DifficultySelectedEvent);
		}
	}
}
