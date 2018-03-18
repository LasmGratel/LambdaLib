using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace LambdaLib
{
    public abstract class Config<T> where T : Config<T>, new()
    {
        public static T Instance { get; protected set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Update()
        {
            try
            {
                var savePath = SavePath;
                if (File.Exists(savePath))
                {
                    Instance = File.ReadAllText(savePath).JsonDeserialize<T>();
                }
                else
                {
                    Instance = new T();
                    Save();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, "Config");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Save()
        {
            File.WriteAllText(SavePath, Instance.ToJsonString());
        }

        public static string SavePath
        {
            get
            {
                var type = typeof(T);
                var attributes = type.GetCustomAttributes<ConfigAttribute>().ToArray();
                if (attributes.Length == 0) throw new AttributeNotFoundException(nameof(ConfigAttribute));
                return attributes.First().SavePath;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ConfigAttribute : Attribute
    {
        public string SavePath { get; }

        public ConfigAttribute(string savePath)
        {
            SavePath = savePath;
        }
    }

    [Serializable]
    public class AttributeNotFoundException : Exception
    {
        public AttributeNotFoundException()
        {
        }

        public AttributeNotFoundException(string message) : base(message)
        {
        }
    }
}