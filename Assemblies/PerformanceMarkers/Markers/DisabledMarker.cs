using System.Collections.Generic;

namespace PerformanceMarkers.Markers
{
	/// <summary>
	/// A marker that is disabled by default.
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
