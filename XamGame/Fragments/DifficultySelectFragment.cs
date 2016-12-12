using Android.Animation;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using XamGame.Common;
using XamGame.Events;
using XamGame.Themes;
using XamGame.UI;

namespace XamGame.Fragments
{
	public class DifficultySelectFragment : Android.Support.V4.App.Fragment
	{

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.difficulty_select_fragment, container, false);
			Theme theme = Shared.Engine.GetSelectedTheme();

			DifficultyView difficulty1 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_1);
			difficulty1.setDifficulty(1, Memory.GetHighStars(theme.Id, 1));
			difficulty1.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(1));
			};

			DifficultyView difficulty2 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_2);
			difficulty2.setDifficulty(2, Memory.GetHighStars(theme.Id, 2));
			difficulty2.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(2));
			};

			DifficultyView difficulty3 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_3);
			difficulty3.setDifficulty(3, Memory.GetHighStars(theme.Id, 3));
			difficulty3.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(3));
			};

			DifficultyView difficulty4 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_4);
			difficulty4.setDifficulty(4, Memory.GetHighStars(theme.Id, 4));
			difficulty4.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(4));
			};

			DifficultyView difficulty5 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_5);
			difficulty5.setDifficulty(5, Memory.GetHighStars(theme.Id, 5));
			difficulty5.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(5));
			};

			DifficultyView difficulty6 = (DifficultyView)view.FindViewById(Resource.Id.select_difficulty_6);
			difficulty6.setDifficulty(6, Memory.GetHighStars(theme.Id, 6));
			difficulty6.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new DifficultySelectedEvent(6));
			};

			Animate(difficulty1, difficulty2, difficulty3, difficulty4, difficulty5, difficulty6);

			return view;
		}

		private void Animate(params View[] view)
		{
			AnimatorSet animatorSet = new AnimatorSet();
			AnimatorSet.Builder builder = animatorSet.Play(new AnimatorSet());
			for (int i = 0; i < view.Length; i++)
			{
				ObjectAnimator scaleX = ObjectAnimator.OfFloat(view[i], "scaleX", 0.8f, 1f);
				ObjectAnimator scaleY = ObjectAnimator.OfFloat(view[i], "scaleY", 0.8f, 1f);
				builder.With(scaleX).With(scaleY);
			}
			animatorSet.SetDuration(500);
			animatorSet.SetInterpolator(new BounceInterpolator());
			animatorSet.Start();
		}
	}
}