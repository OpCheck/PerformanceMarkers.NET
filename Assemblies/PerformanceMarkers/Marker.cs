using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	/// <summary>
	/// Represents a set of activities and their corresponding lifetimes.
	/// A single marker is called within a component to determine how long an activity takes to complete for the purpose of performance tuning.
	/// </summary>
	public abstract class Marker
	{
		/// <summary>
		/// Creates a marker and ensures that the activity point list is created and ready to use.
		/// </summary>
		public Marker ()
		{
			_ActivityPointList = new List<ActivityPoint>();
		}
	
	
		/// <summary>
		/// Starts the timing for the marker activity.
		/// </summary>
		public void Start ()
		{
			Start(_Name);
		}


		/// <summary>
		/// Starts the timing for the specified activity name inside of the marker activity.
		/// </summary>
		public void Start (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.Start;
			_ActivityPointList.Add(CreatedPoint);
		}


		/// <summary>
		/// Ends the timing for the marker activity.
		/// </summary>
		public void End ()
		{
			End(_Name);
		}


		/// <summary>
		/// Ends the timing for the specified activity name.
		/// </summary>
		public void End (string ActivityNameParam)
		{
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = ActivityNameParam;
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.End;
			_ActivityPointList.Add(CreatedPoint);
		}


		/// <summary>
		/// Sets the name of the marker.
		/// Set this right after creation but before any calls to start and/or end.
		/// Never change the name of the marker after any call to start or end - behavior is undefined otherwise.
		/// </summary>
		public string Name
		{
			set
			{
				_Name = value;
			}
		}
		

		/// <summary>
		/// Set to true to enable the marker, false otherwise.
		/// A marker that is enabled will create activity points for performance timing when the start or end methods are called.
		/// A disabled marker will not create activity points - it is effectively turned off and not useful.
		/// It may be useful to disable markers in certain scenarios - such as production - where collecting and processing metrics may be a performance or memory issue itself.
		/// </summary>
		public bool Enabled
		{
			set
			{
				_Enabled = value;
			}
		}
		
		
		/// <summary>
		/// Returns true if the marker is disabled, false otherwise.
		/// </summary>
		public bool IsDisabled
		{
			get
			{
				return !_Enabled;
			}
		}


		/// <summary>
		/// Returns true if the marker is enabled, false otherwise.
		/// </summary>
		public bool IsEnabled
		{
			get
			{
				return _Enabled;
			}
		}
	

		/// <summary>
		/// Gets the activity points from this marker.
		/// </summary>
		public ActivityPoint[] ActivityPoints
		{
			get
			{
				return _ActivityPointList.ToArray();
			}
		}


		/// <summary>
		/// Gets and sets the failure mode for the performance marker.
		/// This mode applies to both the marker itself and all downstream components that process it.
		/// </summary>
		public MarkerFailureMode FailureMode
		{
			get
			{
				return _MarkerFailureMode;
			}


			set
			{
				_MarkerFailureMode = value;
			}
		}
		

		//
		// INPUT FIELDS.
		//
		protected string _Name;
		protected bool _Enabled;
		protected MarkerFailureMode _MarkerFailureMode;
		
		//
		// OPERATIONAL FIELDS.
		//
		protected List<ActivityPoint> _ActivityPointList;
	}
}
