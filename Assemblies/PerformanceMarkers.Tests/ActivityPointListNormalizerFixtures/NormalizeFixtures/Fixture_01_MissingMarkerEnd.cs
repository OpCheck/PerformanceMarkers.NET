using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityPointListNormalizerFixtures.NormalizeFixtures
{
	[TestFixture]
	public class Fixture_01_MissingMarkerEnd
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE STACK.
			//	
			List<ActivityPoint> ActivityPointList = new List<ActivityPoint>();

			//
			// CREATE THE MARKER START POINT.
			//
			ActivityPoint CreatedPoint = new ActivityPoint();
			CreatedPoint.ActivityName = "Marker";
			CreatedPoint.PointDateTime = DateTime.UtcNow;
			CreatedPoint.PointType = ActivityPointType.Start;
			ActivityPointList.Add(CreatedPoint);
		
			//
			// NORMALIZE.
			//
			ActivityPointStack CreatedStack = ActivityPointListNormalizer.Normalize(ActivityPointList.ToArray());
			
			//
			// GET THE START AND END POINTS.
			//
			_StartPoint = CreatedStack.Pop();
			_EndPoint = CreatedStack.Pop();
		}
		
		
		[Test]
		public void Test_StartPoint ()
		{
			Assert.AreEqual(ActivityPointType.Start, _StartPoint.PointType);
		}


		[Test]
		public void Test_EndPoint ()
		{
			Assert.AreEqual(ActivityPointType.End, _EndPoint.PointType);
		}
		
		
		private ActivityPoint _StartPoint;
		private ActivityPoint _EndPoint;
	}
}
