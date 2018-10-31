using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using ManiaPlanetSharp.GameBox;

namespace ManiaPlanetSharp.Utilities
{
    public static class Utils
    {
        public static void Repeat(Action action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public static void Repeat<T>(Func<T> action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        public static void PrintRecursive(object data, int level = 0)
        {
            if (level > 10) return;

            Type type = data.GetType();

            if ((new[] { typeof(byte) }).Any(t => t == type))
            {
                Console.WriteLine(data);
            }

            Debug.WriteLine(type.Name);

            if (data == null)
            {
                Debug.Write("null");
            }
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type))
            {
                if (type != typeof(byte[]))
                {
                    object[] items = ((IEnumerable)data).OfType<object>().ToArray();
                    if (items.Length > 100 && type != typeof(GbxNode)) return;
                    foreach (object item in items)
                    {
                        Debug.Write(Indent("", level + 1));
                        PrintRecursive(item, level + 1);
                    }
                }
            }
            else
            {
                foreach (var property in type.GetTypeInfo().GetProperties())
                {
                    Debug.Write(Indent($"{(property.CustomAttributes.Any(cad => cad.AttributeType == typeof(ObsoleteAttribute)) ? "(Obsolete/Unused) " : "")}{property.Name}: ", level + 1));
                    if (property.PropertyType.GetTypeInfo().IsPrimitive || (new[] { typeof(string), typeof(TimeSpan) }).Contains(property.PropertyType))
                    {
                        object value = property.GetValue(data);
                        if (value == null)
                        {
                            Debug.WriteLine("null");
                        }
                        else
                        {
                            switch (value)
                            {
                                case byte b:
                                    Debug.WriteLine(b);
                                    break;
                                case string s:
                                    Debug.WriteLine($"\"{s}\"");
                                    break;
                                default:
                                    Debug.WriteLine(value);
                                    break;
                            }
                        }
                    }
                    else if (property.PropertyType.GetTypeInfo().IsEnum)
                    {
                        Debug.WriteLine(Enum.GetName(property.PropertyType, property.GetValue(data)));
                    }
                    else
                    {
                        try
                        {
                            PrintRecursive(property.GetValue(data), level + 1);
                        }
                        catch
                        {
                            Debug.WriteLine("Error");
                        }
                    }
                }
            }
        }

        public static string Indent(string text, int level, string intendation = "    ")
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                builder.Append(intendation);
            }
            builder.Append(text);
            return builder.ToString();
        }
    }
}
