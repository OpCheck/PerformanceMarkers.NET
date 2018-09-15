using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;
using PerformanceMarkers.MarkerReportFactories;

namespace PerformanceMarkers.Tests.MarkerReportFactoryFixtures.PlainTextReportFactoryFixtures.CreateReportForActivityReportItemFixtures
{
	[TestFixture]
	public class Fixture_03_MultipleChildrenAndMultipleNestingLevels
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
			ActivityReportItem CreatedReportItemP1;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "P1";
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
				CreatedReportItemP1 = CreatedReportItem;
			}

			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemP2;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "P2";
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
				CreatedReportItemP2 = CreatedReportItem;
			}
		

			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemP3;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "P3";
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
				CreatedReportItemP3 = CreatedReportItem;
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
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemP1, CreatedReportItemP2, CreatedReportItemP3};
				CreatedReportItemP = CreatedReportItem;
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
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemP};
				CreatedReportItemQ = CreatedReportItem;
			}

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
				
				CreatedReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemQ};
				CreatedReportItemR = CreatedReportItem;
			}


			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemT1;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "T";
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
				CreatedReportItemT1 = CreatedReportItem;
			}


			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemT2;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "T2";
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
				CreatedReportItemT2 = CreatedReportItem;
			}


			//
			// CREATE A CHILD REPORT ITEM.
			//
			ActivityReportItem CreatedReportItemT3;

			{
				ActivityReportItem CreatedReportItem = new ActivityReportItem();
				CreatedReportItem.ActivityName = "T2";
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
				CreatedReportItemT3 = CreatedReportItem;
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
			
			ParentReportItem.ChildReportItems = new ActivityReportItem[]{CreatedReportItemT1, CreatedReportItemT2, CreatedReportItemT3};

			//
			// CREATE THE REPORT.
			//
			PlainTextReportFactory CreatedFactory = new PlainTextReportFactory();
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
