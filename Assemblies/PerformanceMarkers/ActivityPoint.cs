using System;

namespace PerformanceMarkers
{
	public class ActivityPoint
	{
		/// <summary>
		/// The name of the activity to which this point corresponds.
		/// There may be several points that describe an activity.
		/// </summary>
		public string ActivityName;
		
		
		/// <summary>
		/// The time of this point in the timeline.
		/// This is null if we don't know when this activity ended - we call this an artificial end.
		/// An artificial end is injected into the activity points during the normalization to comply with the rule that every activity must have a start point and an end point.
		/// </summary>
		public DateTime? PointDateTime;
		
		
		/// <summary>
		/// Describes if this is a start or end of an activity.
		/// </summary>
		public ActivityPointType PointType;
		
		
		/// <summary>
		/// The unique, ordered sequence number for this activity point.
		/// This is what we use to tell if an activity is nested inside of another activity.
		/// </summary>
		public int SequenceNumber;
	}
}
