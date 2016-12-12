using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamGame.Common;
using XamGame.Events;
using XamGame.Models;
using XamGame.UI;
using XamGame.Utils;

namespace XamGame.Fragments
{
	public class GameFragment : BaseFragment
	{
		private BoardView mBoardView;
		private TextView mTime;
		private ImageView mTimeImage;
		private LinearLayout ads;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			ViewGroup view = (ViewGroup)inflater.Inflate(Resource.Layout.game_fragment, container, false);
			view.SetClipChildren(false);
			((ViewGroup)view.FindViewById(Resource.Id.game_board)).SetClipChildren(false);
			mTime = (TextView)view.FindViewById(Resource.Id.time_bar_text);
			mTimeImage = (ImageView)view.FindViewById(Resource.Id.time_bar_image);
			FontLoader.SetTypeface(new TextView[] { mTime }, FontLoader.Font.GROBOLD);
			mBoardView = BoardView.fromXml(Android.App.Application.Context, view);
			FrameLayout frameLayout = (FrameLayout)view.FindViewById(Resource.Id.game_container);
			frameLayout.AddView(mBoardView);
			frameLayout.SetClipChildren(false);

			// build board
			BuildBoard();
			Shared.EventBus.listen(nameof(FlipDownCardsEvent), this);
			Shared.EventBus.listen(nameof(HidePairCardsEvent), this);
			Shared.EventBus.listen(nameof(GameWonEvent), this);

			return view;
		}

		public override void OnDestroy()
		{
			Shared.EventBus.unlisten(nameof(FlipDownCardsEvent), this);
			Shared.EventBus.unlisten(nameof(HidePairCardsEvent), this);
			Shared.EventBus.unlisten(nameof(GameWonEvent), this);
			base.OnDestroy();
		}

		private void BuildBoard()
		{
			Game game = Shared.Engine.GetActiveGame();
			int time = game.BoardConfiguration.Time;
			setTime(time);
			mBoardView.setBoard(game);

			startClock(time);
		}

		private void setTime(int time)
		{
			int min = time / 60;
			int sec = time - min * 60;
			mTime.Text = $" { min}:{sec}";
		}

		private void startClock(int sec)
		{
			Clock clock = Clock.getInstance();

			IOnTimerCount timer = new TimerCount();


			clock.startTimer(sec * 1000, 1000, timer);
		}

		public override void onEvent(GameWonEvent evt) {
			mTime.Visibility = ViewStates.Gone;
			mTimeImage.Visibility = ViewStates.Gone;
			PopupManager.showPopupWon(evt.GameState);
		}


		public override void onEvent(FlipDownCardsEvent evt) {
			mBoardView.flipDownAll();
		}


		public  override void onEvent(HidePairCardsEvent evt) {
			mBoardView.hideCards(evt.Id1, evt.Id2);
		}

	}
}
