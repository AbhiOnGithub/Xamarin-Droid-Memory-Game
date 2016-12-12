using Android.Animation;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamGame.Common;
using XamGame.Events;
using XamGame.Themes;

namespace XamGame.Fragments
{
	public class ThemeSelectFragment : Android.Support.V4.App.Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.theme_select_fragment, container, false);
			View animals = view.FindViewById(Resource.Id.theme_animals_container);
			View monsters = view.FindViewById(Resource.Id.theme_monsters_container);

			Theme themeAnimals = GameThemes.createAnimalsTheme();
			setStars((ImageView)animals.FindViewById(Resource.Id.theme_animals), themeAnimals, "animals");

			Theme themeMonsters = GameThemes.createMosterTheme();
			setStars((ImageView)monsters.FindViewById(Resource.Id.theme_monsters), themeMonsters, "monsters");

			animals.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new ThemeSelectedEvent(themeAnimals));
			};

			monsters.Click += (sender, e) =>
			{
				Shared.EventBus.Notify(new ThemeSelectedEvent(themeMonsters));
			};

			animateShow(animals);
			animateShow(monsters);

			return view;
		}

		private void animateShow(View view)
		{
			ObjectAnimator animatorScaleX = ObjectAnimator.OfFloat(view, "scaleX", 0.5f, 1f);
			ObjectAnimator animatorScaleY = ObjectAnimator.OfFloat(view, "scaleY", 0.5f, 1f);
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.SetDuration(300);
			animatorSet.PlayTogether(animatorScaleX, animatorScaleY);
			animatorSet.SetInterpolator(new DecelerateInterpolator(2));
			view.SetLayerType(LayerType.Hardware, null);
			animatorSet.Start();
		}

		private void setStars(ImageView imageView, Theme theme, string type)
		{
			int sum = 0;
			for (int difficulty = 1; difficulty <= 6; difficulty++)
			{
				sum += Memory.GetHighStars(theme.Id, difficulty);
			}
			int num = sum / 6;
			if (num != 0)
			{
				string drawableResourceName = $"{type}_theme_star_{num}";
				int drawableResourceId = Application.Context.Resources.GetIdentifier(drawableResourceName, "drawable", Application.Context.PackageName);
				imageView.SetImageResource(drawableResourceId);
			}
		}
	}
}