using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using ManiaPlanetSharp.GameBox;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

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

        public static TResult Modify<T, TResult>(this T value, Func<T, TResult> func)
        {
            return (func ?? throw new ArgumentNullException(nameof(func)))(value);
        }



        /*public static string PrintNodeTree(Node root)
        {
            StringBuilder builder = new StringBuilder();

            PrintNodeTreeRecursive(root, builder, 0);

            return builder.ToString();
        }

        private static void PrintNodeTreeRecursive(Node node, StringBuilder builder, int level)
        {
            if (node == null)
            {
                builder.AppendLine(Indent("null", level));
                return;
            }
            Type type = node.GetType();
            builder.AppendLine(Indent((level == 0 ? "# " : "- ") + type.Name, level));

            foreach (var property in type.GetTypeInfo().GetProperties())
            {
                if (typeof(Node).GetTypeInfo().IsAssignableFrom(property.PropertyType) && property.GetAccessors().Any(mi => mi.GetParameters().Length == 0))
                {
                    builder.AppendLine(Indent("- " + property.Name, level + 1));
                    PrintNodeTreeRecursive((Node)property.GetValue(node), builder, level + 2);
                }
                else if (property.Name == "Data" || (property.Name == "Count" && (int)property.GetValue(node) == 0))
                {
                    //Do nothing
                }
                else if (property.Name == "Class")
                {
                    builder.AppendLine(Indent($"- {property.Name}: 0x{(uint)property.GetValue(node):X8} ({KnownClassIds.GetClassName(((uint)property.GetValue(node)) & ~0xFFFU) ?? "Unknown"})", level + 1));
                }
                else
                {
                    string value = FormatPropertyValue(node, property);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        builder.AppendLine(Indent("- " + value, level + 1));
                    }
                }

            }

            if (node.Count > 0 && !(node is GameBoxFile))
            {
                //builder.AppendLine(Indent("--------------", level + 1));
                foreach (Node subnode in node)
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
                    //Debug.WriteLine("null");
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
                try
                {
                    builder.Append(property.GetValue(obj).ToString());
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine($"Error ({ex.GetType().Name})");
                    builder.Append($"Error ({ex.GetType().Name})");
                }
                
            }

            return builder.ToString();
        }*/

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

        public static byte[] ImageToArray(this SixLabors.ImageSharp.Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, new PngEncoder());
                return ms.ToArray();
            }
        }

        public static System.Drawing.Bitmap ToDrawingBitmap(this byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                System.Drawing.Bitmap returnImage = new System.Drawing.Bitmap(ms);
                return returnImage;
            }
        }
    }
}