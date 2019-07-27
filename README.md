# C# StringTokenizer

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![License MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)
[![codecov](https://codecov.io/gh/FredEkstrand/StringTokenizer/branch/master/graph/badge.svg)](https://codecov.io/gh/FredEkstrand/StringTokenizer)

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/StringTokenizer/Tokenizer_A.png)

# Overview

The StringTokenizer class allows an application to break a string into tokens.
The set of delimiters (the characters that separate tokens) may be specified either at creation time or on a per-token basis.

## Features
* Allow enumeration of a string one token at a time.
* Delimiters property allowing replacement on the fly.
* StringSource property allowing replacing string to be tokenized.
* TokensToArray tokenized the given string and return an array of tokens.
* TokensToList tokenized the given string and return a list of tokens.
* ResetTokenizer sets the StringTokenizer internally to the beginning.
* StringTokenizer implements the IEnumerable and IEnumerator interface.

# Getting started
The souce code and provided DLL is written in C# and targeted for the .Net Framework 4.0 and later. You can download the DLL [here](#).
Their are two options to obtain the StringTokenizer dll.

### Option 1:
You can download the DLL [here](#).

### Option 2:
Download the entire project and compile and add a reference to the dll for use in your project.

# Usage
Once you have the dll add a reference to your Visual Studio project.
Then in your code file add the following to the collection of using statement.
```csharp
using Ekstrand.Text;
```
{ additional code examples please }

# Code Documentation
MSDN-style code documentation can be found [here](http://fredekstrand.github.io/StringTokenizer).


# History
 1.0.0 Initial release into the wild.

# Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are always welcome.

# Contact
Fred Ekstrand
email: fredekstrandgithub@gmail.com

# Licensing

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
