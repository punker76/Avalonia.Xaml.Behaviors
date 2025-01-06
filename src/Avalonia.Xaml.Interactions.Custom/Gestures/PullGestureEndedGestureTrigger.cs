using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.Xaml.Interactions.Custom;

/// <summary>
/// 
/// </summary>
public class PullGestureEndedGestureTrigger : RoutedEventTriggerBase<PullGestureEndedEventArgs>
{
    /// <inheritdoc />
    protected override RoutedEvent<PullGestureEndedEventArgs> RoutedEvent
        => Gestures.PullGestureEndedEvent;

    static PullGestureEndedGestureTrigger()
    {
        EventRoutingStrategyProperty.OverrideMetadata<PullGestureEndedGestureTrigger>(
            new StyledPropertyMetadata<RoutingStrategies>(
                defaultValue: RoutingStrategies.Bubble));
    }
}
