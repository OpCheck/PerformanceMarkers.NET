using System;

namespace PerformanceMarkers
{
	public class DurationCalculator
	{
		public static TimeSpan? CalcDuration (ActivityPoint StartPoint, ActivityPoint EndPoint)
		{
			if (EndPoint.PointDateTime == null)
				return null;
				
			return EndPoint.PointDateTime.Value.Subtract(StartPoint.PointDateTime.Value);
		}
	}
}
