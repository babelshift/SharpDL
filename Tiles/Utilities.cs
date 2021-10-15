using System;
using System.Globalization;
using System.Xml;

namespace SharpDL.Tiles
{
    internal static class Utilities
    {
        public static int TryToParseInt(string valueToParse)
        {
            if(String.IsNullOrEmpty(valueToParse))
            {
                throw new ArgumentNullException("valueToParse");
            }

            int tempValue = 0;
            bool success = int.TryParse(valueToParse, NumberStyles.None, CultureInfo.InvariantCulture, out tempValue);
            if (success)
                return tempValue;
            else
                throw new Exception(String.Format("Failed to parse value {0} to int", valueToParse));
        }

        public static bool TryToParseBool(string valueToParse)
        {
            if (String.IsNullOrEmpty(valueToParse))
            {
                throw new ArgumentNullException("valueToParse");
            }

            bool tempValue = false;
            bool success = bool.TryParse(valueToParse, out tempValue);
            if (success)
                return tempValue;
            else
                throw new Exception(String.Format("Failed to parse value {0} to bool", valueToParse));
        }

        public static float TryToParseFloat(string valueToParse)
        {
            if (String.IsNullOrEmpty(valueToParse))
            {
                throw new ArgumentNullException("valueToParse");
            }

            float tempOpacity = 0f;
            bool success = float.TryParse(valueToParse, NumberStyles.None, CultureInfo.InvariantCulture, out tempOpacity);
            if (success)
                return tempOpacity;
            else
                throw new Exception(String.Format("Failed to parse value {0} to float", valueToParse));
        }

        public static bool IsNull(object argument)
        {
            if (argument == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ThrowExceptionIfIsNullOrEmpty(string argument, string argumentName)
        {
            if (String.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ThrowExceptionIfIsNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ThrowExceptionIfAttributeIsNull(XmlNode node, string nodeName, string attributeName)
        {
            if (Utilities.IsNull(node.Attributes[attributeName]))
            {
                throw new InvalidOperationException(String.Format("'{0}' is missing '{1}' attribute.", nodeName, attributeName));
            }
        }

        public static void ThrowExceptionIfChildNodeIsNull(XmlNode node, string nodeName, string childNodeName)
        {
            if (Utilities.IsNull(node[childNodeName]))
            {
                throw new InvalidOperationException(String.Format("'{0}' is missing '{1}' child node.", nodeName, childNodeName));
            }
        }
    }
}