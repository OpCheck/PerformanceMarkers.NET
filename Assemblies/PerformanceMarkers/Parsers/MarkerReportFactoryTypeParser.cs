using System;

namespace PerformanceMarkers.Parsers
{
	public class MarkerReportFactoryTypeParser
	{
		public static MarkerReportFactoryType Parse (string FactoryTypeParam)
		{
			return (MarkerReportFactoryType)Enum.Parse(typeof(MarkerReportFactoryType), FactoryTypeParam);
		}
	}
}
