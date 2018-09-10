using System;
using System.Collections.Generic;

using MbUnit.Framework;

using PerformanceMarkers;

namespace PerformanceMarkers.Tests.ActivityReportItemTreeFactoryFixtures.CreateReportItemTreeFixtures
{
	[TestFixture]
	public class Fixture_00_EmptyStack
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE STACK.
			//	
			ActivityPointStack CreatedStack = new ActivityPointStack();

			//
			// BUILD THE TREE.
			//
			_ReportItem = ActivityReportItemTreeFactory.CreateReportItemTree(CreatedStack);
		}
		
		
		[Test]
		public void Test_ReportItem ()
		{
			Assert.IsNull(_ReportItem);
		}
		
		
		private ActivityReportItem _ReportItem;
	}
}
