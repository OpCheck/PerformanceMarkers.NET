using System;
using System.Collections.Generic;

namespace PerformanceMarkers.Markers
{
	/// <summary>
	/// Creates a marker that is enabled by default.
	/// </summary>
	public class EnabledMarker : Marker
	{
		/// <summary>
		/// Initializes the marker by creating a new, empty list of activity points.
		/// </summary>
		public override void Init ()
		{
			_ActivityPointList = new List<ActivityPoint>();
		}
	

		/// <summary>
		/// Starts the "marker activity" using the marker name.
		/// The marker activity is the root activity, and is either the parent or ancestor of all other activities.
		/// Please note that you must set the name property of the marker before call this method.
		/// </summary>
		public override void Start ()
		{
			Start(_Name);
		}


		/// <summary>
		/// Starts the timing for the specified activity name inside of the marker activity.
		/// Activities can be nested.
		/// If an activity is nested, then it will appear as a child in a performance report.
		/// All activities must have a unique name inside the marker.
		/// </summary>
		public override void Start (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.Start;
			_ActivityPointList.Add(CreatedPoint);
		}


		/// <summary>
		/// Ends the marker activity.
		/// </summary>
		public override void End ()
		{
			End(_Name);
		}
	

		/// <summary>
		/// Ends the timing for the activity with the specified name.
		/// </summary>
		public override void End (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.End;
			_ActivityPointList.Add(CreatedPoint);
		}


		/// <summary>
		/// Returns true if this marker is disabled, false otherwise.
		/// Enabled markers will always return false.
		/// </summary>
		public override bool IsDisabled
		{
			get
			{
				return false;
			}
		}


		/// <summary>
		/// Returns true if this marker is enabled, false otherwise.
		/// Enabled markers will always return true.
		/// </summary>
		public override bool IsEnabled
		{
			get
			{
				return true;
			}
		}


		/// <summary>
		/// Gets the set of activity points from the marker.
		/// This is used by the report factories.
		/// </summary>
		public override ActivityPoint[] ActivityPoints
		{
			get
			{
				return _ActivityPointList.ToArray();
			}
		}
	}
}
