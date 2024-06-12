[![NuGet Version and Downloads count](https://buildstats.info/nuget/TJC.Logging)](https://www.nuget.org/packages/TJC.Logging)

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

#### [ILogger.LogStart())](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Returns a [LogTracker](TJC.Logging/Trackers/LogTracker.cs) that is used to track the duration & status of a process.

#### [ILogger.LogEnd(LogTracker, CompletionStatus, Exception))](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Logs the duration & completion status of a process.

#### [ILogger.LogSucceeded(LogTracker))](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
> Logs the duration & completion status of a successful process.

#### [ILogger.LogFailed(LogTracker, Exception))](TJC.Logging/Extensions/Specialty/LogTrackerExtensions.cs)
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
