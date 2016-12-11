using System;
using Android.Util;

namespace XamGame.Utils
{
	public class Clock
	{
		private PauseTimer mPauseTimer = null;
		private static Clock mInstance = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XamGame.Utils.Clock"/> class.
		/// </summary>
		private Clock()
		{
			Log.Info("my_tag", "NEW INSTANCE OF CLOCK");
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		public static Clock getInstance()
		{
			if (mInstance == null)
			{
				mInstance = new Clock();
			}
			return mInstance;
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		/// <param name="millisOnTimer">Millis on timer.</param>
		/// <param name="countDownInterval">Count down interval.</param>
		/// <param name="onTimerCount">On timer count.</param>
		public void startTimer(long millisOnTimer, long countDownInterval, IOnTimerCount onTimerCount)
		{
			if (mPauseTimer != null)
			{
				mPauseTimer.Cancel();
			}
			mPauseTimer = new PauseTimer(millisOnTimer, countDownInterval, true, onTimerCount);
			mPauseTimer.Create();
		}

		/// <summary>
		/// Pause this instance.
		/// </summary>
		public void pause()
		{
			if (mPauseTimer != null)
			{
				mPauseTimer.Pause();
			}
		}

		/// <summary>
		/// Resume this instance.
		/// </summary>
		public void resume()
		{
			if (mPauseTimer != null)
			{
				mPauseTimer.Resume();
			}
		}

		/// <summary>
		/// Stop and cancel the timer
		/// </summary>
		public void cancel()
		{
			if (mPauseTimer != null)
			{   
				mPauseTimer.mOnTimerCount = null;
				mPauseTimer.Cancel();
			}
		}

		/// <summary>
		/// Gets the passed time.
		/// </summary>
		/// <returns>The passed time.</returns>
		public long getPassedTime()
		{
			return mPauseTimer.timePassed();
		}
  	}
}
