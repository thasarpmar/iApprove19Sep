using System;
using Android.Content;
using Android.Preferences;
using iApprove.Droid.DependencyService;
using iApprove.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(SecureStorage))]
namespace iApprove.Droid.DependencyService
{
    public class SecureStorage : ISecureStorage
    {
        public bool Contains(string key)
        {
			var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
			return pref.Contains(key);
        }

        public void Delete(string key)
        {
			var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
			ISharedPreferencesEditor editor = pref.Edit();
			editor.Remove(key).Commit();
        }

        public byte[] Retrieve(string key)
        {
			return null;
        }

		public string RetrieveString(string key)
		{
			var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
			return pref.GetString(key, "");
		}

		public void Store(string key, byte[] dataBytes)
        {
            
        }

		public void StoreString(string key, string datastr)
		{
			var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
			ISharedPreferencesEditor editor = pref.Edit();
			editor.PutString(key,datastr).Commit();
		}
	}
}