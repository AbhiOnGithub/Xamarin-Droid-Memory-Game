using Android.OS;

namespace XamGame.Utils
{
	public abstract class CountDownClock : Handler
	{
		/// <summary>
		/// The message.
		/// </summary>
		private static int MSG = 1;

		/// <summary>
		/// Millis since boot when alarm should stop.
		/// </summary>
		private long mStopTimeInFuture;

		/// <summary>
		/// Real time remaining until timer completes
		/// </summary>
		private long mMillisInFuture;

		/// <summary>
		/// Total time on timer at start
		/// </summary>
		private long mTotalCountdown;

		/// <summary>
		/// The interval in millis that the user receives callbacks
		/// </summary>
		private long mCountdownInterval;

		/// <summary>
		/// The time remaining on the timer when it was paused, if it is currently paused; 0 otherwise
		/// </summary>
		private long mPauseTimeRemaining;

		/// <summary>
		/// True if timer was started running, false if not.
		/// </summary>
		private bool mRunAtStart;

		/// <summary>
		/// The number of millis in the future from the call to
		/// </summary>
		/// <param name="millisOnTimer">Millis on timer.</param>
		/// <param name="countDownInterval">Count down interval.</param>
		/// <param name="runAtStart">Run at start.</param>
		public CountDownClock(long millisOnTimer, long countDownInterval, bool runAtStart) {
			mMillisInFuture = millisOnTimer;
			mTotalCountdown = mMillisInFuture;
			mCountdownInterval = countDownInterval;
			mRunAtStart = runAtStart;
		}

		/// <summary>
		/// Cancel the countdown and clears all remaining messages
		/// </summary>
		public void Cancel() {
			this.RemoveCallbacksAndMessages(null);
		}

		/// <summary>
		/// Create the timer object.
		/// </summary>
		public CountDownClock Create() 
		{
			if (mMillisInFuture <= 0)
			{
				OnFinish();
			} else {
				mPauseTimeRemaining = mMillisInFuture;
			}

			if (mRunAtStart) {
				Resume();
			}

			return this;
		}

		/// <summary>
		/// Pauses the counter.
		/// </summary>
		public void Pause() {
			if (IsRunning()) {
				mPauseTimeRemaining = TimeLeft();
				Cancel();
			}
		}

		/// <summary>
		/// Resumes the counter.
		/// </summary>
		public void Resume() {
			if (IsPaused()) {
				mMillisInFuture = mPauseTimeRemaining;
				mStopTimeInFuture = SystemClock.ElapsedRealtime() + mMillisInFuture;
				this.SendMessage(this.ObtainMessage(MSG));
				mPauseTimeRemaining = 0;
			}
		}

		/// <summary>
		/// Tests whether the timer is paused.
		/// </summary>
		/// <returns>The paused.</returns>
		public bool IsPaused() {
			return (mPauseTimeRemaining > 0);
		}

		/// <summary>
		/// Tests whether the timer is running.
		/// </summary>
		/// <returns>The running.</returns>
		public bool IsRunning() {
			return (!IsPaused());
		}

		/// <summary>
		/// Returns the number of milliseconds remaining until the timer is finished
		/// </summary>
		/// <returns>The left.</returns>
		public long TimeLeft() {
			long millisUntilFinished;
			if (IsPaused()) {
				millisUntilFinished = mPauseTimeRemaining;
			} else {
				millisUntilFinished = mStopTimeInFuture - SystemClock.ElapsedRealtime();
				if (millisUntilFinished < 0)
					millisUntilFinished = 0;
			}
			return millisUntilFinished;
		}

		/// <summary>
		/// Returns the number of milliseconds in total that the timer was set to run
		/// </summary>
		/// <returns>The countdown.</returns>
		public long totalCountdown() {
			return mTotalCountdown;
		}

		/// <summary>
		/// Returns the number of milliseconds that have elapsed on the timer.
		/// </summary>
		/// <returns>The passed.</returns>
		public long timePassed() {
			return mTotalCountdown - TimeLeft();
		}

		/// <summary>
		/// Returns true if the timer has been started, false otherwise.
		/// </summary>
		/// <returns>The been started.</returns>
		public bool hasBeenStarted() {
			return (mPauseTimeRemaining <= mMillisInFuture);
		}

		/// <summary>
		/// Callback fired on regular interval
		/// </summary>
		/// <param name="millisUntilFinished">Millis until finished.</param>
		public abstract void onTick(long millisUntilFinished);

		/// <summary>
		/// Callback fired when the time is up.
		/// </summary>
		public abstract void OnFinish();

		/// <summary>
		/// Handles the message.
		/// </summary>
		/// <param name="msg">Message.</param>
		public override void HandleMessage(Message msg) 
		{

			long millisLeft = TimeLeft();
			if (millisLeft <= 0) 
			{
				Cancel();
				OnFinish();
			} 
			else if (millisLeft < mCountdownInterval) 
			{
				// no tick, just delay until done
				this.SendMessageDelayed(this.ObtainMessage(MSG), millisLeft);
			} 
			else 
			{
				long lastTickStart = SystemClock.ElapsedRealtime();
				onTick(millisLeft);

				// take into account user's onTick taking time to execute
				long delay = mCountdownInterval - (SystemClock.ElapsedRealtime() - lastTickStart);

				// special case: user's onTick took more than
				// mCountdownInterval to
				// complete, skip to next interval
				while (delay < 0)
					delay += mCountdownInterval;

				this.SendMessageDelayed(this.ObtainMessage(MSG), delay);
			}
		}
	}
}