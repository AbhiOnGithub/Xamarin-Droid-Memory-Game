using System;
using Android.App;
using Android.Media;

namespace XamGame.Common
{
	public class Music
	{
		public static bool OFF;

		public static void PlayCorrent()
		{
			if (!OFF)
			{
				MediaPlayer mp = MediaPlayer.Create(Application.Context, Resource.Raw.correct_answer);
				mp.Completion += (sender, e) =>
				{
					mp.Reset();
					mp.Release();
					mp = null;
				};
				mp.Start();
			}
		}

		public static void playBackgroundMusic()
		{
			// TODO
		}

		public static void showStar()
		{
			if (!OFF)
			{
				MediaPlayer mp = MediaPlayer.Create(Application.Context, Resource.Raw.star);
				mp.Completion += (sender, e) =>
				{
					mp.Reset();
					mp.Release();
					mp = null;
				};
				mp.Start();
			}
		}
	}
}
