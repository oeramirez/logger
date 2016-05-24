# logger
An example of a simple logging library

This is a simple programming exercise about improving a given code. The original file is **Original.cs**, a purported logging library that has many problems. It was redone with good programming practices like separation of responsibilities and unit testing.

Some of the problems that the original file has:

* It doesn't compile. The LogMessage method has a duplicated parameter (bool and string) and a uninitialized local variable int t.
* An unused private bool field _initialized.
* Variable names are not good, we have single letter variables that make it difficult to understand their purpose.
* Variable naming is also inconsistent. private bool LogToDatabase starts with uppercase while all other private variables follow the underscore convention.
* The LogMessage method has too many responsibilities, writing to files, database and console. It needs functional decomposition.
* The configuration parameters are static, so each new instantiation of the class is overriding the previous configuration
* It has logical errors too. The configuration for log targets is not being used at all, it is always logging to all targets.
* Log messages are not well formated, lacking spaces
* messageText is being incorrectly trimmed at the start of the LogMessage method because the string is being treated like a mutable type and it's not. The result of Trim() must be saved in a variable.
* The logging level is being specified as three different booleans introducing unnecessary complexity in the method call, it should be an enum. As a convenience to the developer, we can introduce overloads to the Log method to specify the level without needing an extra parameter.
* The file logging code has several errors. It checks if the file to log to exists, and reads from it when it DOES NOT EXIST, it should be the opposite. It is also trying to read all the text of the existing file to append to it in memory! It's a big performance problem.
* Culture can cause an error in file logging if the date uses forward slashes. Better use a sortable format for dates.
* All the logging code is using ToShortDateString which doesn't print the hour when the error ocurred.
* The error level is not being logged.
* The database logger is concatenating it's input so it's open to SQL injection attacks! It's also keeping connections open.
* There is no error handling anywhere in the project.

