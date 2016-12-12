using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace XamGame.UI
{
	public class DifficultyView : LinearLayout
	{
		private ImageView mTitle;

		public DifficultyView(Context context):this(context,null)
		{
		}

		public DifficultyView(Context context, IAttributeSet attrs):base(context, attrs)
		{
			LayoutInflater.From(context).Inflate(Resource.Layout.difficult_view, this, true);
			Orientation = Orientation.Vertical;
			mTitle = (ImageView)FindViewById(Resource.Id.title);
		}

		public void setDifficulty(int difficulty, int stars)
		{
			String titleResource = $"button_difficulty_{difficulty}_star_{stars}";
			int drawableResourceId = Application.Context.Resources.GetIdentifier(titleResource, "drawable", Application.Context.PackageName);
			mTitle.SetImageResource(drawableResourceId);
		}
	}
}