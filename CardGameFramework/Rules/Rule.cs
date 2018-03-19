using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CardGameFramework.GameComponent;

namespace CardGameFramework.Rules
{
    public class Rule : IIdentifiable<string>
    {
        public Rule(string identifier = "No name")
        {
            Identifier = Identifier<string>.Of(identifier);
        }

        public ConcurrentBag<IRequirement> Requirements { get; } = new ConcurrentBag<IRequirement>();
        public Identifier<string> Identifier { get; set; }

        public void Validate(RuleContext context)
        {
#if DEBUG
            Trace.WriteLine("Start rule check.", $"Rule: {Identifier}");
            foreach (var requirement in Requirements.AsParallel().Select(req => new {IsMatch = req.IsMatch(in context), Requirement = req }))
            {
                Trace.WriteLine($"Requierment {requirement.Requirement.Name}: {(requirement.IsMatch ? "Pass" : "Error")}", $"Rule: {Identifier}"); // TODO GUI Draw
            }
#else
            var requirement = Requirements.AsParallel().FirstOrDefault(req => !req.IsMatch(in context));
            if (requirement != null)
                throw new RequirementException(requirement.Name);
#endif
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RuleAttribute : Attribute
    {
        public RuleAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
    
}
