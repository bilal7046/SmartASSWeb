using System;
using System.Collections.Generic;
using System.ComponentModel;
using Humanizer;

namespace SmartASSWeb.Bll.Core
{
    public static class ObjectToDictionaryHelper
    {
        public static Dictionary<string, object> ToDictionary(this object source, bool includeDateTimeType = true)
        {
            return source.ToDictionary<object>(includeDateTimeType);
        }

        public static Dictionary<string, T> ToDictionary<T>(this object source, bool includeDateTimeType = true)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary, includeDateTimeType);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary, bool includeDateTimeType = true)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value) && IncludeDateTimes(property, includeDateTimeType))
                dictionary.Add(property.Name.Camelize(), (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static bool IncludeDateTimes(PropertyDescriptor property, bool includeDateTimeType)
        {
            if (!includeDateTimeType && (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))) return false;
            return true;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
