﻿// <copyright company="RealDimensions Software, LLC" file="LogExtensions.cs">
//   Copyright 2015 - Present RealDimensions Software, LLC
// </copyright>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// 
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace chocolatey.package.verifier
{
    using System;
    using System.Collections.Concurrent;
    using Infrastructure.Logging;

    /// <summary>
    ///   Extensions to help make logging awesome
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        ///   Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly Lazy<ConcurrentDictionary<string, ILog>> Dictionary = new Lazy<ConcurrentDictionary<string, ILog>>(() => new ConcurrentDictionary<string, ILog>());

        // /// <summary>
        // /// Gets the logger for the specified object
        // /// </summary>
        // /// <param name="obj">The obj.</param>
        // /// <returns></returns>
        // public static ILog Log(this object obj)
        // {
        //     string objectName = obj.GetType().FullName;
        //     return Log(objectName);
        // }

        /// <summary>
        ///   Gets the logger for <see cref="T" />.
        /// </summary>
        /// <typeparam name="T">The logger type</typeparam>
        /// <param name="type">The type to get the logger for.</param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log<T>(this T type)
        {
            string objectName = typeof(T).FullName;
            return Log(objectName);
        }

        /// <summary>
        ///   Gets the logger for the specified object name.
        /// </summary>
        /// <param name="objectName">Either use the fully qualified object name or the short. If used with Log&lt;T&gt;() you must use the fully qualified object name"/></param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log(this string objectName)
        {
            return Dictionary.Value.GetOrAdd(objectName, Infrastructure.Logging.Log.GetLoggerFor);
        }
    }
}