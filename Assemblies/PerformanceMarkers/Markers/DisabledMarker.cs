using System.Collections.Generic;

namespace PerformanceMarkers.Markers
{
	/// <summary>
	/// A marker that is disabled by default.
	/// </summary>
	public class DisabledMarker : Marker
	{
		/// <summary>
		/// Creates a disabled marker with no name.
		/// The caller must set the name using the name setter.
		/// </summary>
		public DisabledMarker () : base()
		{
			_Enabled = false;
		}


		/// <summary>
		/// Creates a performance marker with the specified name.
		/// A marker name can be the name of anything you want such as a web page or a method.
		/// By defaults, this marker is disabled.
		/// </summary>
		public DisabledMarker (string NameParam) : this()
		{
			_Name = NameParam;
		}
	}
}
