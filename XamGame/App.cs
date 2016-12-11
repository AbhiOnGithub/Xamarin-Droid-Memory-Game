using System;
using Android.App;
using XamGame.Utils;

namespace XamGame
{
	public class App : Application
	{
		public override void OnCreate()
		{
			base.OnCreate();
			FontLoader.LoadFonts();
		}
	}
}
