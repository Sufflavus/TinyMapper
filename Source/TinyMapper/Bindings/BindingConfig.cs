﻿using System;
using System.Collections.Generic;
using Nelibur.ObjectMapper.Core.DataStructures;

namespace Nelibur.ObjectMapper.Bindings
{
    internal class BindingConfig
    {
        private readonly Dictionary<string, string> _bindFields = new Dictionary<string, string>();
        private readonly Dictionary<string, Type> _bindTypes = new Dictionary<string, Type>();
        private readonly HashSet<string> _ignoreFields = new HashSet<string>();

        internal void BindFields(string sourceName, string targetName)
        {
            _bindFields[sourceName] = targetName;
        }

        internal void BindType(string targetName, Type value)
        {
            _bindTypes[targetName] = value;
        }

        internal Option<string> GetBindField(string sourceName)
        {
            string result;
            bool exsist = _bindFields.TryGetValue(sourceName, out result);
            return new Option<string>(result, exsist);
        }

        internal Option<Type> GetBindType(string targetName)
        {
            Type result;
            bool exsist = _bindTypes.TryGetValue(targetName, out result);
            return new Option<Type>(result, exsist);
        }

        internal void IgnoreField(string sourceName)
        {
            _ignoreFields.Add(sourceName);
        }

        internal bool IsIgnoreField(string sourceName)
        {
            if (string.IsNullOrEmpty(sourceName))
            {
                return true;
            }
            return _ignoreFields.Contains(sourceName);
        }
    }
}
