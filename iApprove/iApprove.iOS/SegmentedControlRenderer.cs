﻿using System;
using iApprove;
using ProjectTemplate.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace ProjectTemplate.iOS
{
    public class SegmentedControlRenderer: ViewRenderer<SegmentedControl, UISegmentedControl>
    {
		UISegmentedControl nativeControl;

		protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				// Instantiate the native control and assign it to the Control property with
				// the SetNativeControl method

				nativeControl = new UISegmentedControl();

				for (var i = 0; i < Element.Children.Count; i++)
				{
					nativeControl.InsertSegment(Element.Children[i].Text, i, false);
				}

				nativeControl.Enabled = Element.IsEnabled;
				nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Color.Gray.ToUIColor();
				SetSelectedTextColor();

				nativeControl.SelectedSegment = Element.SelectedSegment;

				SetNativeControl(nativeControl);
			}

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources

				if (nativeControl != null)
					nativeControl.ValueChanged -= NativeControl_ValueChanged;
			}

			if (e.NewElement != null)
			{
				// Configure the control and subscribe to event handlers

				nativeControl.ValueChanged += NativeControl_ValueChanged;
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "Renderer":
					Element.ValueChanged?.Invoke(Element, null);
					break;
				case "SelectedSegment":
					nativeControl.SelectedSegment = Element.SelectedSegment;
					Element.ValueChanged?.Invoke(Element, null);
					break;
				case "TintColor":
					nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Color.Gray.ToUIColor();
					break;
				case "IsEnabled":
					nativeControl.Enabled = Element.IsEnabled;
					nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Color.Gray.ToUIColor();
					break;
				case "SelectedTextColor":
					SetSelectedTextColor();
					break;

			}

		}

		void SetSelectedTextColor()
		{
			var attr = new UITextAttributes();
			attr.TextColor = Element.SelectedTextColor.ToUIColor();
			nativeControl.SetTitleTextAttributes(attr, UIControlState.Selected);
		}

		void NativeControl_ValueChanged(object sender, EventArgs e)
		{
			Element.SelectedSegment = (int)nativeControl.SelectedSegment;
		}

		protected override void Dispose(bool disposing)
		{
			if (nativeControl != null)
			{
				nativeControl.ValueChanged -= NativeControl_ValueChanged;
				nativeControl.Dispose();
				nativeControl = null;
			}

			try
			{
				base.Dispose(disposing);
			}
			catch (Exception ex)
			{
				return;
			}
		}

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init()
		{
			var temp = DateTime.Now;
		}
	}

}
