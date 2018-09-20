using System.Collections.Generic;

namespace PerformanceMarkers.Markers
{
	/// <summary>
	/// A marker that is disabled.
	/// It does absolutely nothing.
	/// This is the marker that is returned if the configuration specifies a marker type of disabled.
	/// </summary>
	public class DisabledMarker : Marker
	{
		public override void Init ()
		{
		}


		public override void Start ()
		{
		}


		public override void Start (string ActivityNameParam)
		{
		}


		public override void End ()
		{
		}


		public override void End (string ActivityNameParam)
		{
		}
		

		public override bool IsDisabled
		{
			get
			{
				return true;
			}
		}


		public override bool IsEnabled
		{
			get
			{
				return false;
			}
		}
		

		public override ActivityPoint[] ActivityPoints
		{
			get
			{
				return null;
			}
		}
	}
}
