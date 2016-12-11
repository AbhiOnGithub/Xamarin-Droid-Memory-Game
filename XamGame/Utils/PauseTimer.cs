using System;
namespace XamGame.Utils
{
	public class PauseTimer : CountDownClock
	{
		public IOnTimerCount mOnTimerCount = null;

		public PauseTimer(long millisOnTimer, long countDownInterval, bool runAtStart, IOnTimerCount onTimerCount) : base(millisOnTimer, countDownInterval, runAtStart)
		{
			mOnTimerCount = onTimerCount;
		}

		public override void onTick(long millisUntilFinished)
		{
			if (mOnTimerCount != null)
			{
				mOnTimerCount.OnTick(millisUntilFinished);
			}
		}

		public override void OnFinish()
		{
			if (mOnTimerCount != null)
			{
				mOnTimerCount.OnFinish();
			}
		}
	}
}
