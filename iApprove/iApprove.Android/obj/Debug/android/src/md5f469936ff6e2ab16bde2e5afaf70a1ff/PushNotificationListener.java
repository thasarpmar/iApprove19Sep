package md5f469936ff6e2ab16bde2e5afaf70a1ff;


public class PushNotificationListener
	extends md5214eafb7e7b3b7fcc363a68a6358563f.GcmBroadcastReceiverBase_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("iApprove.Droid.DependencyService.PushNotificationListener, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationListener.class, __md_methods);
	}


	public PushNotificationListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationListener.class)
			mono.android.TypeManager.Activate ("iApprove.Droid.DependencyService.PushNotificationListener, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
