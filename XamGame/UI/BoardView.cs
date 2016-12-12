using System;
using System.Collections.Generic;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamGame.Common;
using XamGame.Events;
using XamGame.Models;
using XamGame.Utils;

namespace XamGame.UI
{
	public class BoardView : LinearLayout
	{

		private LinearLayout.LayoutParams mRowLayoutParams = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
		private LinearLayout.LayoutParams mTileLayoutParams;
		private int mScreenWidth;
		private int mScreenHeight;
		private BoardConfiguration mBoardConfiguration;
		private BoardArrangment mBoardArrangment;
		private Dictionary<int, TileView> mViewReference;
		private List<int> flippedUp = new List<int>();
		private bool mLocked = false;
		private int mSize;

		public BoardView(Context context) : this(context, null)
		{
		}

		public BoardView(Context context, IAttributeSet attributeSet) : base(context, attributeSet)
		{
			Orientation = Orientation.Vertical;
			SetGravity(Android.Views.GravityFlags.Center);

			int margin = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.margine_top);
			int padding = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.board_padding);
			mScreenHeight = Application.Context.Resources.DisplayMetrics.HeightPixels - margin - padding * 2;
			mScreenWidth = Application.Context.Resources.DisplayMetrics.WidthPixels - padding * 2 - GameUtility.Px(20);
			mViewReference = new Dictionary<int, TileView>();
			SetClipToPadding(false);
		}


		protected override void OnFinishInflate()
		{
			base.OnFinishInflate();
		}

		public static BoardView fromXml(Context context, ViewGroup parent)
		{
			return (BoardView)LayoutInflater.From(context).Inflate(Resource.Layout.board_view, parent, false);
		}

		public void setBoard(Game game)
		{
			mBoardConfiguration = game.BoardConfiguration;
			mBoardArrangment = game.BoardArrangment;
			// calc prefered tiles in width and height
			int singleMargin = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.card_margin);
			float density = Application.Context.Resources.DisplayMetrics.Density;
			singleMargin = Math.Max((int)(1 * density), (int)(singleMargin - mBoardConfiguration.Difficulty * 2 * density));
			int sumMargin = 0;
			for (int row = 0; row < mBoardConfiguration.NumRows; row++)
			{
				sumMargin += singleMargin * 2;
			}
			int tilesHeight = (mScreenHeight - sumMargin) / mBoardConfiguration.NumRows;
			int tilesWidth = (mScreenWidth - sumMargin) / mBoardConfiguration.NumTilesInRow;
			mSize = Math.Min(tilesHeight, tilesWidth);

			mTileLayoutParams = new LinearLayout.LayoutParams(mSize, mSize);
			mTileLayoutParams.SetMargins(singleMargin, singleMargin, singleMargin, singleMargin);

			// build the ui
			buildBoard();
		}

		/**
		 * Build the board
		 */
		private void buildBoard()
		{

			for (int row = 0; row < mBoardConfiguration.NumRows; row++)
			{
				// add row
				addBoardRow(row);
			}

			SetClipChildren(false);
		}

		private void addBoardRow(int rowNum)
		{

			LinearLayout linearLayout = new LinearLayout(Application.Context);
			linearLayout.Orientation = Orientation.Horizontal;
			linearLayout.SetGravity(GravityFlags.Center);

			for (int tile = 0; tile < mBoardConfiguration.NumTilesInRow; tile++)
			{
				addTile(rowNum * mBoardConfiguration.NumTilesInRow + tile, linearLayout);
			}

			// add to this view
			AddView(linearLayout, mRowLayoutParams);
			linearLayout.SetClipChildren(false);
		}

		private void addTile(int id, ViewGroup parent)
		{
			TileView tileView = TileView.fromXml(Application.Context, parent);
			tileView.LayoutParameters = mTileLayoutParams;
			parent.AddView(tileView);
			parent.SetClipChildren(false);
			mViewReference.Add(id, tileView);

			tileView.setTileImage(mBoardArrangment.getTileBitmap(id, mSize));
			tileView.Click += (sender, e) =>
			{
				if (!mLocked && tileView.isFlippedDown())
				{
					tileView.flipUp();
					flippedUp.Add(id);
					if (flippedUp.Count == 2)
						mLocked = true;
					Shared.EventBus.Notify(new FlipCardEvent(id));
				}
			};

			ObjectAnimator scaleXAnimator = ObjectAnimator.OfFloat(tileView, "scaleX", 0.8f, 1f);
			scaleXAnimator.SetInterpolator(new BounceInterpolator());
			ObjectAnimator scaleYAnimator = ObjectAnimator.OfFloat(tileView, "scaleY", 0.8f, 1f);
			scaleYAnimator.SetInterpolator(new BounceInterpolator());
			AnimatorSet animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(scaleXAnimator, scaleYAnimator);
			animatorSet.SetDuration(500);
			tileView.SetLayerType(LayerType.Hardware, null);
			animatorSet.Start();
		}

		public void flipDownAll()
		{
			foreach (int id in flippedUp)
			{
				mViewReference[id].flipDown();
			}
			flippedUp.Clear();
			mLocked = false;
		}

		public void hideCards(int id1, int id2)
		{
			AnimateHide(mViewReference[id1]);
			AnimateHide(mViewReference[id2]);
			flippedUp.Clear();
			mLocked = false;
		}

		protected void AnimateHide(TileView v)
		{
			ObjectAnimator animator = ObjectAnimator.OfFloat(v, "alpha", 0f);
			animator.AnimationEnd += (sender, e) =>
			{
				v.SetLayerType(LayerType.None, null);
				v.Visibility = ViewStates.Invisible;
			};
			animator.SetDuration(100);
			v.SetLayerType(LayerType.Hardware, null);
			animator.Start();
		}
	}
}