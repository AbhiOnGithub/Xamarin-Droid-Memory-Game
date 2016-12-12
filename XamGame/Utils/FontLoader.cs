using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;

namespace XamGame.Utils
{
	public class FontLoader
	{
		private static SparseArray<Typeface> fonts = new SparseArray<Typeface>();
		private static bool fontsLoaded = false;

		public enum Font
		{
			GROBOLD
		}

		public static void LoadFonts()
		{
			fonts.Put(Convert.ToInt32(Font.GROBOLD), Typeface.CreateFromAsset(Application.Context.Assets, "grobold.ttf"));
			fontsLoaded = true;
		}

		public static Typeface GetTypeface(Font font)
		{
			if (!fontsLoaded)
			{
				LoadFonts();
			}
			return fonts.Get(Convert.ToInt32(font));
		}

		public static void SetTypeface(TextView[] textViews, Font font)
		{
			SetTypeFaceToTextViews(textViews, font, TypefaceStyle.Normal);
		}

		public static void setBoldTypeface(TextView[] textViews, Font font)
		{
			SetTypeFaceToTextViews(textViews, font, TypefaceStyle.Bold);
		}

		private static void SetTypeFaceToTextViews(TextView[] textViews, Font font,TypefaceStyle fontStyle)
		{
			if (!fontsLoaded)
			{
				LoadFonts();
			}
			Typeface currentFont = fonts.Get(Convert.ToInt32(font));

			for (int i = 0; i < textViews.Length; i++)
			{
				if (textViews[i] != null)
					textViews[i].SetTypeface(currentFont, fontStyle);
			}
		}
	}
}
