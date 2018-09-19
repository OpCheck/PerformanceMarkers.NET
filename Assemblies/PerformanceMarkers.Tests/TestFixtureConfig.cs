using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceMarkers.Tests
{
	public class TestFixtureConfig
	{
		static TestFixtureConfig ()
		{
			TestFixturesBaseDir = "C:\\Projects\\PerformanceMarkers.NET\\TestFixtures";
		}
		
		
		public static string TestFixturesBaseDir;
	}
}
