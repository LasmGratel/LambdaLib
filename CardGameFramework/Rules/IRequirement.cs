using CardGameFramework.GameComponent;

namespace CardGameFramework.Rules
{
    public interface IRequirement : IIdentifiable<string>
    {
        bool IsMatch(in RuleContext context);
    }

    public abstract class ParamRequirementBase<T> : RequirementBase where T : IRuleParameter
    {
        public abstract ParameterRequirement<T> WithParam(T t);
    }

    public abstract class RequirementBase : IRequirement
    {
        public abstract Identifier<string> Identifier { get; }

        public abstract bool IsMatch(in RuleContext context);

        public static Rule operator |(RequirementBase c1, RequirementBase c2) => new Rule() | c1 | c2;

        public static Rule operator |(Rule c1, RequirementBase c2)
        {
            c1.Requirements.Add(c2);
            return c1;
        }
    }
}