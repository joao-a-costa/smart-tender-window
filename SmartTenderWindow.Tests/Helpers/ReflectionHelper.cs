using System;
using System.Reflection;

namespace SmartTenderWindow.Tests.Helpers
{
    internal static class ReflectionHelper
    {
        internal static object InvokePrivate(object target, string method, params object[] args)
        {
            var mi = target.GetType().GetMethod(method,
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi == null)
                throw new MissingMethodException(target.GetType().Name, method);
            return mi.Invoke(target, args);
        }

        internal static object InvokePrivateStatic(Type type, string method, params object[] args)
        {
            var mi = type.GetMethod(method,
                BindingFlags.NonPublic | BindingFlags.Static);
            if (mi == null)
                throw new MissingMethodException(type.Name, method);
            return mi.Invoke(null, args);
        }

        internal static T GetField<T>(object target, string field)
        {
            var fi = target.GetType().GetField(field,
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi == null)
                throw new MissingFieldException(target.GetType().Name, field);
            return (T)fi.GetValue(target);
        }

        internal static void SetField(object target, string field, object value)
        {
            var fi = target.GetType().GetField(field,
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi == null)
                throw new MissingFieldException(target.GetType().Name, field);
            fi.SetValue(target, value);
        }
    }
}
