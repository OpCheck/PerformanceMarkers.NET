using System;

namespace PerformanceMarkers.Parsers
{
	public class MarkerFailureModeParser
	{
		public static MarkerFailureMode Parse (string MarkerFailureModeParam)
		{
			return (MarkerFailureMode)Enum.Parse(typeof(MarkerFailureMode), MarkerFailureModeParam);
		}
	}
}
