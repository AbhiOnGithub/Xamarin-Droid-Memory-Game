using System;
using Android.Content;
using Android.Support.V4.App;
using XamGame.Engine;
using XamGame.Events;

namespace XamGame.Common
{
	public class Shared
	{
		public static FragmentActivity Activity; // it's fine for this app, but better move to weak reference
		public static GameEngine Engine;
		public static EventBus EventBus;
	}
}
