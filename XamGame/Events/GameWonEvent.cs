using System;
namespace XamGame.Events
{
	public class GameWonEvent : AbstractEvent,IEvent
	{
		public GameState GameState { get; set;}

		public GameWonEvent(GameState gameState)
		{
			this.GameState = gameState;
		}

		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}

		public string GetEventType()
		{
			return nameof(GameWonEvent);
		}
	}
}
