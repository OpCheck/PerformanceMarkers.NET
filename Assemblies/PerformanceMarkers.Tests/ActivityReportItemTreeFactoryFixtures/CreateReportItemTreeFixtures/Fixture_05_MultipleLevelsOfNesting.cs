using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityReportItemTreeFactoryFixtures.CreateReportItemTreeFixtures
{
	[TestFixture]
	public class Fixture_05_MultipleLevelsOfNesting
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
			
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "P";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "Q";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "R";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "R";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.End;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "Q";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(SequenceNumber * TimeIncrement);
				CreatedPoint.PointType = ActivityPointType.End;
				CreatedPoint.SequenceNumber = SequenceNumber++;
				CreatedStack.Push(CreatedPoint);
			}

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
			_MarkerReportItem = ActivityReportItemTreeFactory.CreateReportItemTree(CreatedStack.Flip());
		}
		
		
		[Test]
		public void Test_MarkerReportItem ()
		{
			Assert.IsNotNull(_MarkerReportItem);
			Assert.AreEqual(_MarkerReportItem.ActivityName, "CreatedMarker");

			Assert.IsNotNull(_MarkerReportItem.Duration);
			Assert.AreEqual(_MarkerReportItem.Duration.Value.TotalSeconds, 7d);

			Assert.IsNotNull(_MarkerReportItem.StartPoint);
			Assert.AreEqual(_MarkerReportItem.StartPoint.PointType, ActivityPointType.Start);
			Assert.AreEqual(_MarkerReportItem.StartPoint.ActivityName, "CreatedMarker");
			
			Assert.IsNotNull(_MarkerReportItem.EndPoint);
			Assert.AreEqual(_MarkerReportItem.EndPoint.PointType, ActivityPointType.End);
			Assert.AreEqual(_MarkerReportItem.EndPoint.ActivityName, "CreatedMarker");
		}


		[Test]
		public void Test_MarkerReportItem_ChildReportItems ()
		{
			Assert.IsNotNull(_MarkerReportItem.ChildReportItems);
			Assert.AreEqual(_MarkerReportItem.ChildReportItems.Length, 1);
		}


		[Test]
		public void Test_ActivityP_ReportItem ()
		{
			ActivityReportItem ActivityPReportItem = _MarkerReportItem.ChildReportItems[0];
		
			Assert.AreEqual(ActivityPReportItem.ActivityName, "P");
			Assert.IsNotNull(ActivityPReportItem.ChildReportItems);
			Assert.AreEqual(ActivityPReportItem.ChildReportItems.Length, 1);
		}


		[Test]
		public void Test_ActivityQ_ReportItem ()
		{
			ActivityReportItem ActivityQReportItem = _MarkerReportItem.ChildReportItems[0].ChildReportItems[0];
		
			Assert.AreEqual(ActivityQReportItem.ActivityName, "Q");
			Assert.IsNotNull(ActivityQReportItem.ChildReportItems);
			Assert.AreEqual(ActivityQReportItem.ChildReportItems.Length, 1);
		}


		[Test]
		public void Test_ActivityR_ReportItem ()
		{
			ActivityReportItem ActivityQReportItem = _MarkerReportItem.ChildReportItems[0].ChildReportItems[0].ChildReportItems[0];
		
			Assert.AreEqual(ActivityQReportItem.ActivityName, "R");
			Assert.IsNotNull(ActivityQReportItem.ChildReportItems);
			Assert.AreEqual(ActivityQReportItem.ChildReportItems.Length, 0);
		}


		private ActivityReportItem _MarkerReportItem;
	}
}
