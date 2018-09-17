using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.MarkerReportFactoryFixtures.PlainTextReportFactoryFixtures.CreateReportFixtures
{
	[TestFixture]
	public class Fixture_10_MultipleLevelsOfNesting
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE STARTING POINT TIME.
			//
			DateTime StartPointDateTime = DateTime.Parse("2018-01-01T00:00:00");
			double TimeIncrement = 1d;
			int SequenceNumber = 0;
		
			//
			// CREATE THE STACK.
			//	
			Marker CreatedMarker = MarkerFactory.StartMarker("SectorFactorsWeeklyApp");
			CreatedMarker.Start("MaxSectorFactorIdQuery");
			CreatedMarker.End("MaxSectorFactorIdQuery");
			CreatedMarker.Start("ByMetricDateGetSectorFactorsQuery");
			CreatedMarker.End("ByMetricDateGetSectorFactorsQuery");
			CreatedMarker.Start("CopySectorFactors");
			
			for (int i = 0; i < 1000; i++)
			{
				CreatedMarker.Start("CopySectorFactor");
				CreatedMarker.End("CopySectorFactor");
			}
			
			CreatedMarker.End("CopySectorFactors");
			CreatedMarker.End();

			//
			// CREATE THE REPORT.
			//
			_ReportText = MarkerReportFactoryProvider.CreateReportFactory().CreateReport(CreatedMarker);
		}
		
		
		[Test]
		public void Test_MarkerReportItem ()
		{
			Console.WriteLine(_ReportText);
		}

		
		private string _ReportText;
	}
}
