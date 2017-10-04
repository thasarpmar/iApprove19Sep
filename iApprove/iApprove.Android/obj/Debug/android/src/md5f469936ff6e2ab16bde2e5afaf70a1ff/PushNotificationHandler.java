package md5f469936ff6e2ab16bde2e5afaf70a1ff;


public class PushNotificationHandler
	extends md5214eafb7e7b3b7fcc363a68a6358563f.GcmServiceBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("iApprove.Droid.DependencyService.PushNotificationHandler, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushNotificationHandler.class, __md_methods);
	}


	public PushNotificationHandler (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == PushNotificationHandler.class)
			mono.android.TypeManager.Activate ("iApprove.Droid.DependencyService.PushNotificationHandler, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public PushNotificationHandler () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationHandler.class)
			mono.android.TypeManager.Activate ("iApprove.Droid.DependencyService.PushNotificationHandler, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PushNotificationHandler (java.lang.String[] p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushNotificationHandler.class)
			mono.android.TypeManager.Activate ("iApprove.Droid.DependencyService.PushNotificationHandler, iApprove.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String[], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
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
