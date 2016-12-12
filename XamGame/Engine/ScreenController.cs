using System.Collections.Generic;
using Android.Support.V4.App;
using XamGame.Common;
using XamGame.Events;
using XamGame.Fragments;

namespace XamGame.Engine
{
	public class ScreenController
	{

		private static ScreenController mInstance = null;
		private static List<Screen> openedScreens = new List<Screen>();
		private FragmentManager mFragmentManager;

		private ScreenController()
		{
		}

		public static ScreenController getInstance()
		{
			if (mInstance == null)
			{
				mInstance = new ScreenController();
			}
			return mInstance;
		}

		public enum Screen
		{
			MENU,
			GAME,
			DIFFICULTY,
			THEME_SELECT
		}

		/// <summary>
		/// Get the last screen.
		/// </summary>
		/// <returns>The last screen.</returns>
		public static Screen getLastScreen()
		{
			return openedScreens[openedScreens.Count - 1];
		}

		/// <summary>
		/// Opens the screen.
		/// </summary>
		/// <param name="screen">Screen.</param>
		public void openScreen(Screen screen)
		{
			mFragmentManager = Shared.Activity.SupportFragmentManager;

			if (screen == Screen.GAME && openedScreens[openedScreens.Count - 1] == Screen.GAME)
			{
				openedScreens.RemoveAt(openedScreens.Count - 1);
			}
			else if (screen == Screen.DIFFICULTY && openedScreens[openedScreens.Count - 1] == Screen.GAME)
			{
				openedScreens.RemoveAt(openedScreens.Count - 1);
				openedScreens.RemoveAt(openedScreens.Count - 1);
			}
			Fragment fragment = getFragment(screen);
			FragmentTransaction fragmentTransaction = mFragmentManager.BeginTransaction();
			fragmentTransaction.Replace(Resource.Id.fragment_container, fragment);
			fragmentTransaction.Commit();
			openedScreens.Add(screen);
		}

		/// <summary>
		/// On the back.
		/// </summary>
		/// <returns><c>true</c>, if back was oned, <c>false</c> otherwise.</returns>
		public bool onBack()
		{
			if (openedScreens.Count > 0)
			{
				Screen screenToRemove = openedScreens[openedScreens.Count - 1];
				openedScreens.RemoveAt(openedScreens.Count - 1);
				if (openedScreens.Count == 0)
				{
					return true;
				}
				Screen screen = openedScreens[openedScreens.Count - 1];
				openedScreens.RemoveAt(openedScreens.Count - 1);
				openScreen(screen);
				if ((screen == Screen.THEME_SELECT || screen == Screen.MENU) &&
						(screenToRemove == Screen.DIFFICULTY || screenToRemove == Screen.GAME))
				{
					Shared.EventBus.Notify(new ResetBackgroundEvent());
				}
				return false;
			}
			return true;
		}

		private Fragment getFragment(Screen screen)
		{
			switch (screen)
			{
				case Screen.MENU:
					return new MenuFragment();
				case Screen.DIFFICULTY:
					return new DifficultySelectFragment();
				case Screen.GAME:
					return new GameFragment();
				case Screen.THEME_SELECT:
					return new ThemeSelectFragment();
				default:
					break;
			}
			return null;
		}
	}

}
