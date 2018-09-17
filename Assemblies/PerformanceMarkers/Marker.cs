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
		/// Initializes the marker for use.
		/// </summary>
		public abstract void Init ();
	
	
		/// <summary>
		/// Starts the timing for the marker activity.
		/// </summary>
		public abstract void Start ();


		/// <summary>
		/// Starts the timing for the specified activity name inside of the marker activity.
		/// </summary>
		public abstract void Start (string ActivityNameParam);


		/// <summary>
		/// Ends the timing for the marker activity.
		/// </summary>
		public abstract void End ();


		/// <summary>
		/// Ends the timing for the specified activity name.
		/// </summary>
		public abstract void End (string ActivityNameParam);


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
		/// Returns true if the marker is disabled, false otherwise.
		/// </summary>
		public abstract bool IsDisabled
		{
			get;
		}


		/// <summary>
		/// Returns true if the marker is enabled, false otherwise.
		/// </summary>
		public abstract bool IsEnabled
		{
			get;
		}
	

		/// <summary>
		/// Gets the activity points from this marker.
		/// </summary>
		public abstract ActivityPoint[] ActivityPoints
		{
			get;
		}


		/// <summary>
		/// Gets and sets the failure mode for the performance marker.
		/// This mode applies to both the marker itself and all downstream components that process it.
		/// </summary>
		public MarkerFailureMode FailureMode
		{
			get
			{
				return _Config.FailureMode;
			}
		}
		
		
		public MarkerConfig Config
		{
			set
			{
				_Config = value;
			}
		}


		//
		// INPUT FIELDS.
		//
		protected string _Name;
		protected MarkerConfig _Config;
		
		//
		// OPERATIONAL FIELDS.
		//
		protected List<ActivityPoint> _ActivityPointList;
	}
}
