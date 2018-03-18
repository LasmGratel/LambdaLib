using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CardGameFramework.Rules
{
    public class RequirementException : Exception
    {
        public RequirementException(string message) : base($"Requirement not match: {message}")
        {
        }
    }
}
