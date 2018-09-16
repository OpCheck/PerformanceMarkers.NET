using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityReportItemTreeFactoryFixtures.CreateReportItemTreeFixtures
{
	[TestFixture]
	public class Fixture_03_SingleNestedActivity
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
			ActivityPointStack CreatedStack = new ActivityPointStack();

			//
			// CREATE THE MARKER START POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "CreatedMarker";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}
			
			//
			// CREATE ACTIVITY P START POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "P";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			//
			// CREATE ACTIVITY P END POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "P";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.End;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			//
			// CREATE THE MARKER END POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "CreatedMarker";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.End;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}
		
			//
			// BUILD THE TREE.
			//
			_ReportItem = ActivityReportItemTreeFactory.CreateReportItemTree(CreatedStack.Flip());
		}
		
		
		[Test]
		public void Test_ReportItem ()
		{
			Assert.IsNotNull(_ReportItem);
			Assert.AreEqual(_ReportItem.ActivityName, "CreatedMarker");

			Assert.IsNotNull(_ReportItem.Duration);
			Assert.AreEqual(_ReportItem.Duration.Value, 3000d);

			Assert.IsNotNull(_ReportItem.StartPoint);
			Assert.AreEqual(_ReportItem.StartPoint.PointType, ActivityPointType.Start);
			Assert.AreEqual(_ReportItem.StartPoint.ActivityName, "CreatedMarker");
			
			Assert.IsNotNull(_ReportItem.EndPoint);
			Assert.AreEqual(_ReportItem.EndPoint.PointType, ActivityPointType.End);
			Assert.AreEqual(_ReportItem.EndPoint.ActivityName, "CreatedMarker");

			Assert.IsNotNull(_ReportItem.ChildReportItems);
			Assert.AreEqual(_ReportItem.ChildReportItems.Length, 1);
		}



		private ActivityReportItem _ReportItem;
	}
}
