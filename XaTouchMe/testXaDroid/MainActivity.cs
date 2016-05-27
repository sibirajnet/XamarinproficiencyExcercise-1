using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Animation;

namespace testXaDroid
{
	[Activity (Label = "testXaDroid", MainLauncher = true)]
	public class MainActivity : Activity, View.IOnTouchListener
	{


		float ShapeMoveX = 0;
		float ShapeMoveY = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			RelativeLayout mainLayout = FindViewById<RelativeLayout> (Resource.Id.maincontainer);
			mainLayout.SetOnTouchListener (this);
		}
		//To Handle OnTouch Event of Main Layout
		public  bool OnTouch (View v, MotionEvent e)
		{
			//Get Main Layout Object
			RelativeLayout mainLayout = FindViewById<RelativeLayout> (Resource.Id.maincontainer);

			switch (e.Action) {
			case MotionEventActions.Down:
		
				//Cretae new Extended Image View
				ShapeImage imageView = new ShapeImage (this);

				//Create Bitmap and canvas to Fill Image
				Bitmap bitMap = Bitmap.CreateBitmap (150, 150, Bitmap.Config.Argb8888);
				Canvas canvas = new Canvas (bitMap);    
				Paint paint = new Paint (); 

				// Fetch the colors
				// This will handle colors from Web API if internet connection available
				ColorPattern FillColors=ColorHandler.FetchColor();

				//Piant from Generated colors
				paint.SetARGB (255, FillColors.R, FillColors.G,FillColors.B);

				//To Generate Random Numbers
				Random randonNum = new Random ();

				//To Handle Circle or Square in Random
				if (randonNum.Next (0, 2) == 0) {
					//Drow Cicrle
					Circle circleView = new Circle ();
					float size=randonNum.Next (10,75);;
					//Fix Circle Size
					circleView.X=size;
					circleView.Y = size;
					circleView.Radius = size;
					canvas.DrawCircle (circleView.X, circleView.Y , circleView.Radius, paint);
					//Tag Imageview as Circle
					imageView.Shape =  circleView;
				} else {
					//Drow Square
					Rectangle rect = new Rectangle ();
			
					float size=randonNum.Next (10,100);
					rect.Height = size;
					rect.Width = size;
					canvas.DrawRect (0, 0, rect.Height , rect.Width , paint);

					//Tag Image as Rectangle
					imageView.Shape = rect;
				}


				//Fill Image
				imageView.SetImageBitmap (bitMap);

				//Adjust Image position
				imageView.SetX (e.GetX () - 50);
				imageView.SetY (e.GetY () - 50);

				//To Handle Touch event on Dynamically created Image view
				imageView.Touch += TouchMeImageViewOnTouch;

				//Finally, Add Image view to Current Layout
				mainLayout.AddView (imageView);

				break;
			case MotionEventActions.Move:
			
				break;
			}
			return true;
		}

		private void TouchMeImageViewOnTouch (object sender, View.TouchEventArgs touchEventArgs)
		{
			ShapeImage imageView = sender as ShapeImage;
			switch (touchEventArgs.Event.Action) {
		
		
			case MotionEventActions.Down:
				// Store Current location to global variables
				ShapeMoveX = touchEventArgs.Event.GetX ();
				ShapeMoveY = touchEventArgs.Event.GetY ();

				imageView.BuildDrawingCache (true);
				Bitmap bitMap = imageView.GetDrawingCache (true);

				ColorPattern FillColors=ColorHandler.FetchColor();

				//Get Canvas
				Canvas canvas = new Canvas (bitMap);    
				Paint paint = new Paint (); 
				paint.SetARGB (255, FillColors.R, FillColors.G, FillColors.B);
                //Check Image Type and Change colors
				if (imageView.Shape.GetType()==typeof(Circle) ) {
					Circle cr = (Circle)imageView.Shape;
					canvas.DrawCircle (cr.X, cr.Y, cr.Radius, paint);

				} else {
					Rectangle Rect = (Rectangle)imageView.Shape;
					canvas.DrawPaint (paint);
					canvas.DrawRect (0, 0, Rect.Height, Rect.Width, paint);

				}	
				imageView.SetImageBitmap (bitMap);
			
				break;
			case MotionEventActions.Move:
				//Move Imageview to new location
				ObjectAnimator animX = ObjectAnimator.OfFloat(imageView, "x", touchEventArgs.Event.RawX+50);
				ObjectAnimator animY = ObjectAnimator.OfFloat(imageView, "y", touchEventArgs.Event.RawY+50);
				AnimatorSet animSetXY = new AnimatorSet();
				animSetXY.PlayTogether(animX, animY);
				animSetXY.Start();
				break;
			}
		}

	}

}


