namespace XamGame.Utils
{
	/// <summary>
	/// IOnTimerCount Interface.
	/// </summary>
	public interface IOnTimerCount
	{
		void OnTick(long millisUntilFinished);
		void OnFinish();
	}
}
