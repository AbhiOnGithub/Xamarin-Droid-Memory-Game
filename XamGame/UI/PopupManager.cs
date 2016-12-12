using System;
using Android.Animation;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamGame.Common;

namespace XamGame.UI
{
	public class PopupManager
	{
		public static void showPopupSettings()
		{
			RelativeLayout popupContainer = (RelativeLayout)Shared.Activity.FindViewById(Resource.Id.popup_container);
			popupContainer.RemoveAllViews();

			// background
			ImageView imageView = new ImageView(Application.Context);
			imageView.SetBackgroundColor(Color.ParseColor("#88555555"));
			imageView.LayoutParameters = new RelativeLayout.LayoutParams(1, 1);
			imageView.Clickable = true;
			popupContainer.AddView(imageView);

			// popup
			PopupSettingsView popupSettingsView = new PopupSettingsView(Application.Context);
			int width = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.popup_settings_width);
			int height = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.popup_settings_height);
			RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(width, height);
			layoutParams.AddRule(LayoutRules.CenterInParent);
			popupContainer.AddView(popupSettingsView, layoutParams);

			// animate all together
			ObjectAnimator scaleXAnimator = ObjectAnimator.OfFloat(popupSettingsView, "scaleX", 0f, 1f);
			ObjectAnimator scaleYAnimator = ObjectAnimator.OfFloat(popupSettingsView, "scaleY", 0f, 1f);
			ObjectAnimator alphaAnimator = ObjectAnimator.OfFloat(imageView, "alpha", 0f, 1f);
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(scaleXAnimator, scaleYAnimator, alphaAnimator);
			animatorSet.SetDuration(500);
			animatorSet.SetInterpolator(new DecelerateInterpolator(2));
			animatorSet.Start();
		}

		public static void showPopupWon(GameState gameState)
		{
			RelativeLayout popupContainer = (RelativeLayout)Shared.Activity.FindViewById(Resource.Id.popup_container);
			popupContainer.RemoveAllViews();

			// popup
			PopupWonView popupWonView = new PopupWonView(Application.Context);
			popupWonView.SetGameState(gameState);
			int width = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.popup_won_width);
			int height = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.popup_won_height);
			RelativeLayout.LayoutParams relparams = new RelativeLayout.LayoutParams(width, height);
			relparams.AddRule(LayoutRules.CenterInParent);
			popupContainer.AddView(popupWonView, relparams);

			// animate all together
			ObjectAnimator scaleXAnimator = ObjectAnimator.OfFloat(popupWonView, "scaleX", 0f, 1f);
			ObjectAnimator scaleYAnimator = ObjectAnimator.OfFloat(popupWonView, "scaleY", 0f, 1f);
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(scaleXAnimator, scaleYAnimator);
			animatorSet.SetDuration(500);
			animatorSet.SetInterpolator(new DecelerateInterpolator(2));
			popupWonView.SetLayerType(LayerType.Hardware, null);
			animatorSet.Start();
		}

		public static void closePopup()
		{
			RelativeLayout popupContainer = (RelativeLayout)Shared.Activity.FindViewById(Resource.Id.popup_container);
			int childCount = popupContainer.ChildCount;
			if (childCount > 0)
			{
				View background = null;
				View viewPopup = null;
				if (childCount == 1)
				{
					viewPopup = popupContainer.GetChildAt(0);
				}
				else {
					background = popupContainer.GetChildAt(0);
					viewPopup = popupContainer.GetChildAt(1);
				}

				AnimatorSet animatorSet = new AnimatorSet();
				ObjectAnimator scaleXAnimator = ObjectAnimator.OfFloat(viewPopup, "scaleX", 0f);
				ObjectAnimator scaleYAnimator = ObjectAnimator.OfFloat(viewPopup, "scaleY", 0f);
				if (childCount > 1)
				{
					ObjectAnimator alphaAnimator = ObjectAnimator.OfFloat(background, "alpha", 0f);
					animatorSet.PlayTogether(scaleXAnimator, scaleYAnimator, alphaAnimator);
				}
				else {
					animatorSet.PlayTogether(scaleXAnimator, scaleYAnimator);
				}
				animatorSet.SetDuration(300);
				animatorSet.SetInterpolator(new AccelerateInterpolator(2));
				animatorSet.AnimationEnd += (sender, e) =>
				{
					popupContainer.RemoveAllViews();
				};
				animatorSet.Start();
			}
		}

		public static bool isShown()
		{
			RelativeLayout popupContainer = (RelativeLayout)Shared.Activity.FindViewById(Resource.Id.popup_container);
			return popupContainer.ChildCount > 0;
		}
	}
}
