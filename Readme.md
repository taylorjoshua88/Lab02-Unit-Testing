# AutomatedTeller

**Author**: Joshua Taylor
**Version**: 1.0.0

## Overview

Automated Teller Machines (ATM) are very important tools in the modern banking
world for performing convenient, simple transactions such as withdrawals and
account balance inquiries. This program aims to simulate the behavior of an ATM
as a way to demonstrate some basic concepts in C#, such as exception handling
and unit testing.

The program should take in user choices and perform the chosen transactionns
while preventing the user's account from overdrafting (falling below $0.00)

## Getting Started

AutomatedTeller targets the .NET Core 2.0 platform. The .NET Core 2.0 SDK can
be downloaded from the following URL for Windows, Linux, and macOS:

https://www.microsoft.com/net/download/

The dotnet CLI utility would then be used to build and run the application:

    cd AutomatedTeller
    dotnet build
    dotnet run

Additionally, users can build, run, and perform unit testing using Visual
Studio 2017 or greater by opening the solution file at the root of this
repository.

## Example

#### Main Menu ####
![Main Menu Screenshot](/assets/menuScreenshot.JPG)
#### Withdrawing Funds ####
![Withdrawing Funds Screenshot](/assets/withdrawScreenshot.JPG)
#### Depositing Funds ####
![Depositing Funds Screenshot](/assets/depositScreenshot.JPG)

## Architecture

AutomatedTeller uses C# and the .NET Core 2.0 platform. The business logic
of the program is spread across three static methods within the Program class.
A unit testing project has been included, AutomatedTellerTest, which uses
the xUnit framework.

### Business Logic

#### WithdrawFunds ####

WithdrawFunds is a method which takes two 128-bit floating point parameters
for the user's current account balance and the amount that the user wishes
to withdraw. This method will not allow any withdrawals that would place
the user into overdraft by throwing an **InvalidOperationException**. Attempting
to withdrawal a negative amount of funds will throw an
**ArgumentOutOfRangeException** (depositing funds should only be done through
the *DepositFunds* method). Upon sucessful operation, the resulting account
balance will be returned.

This method does not destructively modify any of its parameters; therefore,
it is upon the caller to ensure that the new balance is propogated to the
data model.

#### DepositFunds ####

DepositFunds is a method which takes two 128-bit floating point parameters
for the user's current account balance and the amount that the user wishes
to deposit. Attempting to deposit a negative amount of funds will throw an
**ArgumentOutOfRangeException** (withdrawing funds should only be done through
the *WithdrawFunds* method). Upon successful operation, the resulting
account balnace will be returned.

This operation is performed non-destructively; therefore, it
is upon the caller to ensure that the new balance is propogated to the
data model.

#### ViewBalance ####

ViewBalance is used to convert a 128-bit floating point representation of
an account balance to a string using the user's current locale settings.
Some example outputs in the en-US locale are as follows:

    ViewBalance(4.50M)    -> "$4.50"
    ViewBalance(0.3M)     -> "$0.30"
    ViewBalance(1337.42M) -> "$1,337.42"

### Data Model

AutomatedTeller's data model simply consists of a single, 128-bit floating
point variable that is local to the **Program.Main** method. No data
persistence is supported for this application.

### Command Line Interface (CLI)

AutomatedTeller uses a simple finite state machine to determine its desired
operation. A menu is displayed to the console, and the user's input determines
whether to present an interface to withdraw funds or to deposit funds. These
two states make use of their respective business logic methods defined above
and are used to update the data model.

Additionally, an option to simply display the user's account balance is
available in addition to an option to terminate the session.

## Change Log

* 3.21.2018 [Joshua Taylor](mailto:taylor.joshua88@gmail.com) - Initial
release. All tests passing assuming en-US locale.