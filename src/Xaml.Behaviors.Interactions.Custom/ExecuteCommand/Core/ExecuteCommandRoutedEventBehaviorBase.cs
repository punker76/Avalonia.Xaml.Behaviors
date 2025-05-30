// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using Avalonia.Interactivity;

namespace Avalonia.Xaml.Interactions.Custom;

/// <summary>
/// 
/// </summary>
public abstract class ExecuteCommandRoutedEventBehaviorBase : ExecuteCommandBehaviorBase
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly StyledProperty<RoutingStrategies> EventRoutingStrategyProperty =
        AvaloniaProperty.Register<ExecuteCommandRoutedEventBehaviorBase, RoutingStrategies>(nameof(EventRoutingStrategy), RoutingStrategies.Bubble);

    /// <summary>
    /// 
    /// </summary>
    public RoutingStrategies EventRoutingStrategy
    {
        get => GetValue(EventRoutingStrategyProperty);
        set => SetValue(EventRoutingStrategyProperty, value);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool MarkAsHandled { get; set; } = true;
}
