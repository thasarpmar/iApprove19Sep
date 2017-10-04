using System;
using System.Collections.Generic;
using Android.Speech.Tts;
using iApprove.Droid.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeech_Droid))]
namespace iApprove.Droid.DependencyService
{
	public class TextToSpeech_Droid : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;

		public TextToSpeech_Droid() { }

		public void Speak(string text)
		{
			var ctx = Xamarin.Forms.Forms.Context; //useful for many Android SDK features
			toSpeak = text;
			if (speaker == null)
			{
				speaker = new TextToSpeech(ctx, this);
			}
			else {
				var p = new Dictionary<string, string>();
				speaker.Speak(toSpeak, QueueMode.Flush, p);
			}
		}

		#region IOnInitListener implementation
		public void OnInit(OperationResult status)
		{
			if (status.Equals(OperationResult.Success))
			{
				var p = new Dictionary<string, string>();
				speaker.Speak(toSpeak, QueueMode.Flush, p);
			}
		}
		#endregion
	}
}
