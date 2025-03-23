#!/bin/bash

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage" --results-directory:./TestResults

# Generate report
reportgenerator \
  -reports:"./TestResults/**/coverage.cobertura.xml" \
  -targetdir:"./CoverageReport" \
  -reporttypes:Html

echo "Coverage report generated at ./CoverageReport/index.html"
