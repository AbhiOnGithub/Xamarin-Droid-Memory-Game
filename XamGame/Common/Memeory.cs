using System;
using Android.App;
using Android.Content;
using Android.Preferences;

namespace XamGame.Common
{
	public class Memory
	{
		private static String SHARED_PREFERENCES_NAME = "com.abhishek.xamgame";
		private static String highStartKey = "theme_%d_difficulty_%d";

		/// <summary>
		/// Save the specified theme, difficulty and stars.
		/// </summary>
		/// <param name="theme">Theme.</param>
		/// <param name="difficulty">Difficulty.</param>
		/// <param name="stars">Stars.</param>
		public static void Save(int theme, int difficulty, int stars)
		{
			int highStars = GetHighStars(theme, difficulty);
			if (stars > highStars)
			{
				ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
				ISharedPreferencesEditor edit = sharedPreferences.Edit();
				String key = String.Format(highStartKey, theme, difficulty);
				edit.PutInt(key, stars).Apply();
			}
		}

		/// <summary>
		/// Gets the high stars.
		/// </summary>
		/// <returns>The high stars.</returns>
		/// <param name="theme">Theme.</param>
		/// <param name="difficulty">Difficulty.</param>
		public static int GetHighStars(int theme, int difficulty)
		{
			ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
			String key = String.Format(highStartKey, theme, difficulty);
			return sharedPreferences.GetInt(key, 0);
		}
	}
}
