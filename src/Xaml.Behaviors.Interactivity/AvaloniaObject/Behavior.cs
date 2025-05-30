﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using System;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Controls;

namespace Avalonia.Xaml.Interactivity;

/// <summary>
/// A base class for behaviors, implementing the basic plumbing of <see cref="IBehavior"/>.
/// </summary>
public abstract class Behavior : AvaloniaObject, IBehavior, IBehaviorEventsHandler
{
    /// <summary>
    /// Identifies the <seealso cref="IsEnabled"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<bool> IsEnabledProperty =
        AvaloniaProperty.Register<Behavior, bool>(nameof(IsEnabled), defaultValue: true);

    /// <summary>
    /// Gets the <see cref="AvaloniaObject"/> to which the behavior is attached.
    /// </summary>
    public AvaloniaObject? AssociatedObject { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is enabled.
    /// </summary>
    /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
    public bool IsEnabled
    {
        get => GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Attaches the behavior to the specified <see cref="AvaloniaObject"/>.
    /// </summary>
    /// <param name="associatedObject">The <see cref="AvaloniaObject"/> to which to attach.</param>
    /// <exception cref="ArgumentNullException"><paramref name="associatedObject"/> is null.</exception>
    public void Attach(AvaloniaObject? associatedObject)
    {
        if (Equals(associatedObject, AssociatedObject))
        {
            return;
        }

        if (AssociatedObject is not null)
        {
            throw new InvalidOperationException(string.Format(
                CultureInfo.CurrentCulture,
                "An instance of a behavior cannot be attached to more than one object at a time."));
        }

        Debug.Assert(associatedObject is not null, "Cannot attach the behavior to a null object.");
        AssociatedObject = associatedObject ?? throw new ArgumentNullException(nameof(associatedObject));

        OnAttached();
    }

    /// <summary>
    /// Detaches the behaviors from the <see cref="AssociatedObject"/>.
    /// </summary>
    public void Detach()
    {
        OnDetaching();
        AssociatedObject = null;
    }

    /// <summary>
    /// Called after the behavior is attached to the <see cref="AssociatedObject"/>.
    /// </summary>
    /// <remarks>
    /// Override this to hook up functionality to the <see cref="AssociatedObject"/>
    /// </remarks>
    protected virtual void OnAttached()
    {
    }

    /// <summary>
    /// Called when the behavior is being detached from its <see cref="AssociatedObject"/>.
    /// </summary>
    /// <remarks>
    /// Override this to unhook functionality from the <see cref="AssociatedObject"/>
    /// </remarks>
    protected virtual void OnDetaching()
    {
    }

    void IBehaviorEventsHandler.AttachedToVisualTreeEventHandler() => OnAttachedToVisualTree();

    void IBehaviorEventsHandler.DetachedFromVisualTreeEventHandler() => OnDetachedFromVisualTree();

    void IBehaviorEventsHandler.AttachedToLogicalTreeEventHandler() => OnAttachedToLogicalTree();

    void IBehaviorEventsHandler.DetachedFromLogicalTreeEventHandler() => OnDetachedFromLogicalTree();

    void IBehaviorEventsHandler.LoadedEventHandler() => OnLoaded();

    void IBehaviorEventsHandler.UnloadedEventHandler() => OnUnloaded();

    void IBehaviorEventsHandler.InitializedEventHandler() => OnInitializedEvent();

    void IBehaviorEventsHandler.DataContextChangedEventHandler() => OnDataContextChangedEvent();

    void IBehaviorEventsHandler.ResourcesChangedEventHandler() => OnResourcesChangedEvent();

    void IBehaviorEventsHandler.ActualThemeVariantChangedEventHandler() => OnActualThemeVariantChangedEvent();

    /// <summary>
    /// Called after the <see cref="AssociatedObject"/> is attached to the visual tree.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="Visual"/>.
    /// </remarks>
    protected virtual void OnAttachedToVisualTree()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> is being detached from the visual tree.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="Visual"/>.
    /// </remarks>
    protected virtual void OnDetachedFromVisualTree()
    {
    }

    /// <summary>
    /// Called after the <see cref="AssociatedObject"/> is attached to the logical tree.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnAttachedToLogicalTree()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> is being detached from the logical tree.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnDetachedFromLogicalTree()
    {
    }

    /// <summary>
    /// Called after the <see cref="AssociatedObject"/> is loaded.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="Control"/>.
    /// </remarks>
    protected virtual void OnLoaded()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> is unloaded.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="Control"/>.
    /// </remarks>
    protected virtual void OnUnloaded()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> is initialized.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnInitializedEvent()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> DataContext changed.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnDataContextChangedEvent()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> Resources changed.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnResourcesChangedEvent()
    {
    }

    /// <summary>
    /// Called when the <see cref="AssociatedObject"/> ActualThemeVariant changed.
    /// </summary>
    /// <remarks>
    /// Invoked only when the <see cref="AssociatedObject"/> is of type <see cref="StyledElement"/>.
    /// </remarks>
    protected virtual void OnActualThemeVariantChangedEvent()
    {
    }
}
