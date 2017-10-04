using System;
using Android.Graphics;
using iApprove.CustomControl;
using ProjectTemplate.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleLabelView), typeof(CircleLabelViewRenderer))]
namespace ProjectTemplate.iOS.Renderer
{
    public class CircleLabelViewRenderer:LabelRenderer
    {
        public CircleLabelViewRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				//Control.SetScaleType(Android.Widget.ImageView.ScaleType.CenterCrop);
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
