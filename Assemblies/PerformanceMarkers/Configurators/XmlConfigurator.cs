using System;
using System.Xml;
using System.IO;

using PerformanceMarkers.Parsers;

namespace PerformanceMarkers.Configurators
{
	public class XmlConfigurator
	{
		public static void ConfigureAndWatch (string FilePathParam)
		{
			//
			// PROPAGATE THE CONFIGURATION.
			//
			Configure(FilePathParam);
			
			//
			// CREATE THE FILE SYSTEM WATCHER.
			//
			_ConfigFileWatcher = new FileSystemWatcher();
			_ConfigFileWatcher.Path = Path.GetDirectoryName(FilePathParam);
			_ConfigFileWatcher.Filter = Path.GetFileName(FilePathParam);
			_ConfigFileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
			_ConfigFileWatcher.Changed += new FileSystemEventHandler(ResponseToConfigFileChange);
			_ConfigFileWatcher.Created += new FileSystemEventHandler(ResponseToConfigFileChange);
			_ConfigFileWatcher.EnableRaisingEvents = true;
			
			//
			// SAVE THE FILE PATH FOR FILE SYSTEM EVENT HANDLERS.
			//
			_ConfigFilePath = FilePathParam;
		}
		
		
		public static void ResponseToConfigFileChange (object Sender, FileSystemEventArgs Args)
		{
			Configure(_ConfigFilePath);
		}


		public static void Configure (string FilePath)
		{
			//
			// LOAD THE XML FILE.
			//
			XmlDocument ConfigDocument = new XmlDocument();
			ConfigDocument.Load(FilePath);
			XmlElement MarkerConfigElement = ConfigDocument.DocumentElement;
			
			//
			// MARKER TYPE.
			//
			try
			{
				 MarkerConfigReference.Type = MarkerTypeParser.Parse(MarkerConfigElement["MarkerType"].InnerText);
			}
			catch (Exception)
			{
			}
			
			//
			// FAILURE MODE.
			//
			try
			{
				 MarkerConfigReference.FailureMode = MarkerFailureModeParser.Parse(MarkerConfigElement["MarkerFailureMode"].InnerText);
			}
			catch (Exception)
			{
			}

			//
			// REPORT FACTORY TYPE.
			//
			try
			{
				 MarkerConfigReference.ReportFactoryType = MarkerReportFactoryTypeParser.Parse(MarkerConfigElement["ReportFactoryType"].InnerText);
			}
			catch (Exception)
			{
			}
		}
		
		
		private static FileSystemWatcher _ConfigFileWatcher;
		private static string _ConfigFilePath;
	}
}
