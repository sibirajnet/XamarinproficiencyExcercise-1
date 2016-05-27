using System;
using Android.Net;
namespace testXaDroid
{
	public class ConnectionHandler
	{
		public ConnectionHandler ()
		{

		}
		public  bool IsConnected
		{
			get
			{
				try
				{
					var activeConnection = ConnectivityManager.ActiveNetworkInfo;

					return ((activeConnection != null) && activeConnection.IsConnected);
				}
				catch (Exception e)
				{

					return false;
				}
			}
		}
	}
}

