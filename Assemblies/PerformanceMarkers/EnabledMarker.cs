using System.Collections.Generic;

namespace PerformanceMarkers
{
	/// <summary>
	/// Creates a marker that is enabled by default.
	/// </summary>
	public class EnabledMarker : Marker
	{
		/// <summary>
		/// Creates an enabled marker with no name.
		/// The caller must set the name using the name setter.
		/// </summary>
		public EnabledMarker () : base()
		{
			_Enabled = true;
		}
	
	
		/// <summary>
		/// Creates a performance marker with the specified name.
		/// A marker name can be the name of anything you want such as a web page or a method.
		/// </summary>
		public EnabledMarker (string NameParam) : this()
		{
			_Name = NameParam;
		}
	}
}
