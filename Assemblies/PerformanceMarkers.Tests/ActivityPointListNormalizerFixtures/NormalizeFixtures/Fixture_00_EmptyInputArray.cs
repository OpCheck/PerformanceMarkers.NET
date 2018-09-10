using System;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityPointListNormalizerFixtures.NormalizeFixtures
{
	[TestFixture]
	public class Fixture_00_EmptyInputArray
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// NORMALIZE THE STACK.
			//
			_Stack = ActivityPointListNormalizer.Normalize(new ActivityPoint[]{});
		}
		
		
		[Test]
		public void Test_Nullity ()
		{
			Assert.IsTrue(_Stack.IsEmpty);
		}
		
		
		private ActivityPointStack _Stack;
	}
}
