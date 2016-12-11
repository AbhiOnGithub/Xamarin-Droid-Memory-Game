using System;
using Android.App;
using Android.Graphics;
using Android.Media;

namespace XamGame.Utils
{
	public class GameUtility
	{
		public static int Px(int dp)
		{
			return (int)(Application.Context.Resources.DisplayMetrics.Density * dp);
		}

		public static int ScreenWidth()
		{
			return Application.Context.Resources.DisplayMetrics.WidthPixels;
		}

		public static int ScreenHeight()
		{
			return Application.Context.Resources.DisplayMetrics.HeightPixels;
		}

		public static Bitmap crop(Bitmap source, int newHeight, int newWidth)
		{
			int sourceWidth = source.Width;
			int sourceHeight = source.Height;

			// Compute the scaling factors to fit the new height and width,
			// respectively.
			// To cover the final image, the final scaling will be the bigger
			// of these two.
			float xScale = (float)newWidth / sourceWidth;
			float yScale = (float)newHeight / sourceHeight;
			float scale = Math.Max(xScale, yScale);

			// Now get the size of the source bitmap when scaled
			float scaledWidth = scale * sourceWidth;
			float scaledHeight = scale * sourceHeight;

			// Let's find out the upper left coordinates if the scaled bitmap
			// should be centered in the new size give by the parameters
			float left = (newWidth - scaledWidth) / 2;
			float top = (newHeight - scaledHeight) / 2;

			// The target rectangle for the new, scaled version of the source bitmap
			// will now
			// be
			RectF targetRect = new RectF(left, top, left + scaledWidth, top + scaledHeight);

			// Finally, we create a new bitmap of the specified size and draw our
			// new,
			// scaled bitmap onto it.
			Bitmap dest = Bitmap.CreateBitmap(newWidth, newHeight, source.GetConfig());
			Canvas canvas = new Canvas(dest);
			canvas.DrawBitmap(source, null, targetRect, null);

			return dest;
		}

		/// <summary>
		/// Scales down the Bitmap
		/// </summary>
		/// <returns>The down.</returns>
		/// <param name="resource">Resource.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
		public static Bitmap ScaleDown(int resource, int reqWidth, int reqHeight)
		{
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InJustDecodeBounds = true;
			BitmapFactory.DecodeResource(Application.Context.Resources, resource);

			// Calculate inSampleSize
			options.InSampleSize = (int)CalculateInSampleSize(options, reqWidth, reqHeight);

			// Decode bitmap with inSampleSize set
			options.InJustDecodeBounds = false;
			return BitmapFactory.DecodeResource(Application.Context.Resources, resource, options);
		}

		/// <summary>
		/// Downscales a bitmap by the specified factor
		/// </summary>
		/// <returns>The bitmap.</returns>
		/// <param name="wallpaper">Wallpaper.</param>
		/// <param name="factor">Factor.</param>
		public static Bitmap downscaleBitmap(Bitmap wallpaper, int factor)
		{
			// convert to bitmap and get the center
			int widthPixels = wallpaper.Width / factor;
			int heightPixels = wallpaper.Height / factor;
			return ThumbnailUtils.ExtractThumbnail(wallpaper, widthPixels, heightPixels);
		}

		/// <summary>
		/// Calculates the size of in sample.
		/// </summary>
		/// <returns>The in sample size.</returns>
		/// <param name="options">Options.</param>
		/// <param name="reqWidth">Req width.</param>
		/// <param name="reqHeight">Req height.</param>
		public static double CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
		{

			// Raw height and width of image
			int height = options.OutHeight;
			int width = options.OutWidth;
			int inSampleSize = 1;

			if (height > reqHeight || width > reqWidth)
			{

				// Calculate ratios of height and width to requested height and
				// width
				decimal heightRatio = Math.Round(Convert.ToDecimal(height / reqHeight));
				decimal widthRatio = Math.Round(Convert.ToDecimal(width / reqWidth));

				// Choose the smallest ratio as inSampleSize value, this will
				// guarantee
				// a final image with both dimensions larger than or equal to the
				// requested height and width.
				inSampleSize = Convert.ToInt32(heightRatio < widthRatio ? heightRatio : widthRatio);
			}
			return inSampleSize;
		}
	}
}