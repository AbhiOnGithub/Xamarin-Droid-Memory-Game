using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace XamGame.UI
{
	public class TileView : FrameLayout
	{

		private RelativeLayout mTopImage;
		private ImageView mTileImage;
		private bool mFlippedDown = true;

		public TileView(Context context) : this(context, null)
		{
		}

		public TileView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public static TileView fromXml(Context context, ViewGroup parent)
		{
			return (TileView)LayoutInflater.From(context).Inflate(Resource.Layout.tile_view, parent, false);
		}

		protected override void OnFinishInflate()
		{
			base.OnFinishInflate();
			mTopImage = (RelativeLayout)FindViewById(Resource.Id.image_top);
			mTileImage = (ImageView)FindViewById(Resource.Id.image);
		}

		public void setTileImage(Bitmap bitmap)
		{
			mTileImage.SetImageBitmap(bitmap);
		}

		public void flipUp()
		{
			mFlippedDown = false;
			flip();
		}

		public void flipDown()
		{
			mFlippedDown = true;
			flip();
		}

		private void flip()
		{
			FlipAnimation flipAnimation = new FlipAnimation(mTopImage, mTileImage);
			if (mTopImage.Visibility == ViewStates.Gone)
			{
				flipAnimation.reverse();
			}
			StartAnimation(flipAnimation);
		}

		public bool isFlippedDown()
		{
			return mFlippedDown;
		}

		public class FlipAnimation : Animation
		{
			private Camera camera;

			private View fromView;
			private View toView;

			private float centerX;
			private float centerY;

			private bool forward = true;

			/**
			 * Creates a 3D flip animation between two views.
			 * 
			 * @param fromView
			 *            First view in the transition.
			 * @param toView
			 *            Second view in the transition.
			 */
			public FlipAnimation(View fromView, View toView)
			{
				this.fromView = fromView;
				this.toView = toView;

				Duration = 700;
				FillAfter = false;
				Interpolator = new AccelerateDecelerateInterpolator();
			}


			public void reverse()
			{
				forward = false;
				View switchView = toView;
				toView = fromView;
				fromView = switchView;
			}

			public override void Initialize(int width, int height, int parentWidth, int parentHeight)
			{
				base.Initialize(width, height, parentWidth, parentHeight);
				centerX = width / 2;
				centerY = height / 2;
				camera = new Camera();
			}


			protected override void ApplyTransformation(float interpolatedTime, Transformation t)
			{
				// Angle around the y-axis of the rotation at the given time
				// calculated both in radians and degrees.
				double radians = Math.PI * interpolatedTime;
				float degrees = (float)(180.0 * radians / Math.PI);

				// Once we reach the midpoint in the animation, we need to hide the
				// source view and show the destination view. We also need to change
				// the angle by 180 degrees so that the destination does not come in
				// flipped around
				if (interpolatedTime >= 0.5f)
				{
					degrees -= 180f;
					fromView.Visibility = ViewStates.Gone;
					toView.Visibility = ViewStates.Visible;
				}

				if (forward)
					degrees = -degrees; // determines direction of rotation when
										// flip begins

				Matrix matrix = t.Matrix;
				camera.Save();
				camera.RotateY(degrees);
				camera.GetMatrix(matrix);
				camera.Restore();
				matrix.PreTranslate(-centerX, -centerY);
				matrix.PostTranslate(centerX, centerY);
			}
		}
	}
}