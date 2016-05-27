using System;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Graphics.Drawables;
namespace testXaDroid
{
	public class ShapeImage: ImageView
	{

		public Shape Shape { get; set; }
		public ShapeImage (Context context) : base(context)
		{

		}
	}

}

