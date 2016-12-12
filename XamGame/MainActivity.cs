using Android.App;
using Android.Widget;
using Android.OS;
using XamGame.Common;
using XamGame.Engine;
using XamGame.Events;
using Android.Support.V4.App;
using Android.Graphics;
using XamGame.Utils;

namespace XamGame
{
	[Activity(Label = "Xam Game", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Holo.NoActionBar")]
	public class MainActivity : FragmentActivity
	{
		private ImageView mBackgroundImage;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Shared.Engine = GameEngine.getInstance();
			Shared.EventBus = EventBus.getInstance();


			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			mBackgroundImage = FindViewById<ImageView>(Resource.Id.background_image);

			Shared.Activity= this;
			Shared.Engine.start();
			Shared.Engine.SetBackgroundImageView(mBackgroundImage);

			// set background
			SetBackgroundImage();

			// set menu
			ScreenController.getInstance().openScreen(ScreenController.Screen.MENU);
		}

		private void SetBackgroundImage()
		{
			Bitmap bitmap = GameUtility.ScaleDown(Resource.Drawable.background, GameUtility.ScreenWidth(), GameUtility.ScreenHeight());
			bitmap = GameUtility.Crop(bitmap, GameUtility.ScreenHeight(), GameUtility.ScreenWidth());
			bitmap = GameUtility.downscaleBitmap(bitmap, 2);
			mBackgroundImage.SetImageBitmap(bitmap);
		}
	}
}

