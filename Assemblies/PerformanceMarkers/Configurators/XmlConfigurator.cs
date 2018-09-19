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
			Watch(FilePathParam);
		}
		
		
		public static void Watch (string FilePathParam)
		{
			//
			// SAVE THE FILE PATH FOR FILE SYSTEM EVENT HANDLERS.
			//
			_ConfigFilePath = FilePathParam;

			//
			// CREATE THE FILE SYSTEM WATCHER.
			//
			_ConfigFileWatcher = new FileSystemWatcher();
			_ConfigFileWatcher.Path = Path.GetDirectoryName(FilePathParam);
			_ConfigFileWatcher.Filter = Path.GetFileName(FilePathParam);
			_ConfigFileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
			_ConfigFileWatcher.Changed += new FileSystemEventHandler(RespondToConfigFileCreatedOrChanged);
			_ConfigFileWatcher.Created += new FileSystemEventHandler(RespondToConfigFileCreatedOrChanged);
			_ConfigFileWatcher.EnableRaisingEvents = true;
		}
		
		
		/// <summary>
		/// Delegate method to respond when a configuration file is created or changed.
		/// This method simply reconfigures the framework if the file is OK.
		/// </summary>
		public static void RespondToConfigFileCreatedOrChanged (object Sender, FileSystemEventArgs Args)
		{
			Configure(_ConfigFilePath);
		}


		/// <summary>
		/// Propagates the configuration file to memory.
		/// </summary>
		public static void Configure (string FilePath)
		{
			try
			{
				//
				// LOAD THE XML FILE.
				//
				XmlDocument ConfigDocument = new XmlDocument();
				ConfigDocument.Load(FilePath);
				XmlElement MarkerConfigElement = ConfigDocument.DocumentElement;

				//
				// PARSE THE CONFIGURATION VALUES.
				//
				MarkerConfig CreatedMarkerConfig = new MarkerConfig();
				CreatedMarkerConfig.Type = MarkerTypeParser.Parse(MarkerConfigElement["MarkerType"].InnerText);
				CreatedMarkerConfig.FailureMode = MarkerFailureModeParser.Parse(MarkerConfigElement["FailureMode"].InnerText);
				CreatedMarkerConfig.ReportFactoryType = MarkerReportFactoryTypeParser.Parse(MarkerConfigElement["ReportFactoryType"].InnerText);
				
				//
				// DO THE CONFIGURATION "CUTOVER."
				// THE PROVIDER WILL NOW RETURN THIS REFERENCE FOR THE CONFIG OBJECT.
				//
				MarkerConfigReference.MarkerConfig = CreatedMarkerConfig;
			}
			catch (Exception)
			{
				//
				// DO NOT ALLOW AN INVALID CONFIGURATION TO PROPAGATE.
				// SUPPRESS ALL EXCEPTIONS.
				//
			}
		}
		
		
		private static FileSystemWatcher _ConfigFileWatcher;
		private static string _ConfigFilePath;
	}
}
