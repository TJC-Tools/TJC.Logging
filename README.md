<a href="https://github.com/TJC-Tools/TJC.Logging/tags">
  <img alt="GitHub Tag" src="https://img.shields.io/github/v/tag/TJC-Tools/TJC.Logging?style=for-the-badge&logo=tag&logoColor=white&labelColor=24292f&color=blue" />
</a>

<a href="https://github.com/TJC-Tools/TJC.Logging/releases/latest">
  <img alt="GitHub Release" src="https://img.shields.io/github/v/release/TJC-Tools/TJC.Logging?style=for-the-badge&logo=starship&logoColor=D9E0EE&labelColor=302D41&&color=green&include_prerelease&sort=semver" />
</a>

<a href="https://www.nuget.org/packages/TJC.Logging">
  <img alt="NuGet Version" src="https://img.shields.io/nuget/v/TJC.Logging?style=for-the-badge&logo=nuget&logoColor=white&labelColor=004880&color=blue" />
</a>

<br/>

<a href="https://www.nuget.org/packages/TJC.Logging">
  <img alt="NuGet Downloads" src="https://img.shields.io/nuget/dt/TJC.Logging?style=for-the-badge&logo=nuget&logoColor=white&labelColor=004880&color=yellow" />
</a>

<a href="https://github.com/TJC-Tools/TJC.Logging">
  <img alt="Repository Size" src="https://img.shields.io/github/repo-size/TJC-Tools/TJC.Logging?style=for-the-badge&logo=files&logoColor=white&labelColor=24292f&color=orange" />
</a>

<br/>

<a href="https://github.com/TJC-Tools/TJC.Logging">
  <img alt="Last commit" src="https://img.shields.io/github/last-commit/TJC-Tools/TJC.Logging?style=for-the-badge&logo=git&logoColor=D9E0EE&labelColor=302D41&color=mediumpurple"/>
</a>

<a href="LICENSE">
  <img alt="License" src="https://img.shields.io/github/license/TJC-Tools/TJC.Logging.svg?style=for-the-badge&logo=balance-scale&logoColor=white&labelColor=333333&color=blueviolet" />
</a>

## Table of Contents
- [Format](#format)
- [Extensions](#extensions)
  - [Trackers](#trackers)
  - [Get & Set](#get--set)
  - [Elapsed Time](#elapsed-time)
  - [Pluralize](#pluralize)

---

## Format
- Timestamp
- Location (`Namespace`, `Type Name`, `Member Name`, `Line Number`)
- Specialty (E.g. `Get`, `Set`, `Tracker`)

---
## Extensions

### Debugging

#### [ILogger.LogMark()](TJC.Logging/Extensions/LogMarkExtension.cs)
> Logs detailed information about the current execution location.

#### [ILogger.LogStep(int step)](TJC.Logging/Extensions/LogStepExtension.cs)
> Logs detailed information about the current execution location, including a step number for debugging purposes.
##### Example
```csharp
var step = 0; 
ILogger.LogStep(ref step);
// Do first step
ILogger.LogStep(ref step);
// Do second step
```

---
### Trackers

#### [ILogger.LogStart()](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Returns a [LogTracker](TJC.Logging/Trackers/LogTracker.cs) that is used to track the duration & status of a process.

#### [ILogger.LogEnd(LogTracker, CompletionStatus, Exception)](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Logs the duration & completion status of a process.

#### [ILogger.LogSuccess(LogTracker)](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Logs the duration & completion status of a successful process.

#### [ILogger.LogFail(LogTracker, Exception)](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Logs the duration & completion status of a failed process.

---
### Get & Set

#### [ILogger.LogGet\<T\>(T obj)](TJC.Logging/Extensions/Specialty/LogGetExtension.cs)
> Logs the value of an object and returns the object.

#### [ILogger.LogSet\<T\>(ref T obj, T value)](TJC.Logging/Extensions/Specialty/LogSetExtension.cs)
> Logs the before and after values of a property and set it when done.

---
### Elapsed Time
[TimeSpan.GetElapsedTime()](TJC.Logging\Extensions\ElapsedTime\ElapsedTimeExtensions.cs)
> Returns a formatted string of the elapsed time.

---
### Pluralize
[string.Pluralize()](TJC.Logging\Extensions\Pluralize\PluralizeExtensions.cs)
> Returns a pluralized string if the count is greater than 1.
