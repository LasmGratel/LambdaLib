using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LambdaLib;

namespace CardGameFramework.GameController
{
    public class RegexMatcher : IMatcher
    {
        public string Pattern { get; }

        public RegexMatcher(string commandName) => Pattern = Localization.GetString($"{commandName}.Regex");

        public bool IsMatch(string text) => Regex.IsMatch(text, Pattern);
    }

    public class PredicateMatcher : IMatcher
    {
        public Predicate<string> Predicate { get; }

        public PredicateMatcher(Predicate<string> predicate) => Predicate = predicate;

        public bool IsMatch(string text) => Predicate(text);
    }

    public class TextMatcher : IMatcher
    {
        public TextMatcher(string commandName)
        {
            Texts = Localization.GetArray($"{commandName}.Matches");
        }

        public string[] Texts { get; }

        public bool IsMatch(string text)
        {
            return Texts.Any(str => str.Equals(text, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class NVMatcher : IMatcher
    {
        public string[] Verbs { get; }
        public string[] Nouns { get; }
        public NVOrder Order { get; }

        private static readonly string[] Separators = Localization.GetArray("NVMatcher.Separators");
        private static readonly string[] Ignores = Localization.GetArray("NVMatcher.Ignores");

        public NVMatcher(string commandName, NVOrder order)
        {
            Order = order;
            Verbs = Localization.GetArray($"NVMatcher.Verbs.{new CommandName($"{commandName}.Verb")}");
            Nouns = Localization.GetArray($"NVMatcher.Nouns.{new CommandName($"{commandName}.Noun")}");
            Trace.Assert(Verbs.Length != 0 && Nouns.Length != 0);
        }

        public bool IsMatch(string text)
        {
            var rText = RemoveIgnores(text);
            if (Order.HasFlag(NVOrder.NounFirst) && MatchNounFirst(rText)) return true;
            if (Order.HasFlag(NVOrder.VerbFirst) && MatchVerbFirst(rText)) return true;
            return false;
        }

        private bool MatchVerbFirst(string text) => MatchInternal(text, Verbs, Nouns);

        private bool MatchNounFirst(string text) => MatchInternal(text, Nouns, Verbs);

        private bool MatchInternal(string str, string[] firstValids, string[] lastValids)
        {
            var firsts = firstValids.Where(str.StartsWith).ToArray();
            var lasts = lastValids.Where(str.EndsWith).ToArray();
            if (firsts.Length == 0 || lasts.Length == 0) return false;
            return firsts.Any(first => lasts.Any(last => first.Length + last.Length == str.Length));
        }

        private static string RemoveIgnores(string text)
        {
            var sb = new StringBuilder(text);
            foreach (var ignore in Ignores) sb.Replace(ignore, string.Empty);
            foreach (var separator in Separators) sb.Replace(separator, string.Empty);
            return sb.ToString();
        }

        [Flags]
        public enum NVOrder
        {
            VerbFirst,
            NounFirst,
            Any = VerbFirst | NounFirst
        }
    }
}