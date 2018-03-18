using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameFramework.Rules
{
    public class Requirement : RequirementBase
    {
        public Requirement(string name, Predicate<RuleContext> predicate)
        {
            Name = name;
            Predicate = predicate;
        }

        public override string Name { get; }
        public Predicate<RuleContext> Predicate { get; }

        public override bool IsMatch(in RuleContext context)
        {
            return Predicate(context);
        }
    }

    public delegate bool Predicate2<in T1, in T2>(T1 t1, T2 t2);

    public class ParameterRequirement<T> : ParamRequirementBase<T> where T : IRuleParameter
    {
        public override string Name { get; }
        public Predicate2<RuleContext, T> Predicate { get; }
        public T Parameter { get; private set; }

        public override bool IsMatch(in RuleContext context)
        {
            return Predicate(context, Parameter);
        }

        public ParameterRequirement(string name, Predicate2<RuleContext, T> predicate, T parameter = default)
        {
            Name = name;
            Predicate = predicate;
            Parameter = parameter;
        }

        public override ParameterRequirement<T> WithParam(T param)
        {
            Parameter = param;
            return this;
        }
    }

    public class RequirementFactory
    {
        public static Requirement Create(string name, Predicate<RuleContext> predicate) =>
            new Requirement(name, predicate);

        public static ParameterRequirement<T> Create<T>(string name, Predicate2<RuleContext, T> predicate, T parameter = default) where T : IRuleParameter =>
            new ParameterRequirement<T>(name, predicate, parameter);
    }

    public interface IRuleParameter
    {
    }
}