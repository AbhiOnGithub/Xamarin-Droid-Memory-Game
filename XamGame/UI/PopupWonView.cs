using System;
using Android.Animation;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using XamGame.Common;
using XamGame.Events;
using XamGame.Utils;

namespace XamGame.UI
{
	public class PopupWonView : RelativeLayout
	{

		private TextView mTime;
		private TextView mScore;
		private ImageView mStar1;
		private ImageView mStar2;
		private ImageView mStar3;
		private ImageView mNextButton;
		private ImageView mBackButton;
		private Handler mHandler;

		public PopupWonView(Context context) : this(context, null)
		{
		}

		public PopupWonView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			LayoutInflater.From(context).Inflate(Resource.Layout.popup_won_view, this, true);
			mTime = (TextView)FindViewById(Resource.Id.time_bar_text);
			mScore = (TextView)FindViewById(Resource.Id.score_bar_text);
			mStar1 = (ImageView)FindViewById(Resource.Id.star_1);
			mStar2 = (ImageView)FindViewById(Resource.Id.star_2);
			mStar3 = (ImageView)FindViewById(Resource.Id.star_3);
			mBackButton = (ImageView)FindViewById(Resource.Id.button_back);
			mNextButton = (ImageView)FindViewById(Resource.Id.button_next);
			FontLoader.SetTypeface(new TextView[] { mTime, mScore }, FontLoader.Font.GROBOLD);
			SetBackgroundResource(Resource.Drawable.level_complete);
			mHandler = new Handler();
			mBackButton.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new BackGameEvent());
			};

			mNextButton.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new NextGameEvent());
			};
		}

		public void SetGameState(GameState gameState)
		{
			int min = gameState.RemainedSeconds / 60;
			int sec = gameState.RemainedSeconds - min * 60;
			string time = $" {min} : {sec} ";
			mTime.Text = time;
			mScore.Text = "0";
		}

		private void AnimateStars(int start)
		{
			switch (start)
			{
				case 0:
					mStar1.Visibility = ViewStates.Gone;
					mStar2.Visibility = ViewStates.Gone;
					mStar3.Visibility = ViewStates.Gone;
					break;
				case 1:
					mStar2.Visibility = ViewStates.Gone;
					mStar3.Visibility = ViewStates.Gone;
					mStar1.Alpha = 0f;
					AnimateStar(mStar1, 0);
					break;
				case 2:
					mStar3.Visibility = ViewStates.Gone;
					mStar1.Visibility = ViewStates.Visible;
					mStar1.Alpha = 0f;
					AnimateStar(mStar1, 0);
					mStar2.Visibility = ViewStates.Visible;
					mStar2.Alpha = 0f;
					AnimateStar(mStar2, 600);
					break;
				case 3:
					mStar1.Visibility = ViewStates.Visible;
					mStar1.Alpha = 0f;
					AnimateStar(mStar1, 0);
					mStar2.Visibility = ViewStates.Visible;
					mStar2.Alpha = 0f;
					AnimateStar(mStar2, 600);
					mStar3.Visibility = ViewStates.Visible;
					mStar3.Alpha = 0f;
					AnimateStar(mStar3, 1200);
					break;
				default:
					break;
			}
		}

		private void AnimateStar(View view, int delay)
		{
			ObjectAnimator alpha = ObjectAnimator.OfFloat(view, "alpha", 0, 1f);
			alpha.SetDuration(100);
			ObjectAnimator scaleX = ObjectAnimator.OfFloat(view, "scaleX", 0, 1f);
			ObjectAnimator scaleY = ObjectAnimator.OfFloat(view, "scaleY", 0, 1f);
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(alpha, scaleX, scaleY);
			animatorSet.SetInterpolator(new BounceInterpolator());
			animatorSet.StartDelay = delay;
			animatorSet.SetDuration(600);
			view.SetLayerType(LayerType.Hardware, null);
			animatorSet.Start();

		}
	}
}