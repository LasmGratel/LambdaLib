using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameFramework.Rules
{
    public class Requires
    {
        #region Example

        private static ParamRequirementBase<AmountParam> _example1 =>
            RequirementFactory.Create<AmountParam>("Amount", (context, param) => context.LastCards.First().Amount == param.Amount);

        private static IRequirement _example2 { get; } = RequirementFactory.Create("Id", context => context.Desk.Identifier == "CNM");

        private void Test()
        {
            Requires._example1.WithParam(new AmountParam(1));
            Requires._example1.WithParam(AmountParam.Of(1));
        }

        #endregion Example

    }

    public class AmountParam : IRuleParameter
    {
        public AmountParam(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }

        public static AmountParam Of(int amount) => new AmountParam(amount);
    }
}