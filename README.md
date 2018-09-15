# PerformanceMarkers.NET

Integrated performance metrics for building the fastest .NET applications known to man.

## Overview

There are several methods to measure performance of an application or component.  Most of these methods are external, expensive, and difficult to set up and use.

We prefer the integrated performance metrics approach - where the performance tracking and reporting is a fundamental part of the application itself.  It is highly accurate and much easier to use.

## Performance Reports



## Performance Report Formats

You can export to the following media types:

* Text - a human-readable hierarchy of activities with summaries of how long each activity took to complete.
* XML - same as the text format, except you can also easily load it using any XML parser.

## Usage

	using PerformanceMarkers;

	//
 	// CREATE THE PERFORMANCE MARKER FOR THIS PROCESS.
	//
	Marker CreatedMarker = MarkerFactory.StartMarkerWithTypeAndName("Enabled", "ProcessName");

	//
 	// RUN A DATABASE QUERY OR DO SOME NETWORK I/O.
	//
	CreatedMarker.Start("Query");

	DatabaseQuery.Run();

	CreatedMarker.End("Query");

	//
	// END THE PERFORMANCE MARKER FOR THIS PROCESS.
	//
	CreatedMarker.End();

	//
	// CREATE THE PERFORMANCE REPORT.
	//
	string PerformanceReport = MarkerReportFactoryProvider.CreateReportFactory().CreateReport(CreatedMarker);

	//
	// OUTPUT THE REPORT WHEREVER YOU WANT.  IT'S JUST A STRING.
	//
	Console.WriteLine(PerformanceReport);

## Sample Report

Here is a sample report from a database-intensive financial analytics program: 

	RanksApp: 163,266 ms.
		+ BatchCommit: [total: 21,734 ms, avg: 5,434 ms, count: 4, max: 6,040 ms, min: 5,185 ms]
		MaxRankIdQuery: 153 ms.
		AllCompaniesQuery: 2,098 ms.
		BatchCommit: 5,185 ms.
		BatchCommit: 5,256 ms.
		BatchCommit: 5,252 ms.
		BatchCommit: 6,040 ms.
		LastBatchCommit: 4,164 ms.

Look at this report gives us a summary of how long each batch took to commit in the database, as well as the length of our queries.