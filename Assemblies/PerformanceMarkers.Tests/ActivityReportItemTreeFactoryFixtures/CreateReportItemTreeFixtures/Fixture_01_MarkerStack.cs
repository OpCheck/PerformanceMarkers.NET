using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityReportItemTreeFactoryFixtures.CreateReportItemTreeFixtures
{
	[TestFixture]
	public class Fixture_01_MarkerStack
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// FAKE THE TIMES.
			//
			DateTime StartPointDateTime = DateTime.UtcNow;
		
			//
			// CREATE THE STACK.
			//	
			ActivityPointStack CreatedStack = new ActivityPointStack();

			//
			// CREATE THE MARKER END POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "Marker";
				CreatedPoint.PointDateTime = StartPointDateTime.AddSeconds(1);
				CreatedPoint.PointType = ActivityPointType.End;
				CreatedStack.Push(CreatedPoint);
			}

			//
			// CREATE THE MARKER START POINT.
			//
			{
				ActivityPoint CreatedPoint = new ActivityPoint();
				CreatedPoint.ActivityName = "Marker";
				CreatedPoint.PointDateTime = StartPointDateTime;
				CreatedPoint.PointType = ActivityPointType.Start;
				CreatedStack.Push(CreatedPoint);
			}
		
			//
			// BUILD THE TREE.
			//
			_ReportItem = ActivityReportItemTreeFactory.CreateReportItemTree(CreatedStack);
		}
		
		
		[Test]
		public void Test_ReportItem ()
		{
			Assert.IsNotNull(_ReportItem);
			Assert.AreEqual(_ReportItem.ActivityName, "Marker");
		}


		[Test]
		public void Test_ActivityName ()
		{
			Assert.IsNotNull(_ReportItem.ActivityName);
			Assert.AreEqual(_ReportItem.ActivityName, "Marker");
		}


		[Test]
		public void Test_Duration ()
		{
			Assert.IsNotNull(_ReportItem.Duration);
			Assert.AreEqual(_ReportItem.Duration.Value.TotalSeconds, 1d);
		}


		[Test]
		public void Test_StartPoint ()
		{
			Assert.IsNotNull(_ReportItem.StartPoint);
			Assert.AreEqual(_ReportItem.StartPoint.PointType, ActivityPointType.Start);
			Assert.AreEqual(_ReportItem.StartPoint.ActivityName, "Marker");
		}


		[Test]
		public void Test_EndPoint ()
		{
			Assert.IsNotNull(_ReportItem.EndPoint);
			Assert.AreEqual(_ReportItem.EndPoint.PointType, ActivityPointType.End);
			Assert.AreEqual(_ReportItem.EndPoint.ActivityName, "Marker");
		}


		[Test]
		public void Test_ChildReportItems ()
		{
			Assert.IsNotNull(_ReportItem.ChildReportItems);
			Assert.AreEqual(_ReportItem.ChildReportItems.Length, 0);
		}


		private ActivityReportItem _ReportItem;
	}
}
