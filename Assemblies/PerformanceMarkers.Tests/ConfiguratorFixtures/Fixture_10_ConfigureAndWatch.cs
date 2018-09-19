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
	public class Fixture_10_ConfigureAndWatch
	{
		[TestFixtureSetUp]
		public void SetUp ()
		{
			//
			// CREATE THE START CONFIGURATION.
			//
			_StartConfig = new MarkerConfig();
			_StartConfig.Type = MarkerType.Enabled;
			_StartConfig.FailureMode = MarkerFailureMode.HighlyVisible;
			_StartConfig.ReportFactoryType = MarkerReportFactoryType.PlainText;
			MarkerConfigReference.MarkerConfig = _StartConfig;
		
			//
			// BUILD THE PATH TO THE CONFIGURATION FILE.
			//
			string FixtureBaseDir = TestFixtureConfig.TestFixturesBaseDir + "\\ConfiguratorFixtures\\Fixture_10_ConfigureAndWatch";
			string FileName = "markers-config.xml";
			string SourceConfigFilePath = FixtureBaseDir + "\\_TestFiles\\" + FileName;
			_TargetConfigFilePath = FixtureBaseDir + "\\" + FileName;
			
			//
			// CLEAN UP FROM ANY PREVIOUS FAILURES.
			//
			if (File.Exists(_TargetConfigFilePath))
				File.Delete(_TargetConfigFilePath);
			
			//
			// WATCH THE FILE.
			//
			XmlConfigurator.ConfigureAndWatch(_TargetConfigFilePath);

			//
			// CREATE THE CONFIGURATION FILE THAT THE XML CONFIGURATOR SHOULD BE WATCHING.
			//
			File.Copy(SourceConfigFilePath, _TargetConfigFilePath);
			
			//
			// WAIT FOR THE CONFIGURATION TO PROPAGATE.
			//
			Thread.Sleep(20);

			//
			// GET THE CONFIGURATION FROM THE PROVIDER.
			//
			_EndConfig = MarkerConfigProvider.GetMarkerConfig();
		}
		
		
		[Test]
		public void Test()
		{
			Assert.AreNotEqual(_StartConfig.Type, _EndConfig.Type);
			Assert.AreNotEqual(_StartConfig.FailureMode, _EndConfig.FailureMode);
			Assert.AreNotEqual(_StartConfig.ReportFactoryType, _EndConfig.ReportFactoryType);
		
			Assert.AreEqual(MarkerType.Disabled, _EndConfig.Type);
			Assert.AreEqual(MarkerFailureMode.CompletelyHidden, _EndConfig.FailureMode);
			Assert.AreEqual(MarkerReportFactoryType.Xml, _EndConfig.ReportFactoryType);
		}


		[TestFixtureTearDown]
		public void TearDown ()
		{
			if (File.Exists(_TargetConfigFilePath))
				File.Delete(_TargetConfigFilePath);
		}
		
		
		private string _TargetConfigFilePath;
		private MarkerConfig _StartConfig;
		private MarkerConfig _EndConfig;
	}
}
