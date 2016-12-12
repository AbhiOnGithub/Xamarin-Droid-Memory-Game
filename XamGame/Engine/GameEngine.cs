using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Widget;
using XamGame.Common;
using XamGame.Events;
using XamGame.Models;
using XamGame.Themes;
using XamGame.UI;

namespace XamGame.Engine
{
	public class GameEngine : IEventObserver
	{
		private static GameEngine mInstance = null;
		private Game mPlayingGame = null;
		private int mFlippedId = -1;
		private int mToFlip = -1;
		private ScreenController mScreenController;
		private Theme mSelectedTheme;
		private ImageView mBackgroundImage;
		private Handler mHandler;

		private GameEngine()
		{
			mScreenController = ScreenController.getInstance();
			mHandler = new Handler();
		}

		/// <summary>
		/// Gets the active game.
		/// </summary>
		/// <returns>The active game.</returns>
		public Game GetActiveGame()
		{
			return mPlayingGame;
		}

		/// <summary>
		/// Gets the selected theme.
		/// </summary>
		/// <returns>The selected theme.</returns>
		public Theme GetSelectedTheme()
		{
			return mSelectedTheme;
		}

		/// <summary>
		/// Sets the background image view.
		/// </summary>
		/// <param name="backgroundImage">Background image.</param>
		public void SetBackgroundImageView(ImageView backgroundImage)
		{
			mBackgroundImage = backgroundImage;
		}

		/// <summary>
		/// Gets the Signleton instance.
		/// </summary>
		/// <returns>The instance.</returns>
		public static GameEngine getInstance()
		{
			if (mInstance == null)
			{
				mInstance = new GameEngine();
			}
			return mInstance;
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public void start()
		{
			Shared.EventBus.listen(nameof(DifficultySelectedEvent), this);
			Shared.EventBus.listen(nameof(FlipCardEvent), this);
			Shared.EventBus.listen(nameof(StartEvent), this);
			Shared.EventBus.listen(nameof(ThemeSelectedEvent), this);
			Shared.EventBus.listen(nameof(BackGameEvent), this);
			Shared.EventBus.listen(nameof(NextGameEvent), this);
			Shared.EventBus.listen(nameof(ResetBackgroundEvent), this);
		}

		/// <summary>
		/// Stop this instance.
		/// </summary>
		public void stop()
		{
			mPlayingGame = null;
			mBackgroundImage.SetImageDrawable(null);
			mBackgroundImage = null;
			mHandler.RemoveCallbacksAndMessages(null);
			mHandler = null;

			Shared.EventBus.unlisten(nameof(DifficultySelectedEvent), this);
			Shared.EventBus.unlisten(nameof(FlipCardEvent), this);
			Shared.EventBus.unlisten(nameof(StartEvent), this);
			Shared.EventBus.unlisten(nameof(ThemeSelectedEvent), this);
			Shared.EventBus.unlisten(nameof(BackGameEvent), this);
			Shared.EventBus.unlisten(nameof(NextGameEvent), this);
			Shared.EventBus.unlisten(nameof(ResetBackgroundEvent), this);

			mInstance = null;
		}

		public void onEvent(HidePairCardsEvent evt)
		{
		}

		public void onEvent(StartEvent evt)
		{
			mScreenController.openScreen(ScreenController.Screen.THEME_SELECT);
		}

		public void onEvent(GameWonEvent evt)
		{
		}

		public void onEvent(NextGameEvent evt)
		{
			PopupManager.closePopup();
			int difficulty = mPlayingGame.BoardConfiguration.Difficulty;
			if (mPlayingGame.GameState.AchievedStars == 3 && difficulty < 6)
			{
				difficulty++;
			}
			Shared.EventBus.Notify(new DifficultySelectedEvent(difficulty));
		}

		public void onEvent(ResetBackgroundEvent evt)
		{
		}

		public void onEvent(BackGameEvent evt)
		{
			PopupManager.closePopup();
			mScreenController.openScreen(ScreenController.Screen.DIFFICULTY);
		}

		public void onEvent(ThemeSelectedEvent evt)
		{
			mSelectedTheme = evt.theme;
			mScreenController.openScreen(ScreenController.Screen.DIFFICULTY);

		}

		public void onEvent(FlipDownCardsEvent evt)
		{
		}

		public void onEvent(DifficultySelectedEvent evt)
		{
			mFlippedId = -1;
			mPlayingGame = new Game();
			mPlayingGame.BoardConfiguration = new BoardConfiguration(evt.Difficulty);
			mPlayingGame.Theme = mSelectedTheme;
			mToFlip = mPlayingGame.BoardConfiguration.NumTiles;

			// arrange board
			arrangeBoard();

			// start the screen
			mScreenController.openScreen(ScreenController.Screen.GAME);
		}

		public void onEvent(FlipCardEvent evt)
		{
		}

		private void arrangeBoard()
		{
			BoardConfiguration boardConfiguration = mPlayingGame.BoardConfiguration;
			BoardArrangment boardArrangment = new BoardArrangment();

			// build pairs
			// result {0,1,2,...n} // n-number of tiles
			List<int> ids = new List<int>();
			for (int i = 0; i < boardConfiguration.NumTiles; i++)
			{
				ids.Add(i);
			}
			// shuffle
			// result {4,10,2,39,...}
			var rnd = new Random();
			ids = ids.OrderBy(item => rnd.Next()).ToList();

			// place the board
			List<String> tileImageUrls = mPlayingGame.Theme.TileImageUrls;
			tileImageUrls = tileImageUrls.OrderBy(item => rnd.Next()).ToList();

			boardArrangment.pairs = new Dictionary<int, int>();
			boardArrangment.tileUrls = new Dictionary<int, string>();
			int j = 0;

			for (int i = 0; i < ids.Count; i++)
			{
				if (i + 1 < ids.Count)
				{
					// {4,10}, {2,39}, ...
					boardArrangment.pairs.Add(ids[i], ids[i + 1]);
					// {10,4}, {39,2}, ...
					boardArrangment.pairs.Add(ids[i + 1], ids[i]);
					// {4,
					boardArrangment.tileUrls.Add(ids[i], tileImageUrls[j]);
					boardArrangment.tileUrls.Add(ids[i + 1], tileImageUrls[j]);
					i++;
					j++;
				}
			}

			mPlayingGame.BoardArrangment = boardArrangment;
		}

		public Game getActiveGame()
		{
			return mPlayingGame;
		}

		public Theme getSelectedTheme()
		{
			return mSelectedTheme;
		}

		public void setBackgroundImageView(ImageView backgroundImage)
		{
			mBackgroundImage = backgroundImage;
		}
	}
}