using System;

namespace PerformanceMarkers
{
	public class DurationCalculator
	{
		/// <summary>
		/// Calculates the duration in milliseconds.
		/// </summary>
		public static double? CalcDuration (ActivityPoint StartPoint, ActivityPoint EndPoint)
		{
			if (EndPoint.PointDateTime == null)
				return null;
				
			return EndPoint.PointDateTime.Value.Subtract(StartPoint.PointDateTime.Value).TotalMilliseconds;
		}
	}
}
