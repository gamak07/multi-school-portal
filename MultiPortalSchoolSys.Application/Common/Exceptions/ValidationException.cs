using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results; // Note: This requires the FluentValidation Nuget package

namespace MultiPortalSchoolSys.Application.Common.Exceptions;

public class ValidationException : Exception
{
    // A dictionary that holds form fields as keys, and arrays of error strings as values
    // Example: "TotalMarks" -> [ "Must be greater than zero", "Cannot exceed 100" ]
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    // This constructor takes errors found by the FluentValidation library and groups them automatically
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(
                failureGroup => failureGroup.Key, 
                failureGroup => failureGroup.ToArray()
            );
    }
}