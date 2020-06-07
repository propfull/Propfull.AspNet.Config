using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Propfull.AspNet.Config.Exceptions
{
    public class InvalidConfigException : Exception
    {
        public new List<ValidationResult> Data { get; }

        public InvalidConfigException(List<ValidationResult> validationResults)
            : base(Constants.InvalidConfigExceptionMessage)
        {
            Data = validationResults;
        }
    }
}