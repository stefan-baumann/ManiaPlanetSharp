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

        //Fairly specific method to this project - could be adapted to a more general usecase but it's good enough for this project
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
            else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type) && type != typeof(GbxNode))
            {
                if (type != typeof(byte[]))
                {
                    object[] items = ((IEnumerable)data).OfType<object>().ToArray();
                    if (items.Length > 100) return;
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

            if (data is GbxNode node)
            {
                foreach (object subNode in node)
                {
                    Debug.Write(Indent("", level + 1));
                    PrintRecursive(subNode, level + 1);
                }
            }
        }

        public static string PrintNodeTree(GbxNode root)
        {
            StringBuilder builder = new StringBuilder();

            PrintNodeTreeRecursive(root, builder, 0);

            return builder.ToString();
        }

        private static void PrintNodeTreeRecursive(GbxNode node, StringBuilder builder, int level)
        {
            if (node == null) return;
            Type type = node.GetType();
            builder.AppendLine(Indent(type.Name, level));

            foreach (var property in type.GetTypeInfo().GetProperties())
            {
                if (typeof(GbxNode).GetTypeInfo().IsAssignableFrom(property.PropertyType) && property.GetAccessors().Any(mi => mi.GetParameters().Length == 0))
                {
                    builder.AppendLine(Indent(property.Name, level + 1));
                    PrintNodeTreeRecursive((GbxNode)property.GetValue(node), builder, level + 2);
                }
                else
                {
                    builder.AppendLine(Indent(FormatPropertyValue(node, property), level + 1));
                }

            }

            if (node.Count > 0)
            {
                builder.AppendLine(Indent("--------------", level + 1));
                foreach (GbxNode subnode in node)
                {
                    PrintNodeTreeRecursive(subnode, builder, level + 1);
                }
            }
        }

        private static string FormatPropertyValue(object obj, PropertyInfo property)
        {
            if (property.GetAccessors().All(mi => mi.GetParameters().Length > 0))
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.Append(property.Name + ": ");

            if (property.PropertyType.GetTypeInfo().IsPrimitive || (new[] { typeof(string), typeof(TimeSpan) }).Contains(property.PropertyType))
            {
                object value = property.GetValue(obj);
                if (value == null)
                {
                    Debug.WriteLine("null");
                }
                else
                {
                    switch (value)
                    {
                        case byte b:
                            builder.Append(b);
                            break;
                        case string s:
                            builder.Append($"\"{s}\"");
                            break;
                        default:
                            builder.Append(value);
                            break;
                    }
                }
            }
            else if (property.PropertyType.GetTypeInfo().IsEnum)
            {
                builder.Append(Enum.GetName(property.PropertyType, property.GetValue(obj)));
            }
            else
            {
                //try
                //{
                //    PrintRecursive(property.GetValue(data), level + 1);
                //}
                //catch
                //{
                //    Debug.WriteLine("Error");
                //}
                builder.Append(obj.ToString());
            }

            return builder.ToString();
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
