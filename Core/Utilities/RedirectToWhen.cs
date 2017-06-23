﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Pixel.FixaBarnkalaset.Core.Utilities
{
    public static class RedirectToWhen
    {
        // ReSharper disable once InconsistentNaming
        private static readonly MethodInfo InternalPreserveStackTraceMethod =
            typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        private static class Cache<T>
        {
            // ReSharper disable StaticFieldInGenericType
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
            // ReSharper restore StaticFieldInGenericType
        }

        [DebuggerNonUserCode]
        public static void InvokeEventOptional<T>(T instance, object @event)
        {
            var type = @event.GetType();
            if (!Cache<T>.Dict.TryGetValue(type, out MethodInfo info))
            {
                // we don't care if state does not consume events
                // they are persisted anyway
                return;
            }
            try
            {
                info.Invoke(instance, new[] { @event });
            }
            catch (TargetInvocationException ex)
            {
                InternalPreserveStackTraceMethod?.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }

        //[DebuggerNonUserCode]
        //public static void InvokeCommand<T>(T instance, object command)
        //{
        //    var type = command.GetType();
        //    if (!Cache<T>.Dict.TryGetValue(type, out MethodInfo info))
        //    {
        //        var s = string.Format("Failed to locate {0}.When({1})", typeof(T).Name, type.Name);
        //        throw new InvalidOperationException(s);
        //    }
        //    try
        //    {
        //        info.Invoke(instance, new[] { command });
        //    }
        //    catch (TargetInvocationException ex)
        //    {
        //        InternalPreserveStackTraceMethod?.Invoke(ex.InnerException, new object[0]);
        //        throw ex.InnerException;
        //    }
        //}
    }
}
