using System;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityPointListNormalizerFixtures.GetEndPointFixtures
{
	[TestFixture]
	public class Fixture_00_EmptyStack
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE START POINT.
			//
			_StartPoint = new ActivityPoint();
			_StartPoint.ActivityName = "DoesNotExist";
			_StartPoint.PointDateTime = DateTime.UtcNow;
			_StartPoint.PointType = ActivityPointType.Start;

			//
			// CREATE THE STACK.
			//			
			_Stack = new ActivityPointStack();
		}
		
		
		[Test]
		public void Test_Nullity ()
		{
			Assert.IsNull(ActivityPointListNormalizer.GetEndPoint(_StartPoint, _Stack));
		}
		
		
		private ActivityPointStack _Stack;
		private ActivityPoint _StartPoint;
	}
}
