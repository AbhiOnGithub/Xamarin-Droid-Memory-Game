using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamGame.Common;
using XamGame.Events;
using XamGame.UI;
using XamGame.Utils;

namespace XamGame.Fragments
{
	public class MenuFragment : Fragment
	{
		private ImageView mTitle;
		private ImageView mStartGameButton;
		private ImageView mStartButtonLights;
		private ImageView mTooltip;
		private ImageView mSettingsGameButton;
		private ImageView mGooglePlayGameButton;

		public class AnimatorAdapter : AnimatorListenerAdapter
		{
			public override void OnAnimationEnd(Animator animation)
			{
				Shared.EventBus.Notify(new StartEvent());
			}
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.menu_fragment, container, false);
			mTitle = (ImageView)view.FindViewById(Resource.Id.title);
			mStartGameButton = (ImageView)view.FindViewById(Resource.Id.start_game_button);
			mSettingsGameButton = (ImageView)view.FindViewById(Resource.Id.settings_game_button);
			mSettingsGameButton.SoundEffectsEnabled = false;
			mSettingsGameButton.Click += (sender, e) =>
			{
				PopupManager.showPopupSettings();
			};
			mGooglePlayGameButton = (ImageView)view.FindViewById(Resource.Id.google_play_button);
			mGooglePlayGameButton.Click += (sender, e) =>
			{
				Toast.MakeText(Shared.Activity, "Leaderboards will be available in the next game updates", ToastLength.Long).Show();
			};
			mStartButtonLights = (ImageView)view.FindViewById(Resource.Id.start_game_button_lights);
			mTooltip = (ImageView)view.FindViewById(Resource.Id.tooltip);
			mStartGameButton.Click += (sender, e) =>
			{
				AnimatorAdapter adapter = new AnimatorAdapter();
				animateAllAssetsOff(adapter);
			};

			startLightsAnimation();
			startTootipAnimation();

			// play background music
			Music.playBackgroundMusic();
			return view;
		}

		private void startTootipAnimation()
		{
			ObjectAnimator scaleY = ObjectAnimator.OfFloat(mTooltip, "scaleY", 0.8f);
			scaleY.SetDuration(200);
			ObjectAnimator scaleYBack = ObjectAnimator.OfFloat(mTooltip, "scaleY", 1f);
			scaleYBack.SetDuration(500);
			scaleYBack.SetInterpolator(new BounceInterpolator());
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.StartDelay = 1000;
			animatorSet.PlaySequentially(scaleY, scaleYBack);
			animatorSet.AnimationEnd += (sender, e) =>
			{
				animatorSet.StartDelay = 2000;
				animatorSet.Start();
			};
			mTooltip.SetLayerType(LayerType.Hardware, null);
			animatorSet.Start();
		}

		private void startLightsAnimation()
		{
			ObjectAnimator animator = ObjectAnimator.OfFloat(mStartButtonLights, "rotation", 0f, 360f);
			animator.SetInterpolator(new AccelerateDecelerateInterpolator());
			animator.SetDuration(6000);
			animator.RepeatCount = ValueAnimator.Infinite;
			mStartButtonLights.SetLayerType(LayerType.Hardware, null);
			animator.Start();
		}

		protected void animateAllAssetsOff(AnimatorListenerAdapter adapter)
		{
			// title
			// 120dp + 50dp + buffer(30dp)
			ObjectAnimator titleAnimator = ObjectAnimator.OfFloat(mTitle, "translationY", GameUtility.Px(-200));
			titleAnimator.SetInterpolator(new AccelerateInterpolator(2));
			titleAnimator.SetDuration(300);

			// lights
			ObjectAnimator lightsAnimatorX = ObjectAnimator.OfFloat(mStartButtonLights, "scaleX", 0f);
			ObjectAnimator lightsAnimatorY = ObjectAnimator.OfFloat(mStartButtonLights, "scaleY", 0f);

			// tooltip
			ObjectAnimator tooltipAnimator = ObjectAnimator.OfFloat(mTooltip, "alpha", 0f);
			tooltipAnimator.SetDuration(100);

			// settings button
			ObjectAnimator settingsAnimator = ObjectAnimator.OfFloat(mSettingsGameButton, "translationY", GameUtility.Px(120));
			settingsAnimator.SetInterpolator(new AccelerateInterpolator(2));
			settingsAnimator.SetDuration(300);

			// google play button
			ObjectAnimator googlePlayAnimator = ObjectAnimator.OfFloat(mGooglePlayGameButton, "translationY", GameUtility.Px(120));
			googlePlayAnimator.SetInterpolator(new AccelerateInterpolator(2));
			googlePlayAnimator.SetDuration(300);

			// start button
			ObjectAnimator startButtonAnimator = ObjectAnimator.OfFloat(mStartGameButton, "translationY", GameUtility.Px(130));
			startButtonAnimator.SetInterpolator(new AccelerateInterpolator(2));
			startButtonAnimator.SetDuration(300);

			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(titleAnimator, lightsAnimatorX, lightsAnimatorY, tooltipAnimator, settingsAnimator, googlePlayAnimator, startButtonAnimator);
			animatorSet.AddListener(adapter);
			animatorSet.Start();
		}
	}
}
