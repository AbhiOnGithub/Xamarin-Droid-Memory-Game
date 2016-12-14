using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using XamGame.Utils;

namespace XamGame.Themes
{
	public class GameThemes
	{
		public static String URI_DRAWABLE = "drawable://";

		public static Theme createEmoticonsTheme()
		{
			Theme theme = new Theme();
			theme.Id = 3;
			theme.Name = "Emoticons";
			theme.BackgroundImageUrl = URI_DRAWABLE + "back_animals";
			theme.TileImageUrls = new List<String>();
			// 40 drawables
			for (int i = 1; i <= 40; i++)
			{
				theme.TileImageUrls.Add(URI_DRAWABLE + $"emoticon_{i}");
			}
			return theme;
		}


		public static Theme createAnimalsTheme()
		{
			Theme theme = new Theme();
			theme.Id = 1;
			theme.Name = "Animals";
			theme.BackgroundImageUrl = URI_DRAWABLE + "back_animals";
			theme.TileImageUrls = new List<String>();
			// 40 drawables
			for (int i = 1; i <= 28; i++)
			{
				theme.TileImageUrls.Add(URI_DRAWABLE + $"animals_{i}");
			}
			return theme;
		}

		public static Theme createMosterTheme()
		{
			Theme theme = new Theme();
			theme.Id = 2;
			theme.Name = "Mosters";
			theme.BackgroundImageUrl = URI_DRAWABLE + "back_horror";
			theme.TileImageUrls = new List<String>();
			// 40 drawables
			for (int i = 1; i <= 40; i++)
			{
				theme.TileImageUrls.Add(URI_DRAWABLE + $"mosters_{i}");
			}
			return theme;
		}

		public static Bitmap getBackgroundImage(Theme theme)
		{
			String drawableResourceName = theme.BackgroundImageUrl.Substring(GameThemes.URI_DRAWABLE.Length);
			int drawableResourceId = Application.Context.Resources.GetIdentifier(drawableResourceName, "drawable", Application.Context.PackageName);
			Bitmap bitmap = GameUtility.ScaleDown(drawableResourceId, GameUtility.ScreenWidth(), GameUtility.ScreenHeight());
			return bitmap;
		}
	}
}