using System;
using Android.Support.V4.App;
using XamGame.Events;

namespace XamGame.Fragments
{
	public class BaseFragment : Fragment, IEventObserver
	{
		public virtual void onEvent(HidePairCardsEvent evt)
		{
		}

		public virtual void onEvent(StartEvent evt)
		{
		}

		public virtual void onEvent(GameWonEvent evt)
		{
		}

		public virtual void onEvent(NextGameEvent evt)
		{
		}

		public virtual void onEvent(ResetBackgroundEvent evt)
		{
		}

		public virtual void onEvent(BackGameEvent evt)
		{
		}

		public virtual void onEvent(ThemeSelectedEvent evt)
		{
		}

		public virtual void onEvent(FlipDownCardsEvent evt)
		{
		}

		public virtual void onEvent(DifficultySelectedEvent evt)
		{
		}

		public virtual void onEvent(FlipCardEvent evt)
		{
		}
	}
}