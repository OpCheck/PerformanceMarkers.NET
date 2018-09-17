using System;
using System.Collections.Generic;

namespace PerformanceMarkers.Markers
{
	/// <summary>
	/// Creates a marker that is enabled by default.
	/// </summary>
	public class EnabledMarker : Marker
	{
		public override void Init ()
		{
			_ActivityPointList = new List<ActivityPoint>();
		}
	

		public override void Start ()
		{
			Start(_Name);
		}


		/// <summary>
		/// Starts the timing for the specified activity name inside of the marker activity.
		/// </summary>
		public override void Start (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.Start;
			_ActivityPointList.Add(CreatedPoint);
		}


		public override void End ()
		{
			End(_Name);
		}
	

		/// <summary>
		/// Ends the timing for the specified activity name.
		/// </summary>
		public override void End (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.End;
			_ActivityPointList.Add(CreatedPoint);
		}


		public override bool IsDisabled
		{
			get
			{
				return false;
			}
		}


		public override bool IsEnabled
		{
			get
			{
				return true;
			}
		}


		public override ActivityPoint[] ActivityPoints
		{
			get
			{
				return _ActivityPointList.ToArray();
			}
		}
	}
}
