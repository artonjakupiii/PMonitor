Process Monitor
===============
This is a simple command line utility that monitors a specified process and kills it if it exceeds a maximum allowed runtime. The program takes three arguments: the process name, the maximum runtime in minutes, and the monitoring frequency in minutes.

Usage
=====
To run the program, open a command prompt or terminal and navigate to the directory containing the ProcessMonitor.exe file. Then, enter the following command:

ProcessMonitor.exe "process name" "maximum run time (minutes)" "check interval (minutes)"

Replace "process name", "maximum run time (minutes)" and "check interval (minutes)" with your desired values.

For example, to monitor a process called "notepad" with a maximum runtime of 10 minutes and a monitoring frequency of 2 minutes, you would run the following command:

ProcessMonitor.exe notepad 10 2

Once the program is running, it will continuously monitor the specified process. If the process exceeds the maximum runtime, the program will kill it and write a corresponding log entry to a file named YYYY-MM-DD.log in the same directory as the ProcessMonitor.exe file. The program will continue monitoring even if no instances of the specified process exist at the moment.

To stop the program, press the q key on your keyboard.

Requirements
============
Windows operating system

.NET Framework 4.5 or later

Testing
=======
This project has been covered by unit tests using NUnit. The tests can be found in the ProcessMonitor.Tests folder.

The tests include:

•TestProcessMonitorWithValidArgs: tests the program with valid command-line arguments, verifying that the monitored process is terminated after the specified maximum run time.

•TestProcessMonitorWithNoArgs: tests the program with no command-line arguments, verifying that the program outputs the correct usage message.

•TestProcessMonitorWithInvalidMaxRunTimeArg: tests the program with an invalid maximum run time argument, verifying that the program outputs the correct error message.

•TestProcessMonitorWithInvalidCheckIntervalArg: tests the program with an invalid check interval argument, verifying that the program outputs the correct error message.

•TestProcessMonitorWithInvalidProcessNameArg: tests the program with an invalid process name argument, verifying that the program outputs the correct error message.


Contributions
=============
Contributions to this project are welcome. If you find a bug or have a suggestion, please create an issue or submit a pull request.
