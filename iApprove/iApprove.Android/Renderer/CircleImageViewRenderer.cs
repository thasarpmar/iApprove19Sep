using Xamarin.Forms;
using iApprove.CustomControl;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using System;
using System.Collections.Generic;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CircleImageView), typeof(CircleImageViewRenderer))]
namespace iApprove.Droid.Renderer
{
	public class CircleImageViewRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.SetScaleType(Android.Widget.ImageView.ScaleType.CenterCrop);
			}
		}

		protected override bool DrawChild(Android.Graphics.Canvas canvas, Android.Views.View child, long drawingTime)
		{
			try
			{
				var radius = Math.Min(Width, Height) / 2;
				var strokeWidth = 10;
				radius -= strokeWidth / 2;

				//Create path to clip
				var path = new Path();
				path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
				canvas.Save();
				canvas.ClipPath(path);

				var result = base.DrawChild(canvas, child, drawingTime);

				canvas.Restore();

				// Create path for circle border
				path = new Path();
				path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

				var paint = new Paint();
				paint.AntiAlias = true;
				paint.StrokeWidth = 1;
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color = global::Android.Graphics.Color.White;

				canvas.DrawPath(path, paint);

				//Properly dispose
				paint.Dispose();
				path.Dispose();
				return result;
			}
			catch (Exception ex)
			{

			}
			return base.DrawChild(canvas, child, drawingTime);
		}
	}
}