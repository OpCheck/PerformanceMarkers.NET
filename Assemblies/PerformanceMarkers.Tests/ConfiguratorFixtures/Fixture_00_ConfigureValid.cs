using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using MbUnit.Framework;

using PerformanceMarkers;
using PerformanceMarkers.Configurators;

namespace PerformanceMarkers.Tests.ConfiguratorFixtures
{
	[TestFixture]
	public class Fixture_00_ConfigureValid
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// BUILD THE PATH TO THE CONFIGURATION FILE.
			//
			string FixtureBaseDir = TestFixtureConfig.TestFixturesBaseDir + "\\ConfiguratorFixtures\\Fixture_00_Created";
			string FileName = "markers-config.xml";
			string SourceConfigFilePath = FixtureBaseDir + "\\_TestFiles\\" + FileName;
			_TargetConfigFilePath = FixtureBaseDir + "\\" + FileName;
			
			if (File.Exists(_TargetConfigFilePath))
				File.Delete(_TargetConfigFilePath);
			
			//
			// CREATE THE CONFIGURATION FILE.
			//
			File.Copy(SourceConfigFilePath, _TargetConfigFilePath);

			//
			// WATCH THE FILE.
			//
			XmlConfigurator.Configure(_TargetConfigFilePath);
			
			//
			// WAIT.
			//
			//Thread.Sleep(1000);

			//
			// GET THE CONFIGURATION.
			//
			_MarkerConfig = MarkerConfigProvider.GetMarkerConfig();
		}
		
		
		[Test]
		public void Test()
		{
			Assert.IsNotNull(_MarkerConfig);
			Assert.AreEqual(MarkerType.Disabled, _MarkerConfig.Type);
			Assert.AreEqual(MarkerFailureMode.CompletelyHidden, _MarkerConfig.FailureMode);
			Assert.AreEqual(MarkerReportFactoryType.Xml, _MarkerConfig.ReportFactoryType);
		}


		[TestFixtureTearDown]
		public void TearDown ()
		{
			if (File.Exists(_TargetConfigFilePath))
				File.Delete(_TargetConfigFilePath);
		}
		
		
		private MarkerConfig _MarkerConfig;
		private string _TargetConfigFilePath;
	}
}
