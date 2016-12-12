using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamGame.Common;
using XamGame.Utils;

namespace XamGame.UI
{
	public class PopupSettingsView : LinearLayout
	{
		private ImageView mSoundImage;
		private TextView mSoundText;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XamGame.UI.PopupSettingsView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public PopupSettingsView(Context context) : this(context, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XamGame.UI.PopupSettingsView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public PopupSettingsView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Orientation = Orientation.Vertical;
			SetBackgroundResource(Resource.Drawable.settings_popup);
			LayoutInflater.From(Application.Context).Inflate(Resource.Layout.popup_settings_view, this, true);
			mSoundText = (TextView)FindViewById(Resource.Id.sound_off_text);
			TextView rateView = (TextView)FindViewById(Resource.Id.rate_text);
			FontLoader.SetTypeface(new TextView[] { mSoundText, rateView }, FontLoader.Font.GROBOLD);
			mSoundImage = (ImageView)FindViewById(Resource.Id.sound_image);
			View soundOff = FindViewById(Resource.Id.sound_off);
			soundOff.Click += (sender, e) =>
			{
				Music.OFF = !Music.OFF;
				SetMusicButton();
			};

			View rate = FindViewById(Resource.Id.rate);
			rate.Click += (sender, e) =>
			{
				String appPackageName = Application.Context.PackageName;
				try
				{
					Shared.Activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + appPackageName)));
				}
				catch (ActivityNotFoundException ex)
				{
					Shared.Activity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + appPackageName)));
				}
			};
			SetMusicButton();
		}

		/// <summary>
		/// Sets the music button.
		/// </summary>
		private void SetMusicButton()
		{
			if (Music.OFF)
			{
				mSoundText.Text = "Sound OFF";
				mSoundImage.SetImageResource(Resource.Drawable.button_music_off);
			}
			else
			{
				mSoundText.Text = "Sound ON";
				mSoundImage.SetImageResource(Resource.Drawable.button_music_on);
			}
		}
	}
}