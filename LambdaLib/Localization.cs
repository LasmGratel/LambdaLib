using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LambdaLib
{
    public class Localization
    {
        private const string Separator = ".";
        private static Dictionary<string, string[]> Arrays { get; } = new Dictionary<string, string[]>();
        private static Dictionary<string, string> Values { get; } = new Dictionary<string, string>();

        public static string GetString(string key)
        {
            return Values.ContainsKey(key) ? Values[key] : string.Empty;
        }

        public static string[] GetArray(string key)
        {
            return Arrays.ContainsKey(key) ? Arrays[key] : new string[0];
        }

        public static void Load(string json)
        {
#if TRACE
            var sw = Stopwatch.StartNew();
#endif
            Load(JToken.Parse(json), string.Empty);
#if TRACE
            Trace.WriteLine($"Localization file loaded, used {sw.ElapsedMilliseconds}ms");
#endif
        }

        private static void Load(JToken token, string key)
        {
            switch (token)
            {
                case JObject obj:
                    LoadInternel(obj, key);
                    break;

                case JArray array:
                    LoadInternel(array, key);
                    break;

                case JProperty property:
                    LoadInternel(property, key);
                    break;

                case JValue value:
                    LoadInternel(value, key);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private static void LoadInternel(JValue value, string key)
        {
            var valueStr = value.Value as string ?? throw new NotSupportedException();
            Values.Add(key, valueStr);
            Trace.WriteLine($"Loaded value: [key: {key}, value: {value}]", nameof(Localization));
        }

        private static void LoadInternel(JProperty property, string key)
        {
            key += string.IsNullOrEmpty(key) ? string.Empty : Separator;
            key += property.Name;
            Load(property.Value, key);
        }

        private static void LoadInternel(JArray array, string key)
        {
            var strArray = array.Select(token => ((JValue)token).Value as string ?? throw new NotSupportedException()).ToArray();
            if (Arrays.ContainsKey(key))
            {
                Arrays[key] = Arrays[key].Concat(strArray).ToArray();
            }
            else
            {
                Arrays.Add(key, strArray);
            }
            Trace.WriteLine($"Loaded array: [key: {key}, value: {strArray.Join()}]", nameof(Localization));
        }

        private static void LoadInternel(JObject obj, string key)
        {
            obj.ForEach<JToken>(o => Load(o, key));
        }
    }

    [PreFix("Command")]
    public class CommandName : PrefixedUnlocalizedName<CommandName>
    {
        public CommandName(string key) : base(key)
        {
        }
    }

    public abstract class PrefixedUnlocalizedName<T> : UnlocalizedName
    {
        public PrefixedUnlocalizedName(string key) : base($"{GetPrefix()}.{key}")
        {
        }

        private static string GetPrefix()
        {
            return typeof(T).GetCustomAttribute<PreFixAttribute>().Prefix;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class PreFixAttribute : Attribute
    {
        public string Prefix { get; }

        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public PreFixAttribute(string prefix)
        {
            Prefix = prefix;
        }
    }

    public class UnlocalizedName
    {
        public UnlocalizedName(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public override string ToString()
        {
            return ToLocalizedString();
        }

        public string ToLocalizedString()
        {
            throw new NotImplementedException();
        }

        public static implicit operator UnlocalizedName(string key)
        {
            return new UnlocalizedName(key);
        }
    }
}