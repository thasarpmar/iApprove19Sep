using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iApprove.CustomControl;
using Xamarin.Forms;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMenuListView), typeof(CustomMenuListViewRenderer))]

namespace iApprove.Droid.Renderer
{
    class CustomMenuListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            if (e.NewElement == null)
                return;

            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Selector = Resources.GetDrawable(Resource.Drawable.lisviewselectbg);

            }
        }
    }
}