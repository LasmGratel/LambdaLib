using System;
using System.Collections.Generic;
using System.Text;
using CardGameFramework.GameComponent;

namespace CardGameFramework.Rules
{
    public class Requirement : RequirementBase
    {
        public Requirement(string identifier, Predicate<RuleContext> predicate)
        {
            Identifier = Identifier<string>.Of(identifier);
            Predicate = predicate;
        }

        public override Identifier<string> Identifier { get; }
        public Predicate<RuleContext> Predicate { get; }

        public override bool IsMatch(in RuleContext context)
        {
            return Predicate(context);
        }
    }

    public delegate bool BiPredicate<in T1, in T2>(T1 t1, T2 t2);

    public class ParameterRequirement<T> : ParamRequirementBase<T> where T : IRuleParameter
    {
        public override Identifier<string> Identifier { get; }
        public BiPredicate<RuleContext, T> Predicate { get; }
        public T Parameter { get; private set; }

        public override bool IsMatch(in RuleContext context)
        {
            return Predicate(context, Parameter);
        }

        public ParameterRequirement(string identifier, BiPredicate<RuleContext, T> predicate, T parameter = default)
        {
            Identifier = Identifier<string>.Of(identifier);
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
        public static Requirement Create(string identifier, Predicate<RuleContext> predicate) =>
            new Requirement(identifier, predicate);

        public static ParameterRequirement<T> Create<T>(string identifier, BiPredicate<RuleContext, T> predicate, T parameter = default) where T : IRuleParameter =>
            new ParameterRequirement<T>(identifier, predicate, parameter);
    }

    public interface IRuleParameter
    {
    }
}