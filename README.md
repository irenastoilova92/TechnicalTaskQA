# QA Technical Task - GAN Bulgaria

This repository contains the solution for the two QA tasks - manual and automation. 
## Project Overview
- **Task 1:** Manual test cases for Live Betting (see `ManualTestCases.md`).
- **Task 2:** Automated test framework for Playjack.com.

## Technology Stack
- **Language:** C# (.NET 8.0)
- **Framework:** Playwright for .NET
- **Test Runner:** NUnit
- **Design Pattern:** Page Object Model (POM)

## Rationale
I chose C# for this task as it aligns with my professional experience. I implemented the **Page Object Model** to ensure code reusability and clean separation between test logic and UI elements.

## How to Run
1. **.NET 8 SDK** is installed on machine.
2. Build the project:
   `dotnet build`
3. Install Playwright browsers:
   `pwsh bin/Debug/net8.0/playwright.ps1 install`
4. Run tests:
   `dotnet test`

## Features Covered
- Successful User Registration with dynamic data.
- User Login validation.
- Bonus History verification: Specifically checking for the "Registration - Endless" 5,000 credit bonus in the Account History section.