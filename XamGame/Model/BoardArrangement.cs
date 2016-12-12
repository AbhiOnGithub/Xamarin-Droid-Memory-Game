using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using XamGame.Themes;
using XamGame.Utils;

namespace XamGame.Models
{
	public class BoardArrangment
	{
		// like {0-2, 4-3, 1-5}
		public Dictionary<int, int> pairs;
		// like {0-mosters_20, 1-mosters_12, 2-mosters_20, ...}
		public Dictionary<int, String> tileUrls;

		/**
		 * 
		 * @param id
		 *            The id is the number between 0 and number of possible tiles of
		 *            this theme
		 * @return The Bitmap of the tile
		 */
		public Bitmap getTileBitmap(int id, int size)
		{
			String str = tileUrls[id];
			if (str.Contains(GameThemes.URI_DRAWABLE))
			{
				String drawableResourceName = str.Substring(GameThemes.URI_DRAWABLE.Length);
				int drawableResourceId = Application.Context.Resources.GetIdentifier(drawableResourceName, "drawable", Application.Context.PackageName);
				Bitmap bitmap = GameUtility.ScaleDown(drawableResourceId, size, size);
				return GameUtility.Crop(bitmap, size, size);
			}
			return null;
		}

		/// <summary>
		/// Is the pair.
		/// </summary>
		/// <returns><c>true</c>, if pair was ised, <c>false</c> otherwise.</returns>
		/// <param name="id1">Id1.</param>
		/// <param name="id2">Id2.</param>
		public bool isPair(int id1, int id2)
		{
			int integer = pairs[id1];
			return integer.Equals(id2);
		}
	}
}