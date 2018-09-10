using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.MarkerReportFactoryFixtures.CreateReportForActivityReportItemFixtures
{
	[TestFixture]
	public class Fixture_02_MultipleNestingLevels
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
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemR;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "R";
				CreatedReportItem.Duration = new TimeSpan(0, 0, 1);
				
				CreatedReportItem.StartPoint = new ActivityPoint();
				CreatedReportItem.StartPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.StartPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.StartPoint.PointType = ActivityPointType.Start;
				CreatedReportItem.StartPoint.SequenceNumber = 1;
				
				CreatedReportItem.EndPoint = new ActivityPoint();
				CreatedReportItem.EndPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.EndPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.EndPoint.PointType = ActivityPointType.End;
				CreatedReportItem.EndPoint.SequenceNumber = 2;
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{};
				CreatedReportItemR = CreatedReportItem;
			}
		
			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemQ;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "Q";
				CreatedReportItem.Duration = new TimeSpan(0, 0, 1);
				
				CreatedReportItem.StartPoint = new ActivityPoint();
				CreatedReportItem.StartPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.StartPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.StartPoint.PointType = ActivityPointType.Start;
				CreatedReportItem.StartPoint.SequenceNumber = 1;
				
				CreatedReportItem.EndPoint = new ActivityPoint();
				CreatedReportItem.EndPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.EndPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.EndPoint.PointType = ActivityPointType.End;
				CreatedReportItem.EndPoint.SequenceNumber = 2;
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemR};
				CreatedReportItemQ = CreatedReportItem;
			}

			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemP;
			
			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "P";
				CreatedReportItem.Duration = new TimeSpan(0, 0, 1);
				
				CreatedReportItem.StartPoint = new ActivityPoint();
				CreatedReportItem.StartPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.StartPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.StartPoint.PointType = ActivityPointType.Start;
				CreatedReportItem.StartPoint.SequenceNumber = 1;
				
				CreatedReportItem.EndPoint = new ActivityPoint();
				CreatedReportItem.EndPoint.ActivityName = CreatedReportItem.ActivityName;
				CreatedReportItem.EndPoint.PointDateTime = DateTime.Now;
				CreatedReportItem.EndPoint.PointType = ActivityPointType.End;
				CreatedReportItem.EndPoint.SequenceNumber = 2;
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemQ};
				CreatedReportItemP = CreatedReportItem;
			}

			//
			// CREATE THE PARENT REPORT ITEM.
			//
			ActivityReportItem ParentReportItem = new ActivityReportItem();
			ParentReportItem.ActivityName = "Marker";
			ParentReportItem.Duration = new TimeSpan(0, 0, 1);
			
			ParentReportItem.StartPoint = new ActivityPoint();
			ParentReportItem.StartPoint.ActivityName = "Marker";
			ParentReportItem.StartPoint.PointDateTime = DateTime.Now;
			ParentReportItem.StartPoint.PointType = ActivityPointType.Start;
			ParentReportItem.StartPoint.SequenceNumber = 0;
			
			ParentReportItem.EndPoint = new ActivityPoint();
			ParentReportItem.EndPoint.ActivityName = "Marker";
			ParentReportItem.EndPoint.PointDateTime = DateTime.Now;
			ParentReportItem.EndPoint.PointType = ActivityPointType.End;
			ParentReportItem.EndPoint.SequenceNumber = 3;
			
			ParentReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemP};

			//
			// CREATE THE REPORT.
			//
			MarkerReportFactory CreatedFactory = new MarkerReportFactory();
			_ReportText = CreatedFactory.CreateReportForActivityReportItem(ParentReportItem, 0);
		}
		
		
		[Test]
		public void Test_MarkerReportItem ()
		{
			Console.WriteLine(_ReportText);
		}

		
		private string _ReportText;
	}
}
