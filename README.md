# PerformanceMarkers.NET

Integrated performance metrics for building the fastest .NET applications known to man.

## Overview & Motivation

A lightweight and extremely easy-to-use .NET library that helps you:

* precisely expose where the bottlenecks are in your application.
* prove (or disprove) the performance impact of changes applied.  We call this the **Tuning Process**.
* methodically increase your application's performance over time.

How we do this is simple: you insert performance collection statements directly into your code - very similar to how you insert logging statements.  In effect,  measuring performance becomes a function of the application itself.  We call this the **Integrated Performance Metrics Approach**.

We believe that collecting these metrics should be a fundamental part of the software development process due to the enormously positive quality impact it has.

Since the performance metrics collection is integrated into your program - and at some point your program may be deployed to a production environment - we provide ways to disable this library.  This makes it similar to a logging framework where you want it to be less "chatty" in production scenarios.

**PerformanceMarkers.NET** is already used in production environments across several industries (credit card processing, financial analytics) for applications that have to be measured in milliseconds.

## Usage

Let's jump right in and show you some code, with some comments to illustrate what is going on.  We will go into much further detail later.

	//
	// REFERENCE THE PERFORMANCE MARKERS NAMESPACE.
	//
	using PerformanceMarkers;

	//
 	// CREATE A PERFORMANCE MARKER NAMED 'MyProcessName'.
	//
	Marker CreatedMarker = MarkerFactory.StartMarker("ProcessName");

	//
 	// START TIMING AN ACTIVITY NAMED 'Query'.
	//
	CreatedMarker.Start("Query");

	Thread.Sleep(577);

	//
	// STOP TIMING THE ACTIVITY.
	//
	CreatedMarker.End("Query");

	//
	// STOP TIMING THE PROCESS.
	//
	CreatedMarker.End();

	//
	// CREATE A PLAIN-TEXT PERFORMANCE REPORT.
	//
	string PerformanceReport = MarkerReportFactoryProvider.CreateReportFactory().CreateReport(CreatedMarker);

	//
	// VIEW THE REPORT.
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


## Performance Reports

Collecting performance points is the easy part. Creating easy-to-read reports with summaries is what PerformanceMarkers does well.


You can export to the following media types:

* **Text** - a human-readable hierarchy of activities with summaries of how long each activity took to complete.  See above for a sample report.
* **XML** - same as the text format, except you can also easily load it using any XML parser.  This is useful when you may need to write a script that aggregates several performance reports.


## Sequential Performance Tracking

## Concurrent Performance Tracking

## Configuration
