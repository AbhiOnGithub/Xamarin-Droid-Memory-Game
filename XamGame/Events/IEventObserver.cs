using System;
namespace XamGame.Events
{
	public interface IEventObserver
	{
		void onEvent(FlipCardEvent evt);

		void onEvent(DifficultySelectedEvent evt);

		void onEvent(HidePairCardsEvent evt);

		void onEvent(FlipDownCardsEvent evt);

		void onEvent(StartEvent evt);

		void onEvent(ThemeSelectedEvent evt);

		void onEvent(GameWonEvent evt);

		void onEvent(BackGameEvent evt);

		void onEvent(NextGameEvent evt);

		void onEvent(ResetBackgroundEvent evt);
	}
}
