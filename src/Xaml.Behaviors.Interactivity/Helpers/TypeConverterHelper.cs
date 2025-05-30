﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Transformation;

namespace Avalonia.Xaml.Interactivity;

/// <summary>
/// A helper class that enables converting values specified in markup (strings) to their object representation.
/// </summary>
[RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
internal static class TypeConverterHelper
{
    /// <summary>
    /// Converts string representation of a value to its object representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <returns>Object representation of the string value.</returns>
    /// <exception cref="ArgumentNullException">destinationType cannot be null.</exception>
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "DynamicallyAccessedMembers handles most of the problems.")]
    public static object? Convert(string value, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type destinationType)
    {
        if (destinationType is null)
        {
            throw new ArgumentNullException(nameof(destinationType));
        }

        var destinationTypeFullName = destinationType.FullName;
        if (destinationTypeFullName is null)
        {
            return null;
        }

        var scope = GetScope(destinationTypeFullName);

        // Value types in the "System" namespace must be special cased due to a bug in the xaml compiler
        if (string.Equals(scope, "System", StringComparison.Ordinal))
        {
            if (string.Equals(destinationTypeFullName, typeof(string).FullName, StringComparison.Ordinal))
            {
                return value;
            }

            if (string.Equals(destinationTypeFullName, typeof(bool).FullName, StringComparison.Ordinal))
            {
                return bool.Parse(value);
            }

            if (string.Equals(destinationTypeFullName, typeof(int).FullName, StringComparison.Ordinal))
            {
                return int.Parse(value, CultureInfo.InvariantCulture);
            }

            if (string.Equals(destinationTypeFullName, typeof(double).FullName, StringComparison.Ordinal))
            {
                return double.Parse(value, CultureInfo.InvariantCulture);
            }
        }

        try
        {
            if (destinationType.BaseType == typeof(Enum))
                return Enum.Parse(destinationType, value);

            if (destinationType.GetInterfaces().Any(t => t == typeof(IConvertible)))
            {
                return (value as IConvertible).ToType(destinationType, CultureInfo.InvariantCulture);
            }

            var converter = TypeDescriptor.GetConverter(destinationType);
            return converter.ConvertFromInvariantString(value);
        }
        catch (ArgumentException)
        {
            // not an enum
        }
        catch (InvalidCastException)
        {
            // not able to convert to anything
        }
        catch (NotSupportedException)
        {
            // not able to convert from string
            try
            {
                var parseResult = ParseHelper.InvokeParse(value, destinationType);
                if (parseResult is not null)
                {
                    return parseResult;
                }
            }
            catch (Exception)
            {
                // not able to parse
                return null;
            }
        }

        return null;
    }

    private static string GetScope(string name)
    {
        var indexOfLastPeriod = name.LastIndexOf('.');
#if !NET6_0_OR_GREATER
        if (indexOfLastPeriod != name.Length - 1)
        {
            return name.Substring(0, indexOfLastPeriod);
        }

        return name;
#else
        return indexOfLastPeriod != name.Length - 1 ? name[..indexOfLastPeriod] : name;
#endif
    }
}
