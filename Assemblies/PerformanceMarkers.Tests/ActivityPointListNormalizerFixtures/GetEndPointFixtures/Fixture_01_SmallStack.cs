using System;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityPointListNormalizerFixtures.GetEndPointFixtures
{
	[TestFixture]
	public class Fixture_01_SmallStack
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE STACK.
			//			
			ActivityPointStack CreatedStack = new ActivityPointStack();

			//
			// CREATE THE END POINT.
			//
			_EndPoint = new ActivityPoint();
			_EndPoint.ActivityName = "Marker";
			_EndPoint.PointDateTime = DateTime.UtcNow;
			_EndPoint.PointType = ActivityPointType.End;
			CreatedStack.Push(_EndPoint);

			//
			// CREATE THE START POINT.
			//
			ActivityPoint StartPoint = new ActivityPoint();
			StartPoint.ActivityName = "Marker";
			StartPoint.PointDateTime = DateTime.UtcNow;
			StartPoint.PointType = ActivityPointType.Start;
			CreatedStack.Push(StartPoint);
			
			//
			// GET THE END POINT.
			//
			_EndPoint = ActivityPointListNormalizer.GetEndPoint(StartPoint, CreatedStack);
		}
		
		
		[Test]
		public void Test_Nullity ()
		{
			Assert.IsNotNull(_EndPoint);
		}


		[Test]
		public void Test_ActivityName ()
		{
			Assert.AreEqual("Marker", _EndPoint.ActivityName);
		}


		[Test]
		public void Test_PointType ()
		{
			Assert.AreEqual(ActivityPointType.End, _EndPoint.PointType);
		}
		
		
		private ActivityPoint _EndPoint;
	}
}
