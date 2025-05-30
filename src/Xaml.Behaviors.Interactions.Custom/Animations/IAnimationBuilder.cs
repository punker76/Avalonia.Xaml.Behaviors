// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using Avalonia.Animation;
using Avalonia.Controls;

namespace Avalonia.Xaml.Interactions.Custom;

/// <summary>
/// Provides a way to build <see cref="Animation"/> instances in code.
/// </summary>
public interface IAnimationBuilder
{
    /// <summary>
    /// Creates an animation for the specified control.
    /// </summary>
    /// <param name="control">The control that will run the animation.</param>
    /// <returns>The created animation or <c>null</c>.</returns>
    Animation.Animation? Build(Control control);
}
