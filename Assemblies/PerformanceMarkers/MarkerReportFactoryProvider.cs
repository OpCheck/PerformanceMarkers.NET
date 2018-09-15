using System;

namespace PerformanceMarkers
{
	public class MarkerReportFactoryProvider
	{
		public static MarkerReportFactory CreateReportFactory (MarkerReportFactoryType FactoryTypeParam)
		{
			return (MarkerReportFactory)Activator.CreateInstance(null, String.Format("PerformanceMarkers.MarkerReportFactories.{0}ReportFactory", FactoryTypeParam)).Unwrap();
		}


		public static MarkerReportFactory CreateReportFactory ()
		{
			return CreateReportFactory(MarkerReportFactoryType.PlainText);
		}
	}
}
