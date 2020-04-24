# Introduction

The **SampleBackgroundProcessing** class demonstrates creating a process that will be executed in the background at regular intervals.  

This class inherits from **BackgroundEvent** and has the following required fields and methods:

1. **RunImmediatelyFirstTime**  
   This specifies if this process should run immediately when the web service starts, before calling **CalculateNextRunTime**
2. **CalculateNextRunTime**  
   The function is used to determine when the process should run next.
3. **DoWork**  
   This is the main part of the background process and is called when the process is to be executed.


There is also an option override field:  

1. **RunSynchronously**  
   This returns either true or false to indicate if this process should run synchronously or not, it defaults to false.  
   When **false**, this process will call **CalculateNextRunTime** only after **DoWork** has completed.
   When **true**, this process will call **CalculateNextRunTime** immediately after calling **DoWork** without waiting for it to complete.  This can be used for running a process at regular intervals.