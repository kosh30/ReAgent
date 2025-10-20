using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Numerics;
using System.Windows.Forms;
using ReAgent.State;
using static ExileCore.Shared.Nodes.HotkeyNodeV2;

namespace ReAgent.SideEffects;

public enum MouseButton
{
    Left,
    Right,
    Middle
}

[DynamicLinqType]
[Api]
public record MouseClickSideEffect(Vector2 Position, MouseButton Button = MouseButton.Left) : ISideEffect
{
    public MouseClickSideEffect(int x, int y, MouseButton button = MouseButton.Left) : this(new Vector2(x, y), button)
    {
    }
    public SideEffectApplicationResult Apply(RuleState state)
    {
        if (!state.InternalState.CanPressKey)
        {
            return SideEffectApplicationResult.UnableToApply;
        }

        // Check if we already have a mouse click pending
        if (state.InternalState.MouseClickToPerform != null)
        {
            // Check if it's the same click (duplicate)
            if (state.InternalState.MouseClickToPerform.Value.Position == Position &&
                state.InternalState.MouseClickToPerform.Value.Button == Button)
            {
                return SideEffectApplicationResult.AppliedDuplicate;
            }
            
            // Different click already pending
            return SideEffectApplicationResult.UnableToApply;
        }

        state.InternalState.MouseClickToPerform = (Position, Button);
        return SideEffectApplicationResult.AppliedUnique;
    }

    public override string ToString() => $"Click {Button} mouse button at ({Position.X}, {Position.Y})";
}

[DynamicLinqType]
[Api]
public record MouseMoveSideEffect(Vector2 Position) : ISideEffect
{
    public MouseMoveSideEffect(int x, int y) : this(new Vector2(x, y))
    {
    }
    public SideEffectApplicationResult Apply(RuleState state)
    {
        // Check if we already have a mouse move pending
        if (state.InternalState.MouseMoveToPosition != null)
        {
            // Check if it's the same position (duplicate)
            if (state.InternalState.MouseMoveToPosition.Value == Position)
            {
                return SideEffectApplicationResult.AppliedDuplicate;
            }
            
            // Different move already pending - replace with the new one
            // (multiple moves in one frame should just use the last one)
        }

        state.InternalState.MouseMoveToPosition = Position;
        return SideEffectApplicationResult.AppliedUnique;
    }

    public override string ToString() => $"Move mouse to ({Position.X}, {Position.Y})";
}

[DynamicLinqType]
[Api]
public record MouseMoveAndClickSideEffect(Vector2 Position, MouseButton Button = MouseButton.Left, int DelayMs = 0) : ISideEffect
{
    public MouseMoveAndClickSideEffect(int x, int y, MouseButton button = MouseButton.Left, int delayMs = 0) : this(new Vector2(x, y), button, delayMs)
    {
    }
    public SideEffectApplicationResult Apply(RuleState state)
    {
        if (!state.InternalState.CanPressKey)
        {
            return SideEffectApplicationResult.UnableToApply;
        }

        // Check if we already have the same action pending
        if (state.InternalState.MouseMoveToPosition == Position &&
            state.InternalState.MouseClickToPerform?.Position == Position &&
            state.InternalState.MouseClickToPerform?.Button == Button)
        {
            return SideEffectApplicationResult.AppliedDuplicate;
        }

        // Set both move and click with delay
        state.InternalState.MouseMoveToPosition = Position;
        state.InternalState.MouseClickToPerform = (Position, Button);
        state.InternalState.MouseActionDelayMs = DelayMs;
        return SideEffectApplicationResult.AppliedUnique;
    }

    public override string ToString() => DelayMs > 0 ? $"Move to ({Position.X}, {Position.Y}) and click {Button} after {DelayMs}ms" : $"Move to ({Position.X}, {Position.Y}) and click {Button}";
}

[DynamicLinqType]
[Api]
public record MouseMoveAndPressKeySideEffect(Vector2 Position, HotkeyNodeValue Key, int DelayMs = 0) : ISideEffect
{
    public MouseMoveAndPressKeySideEffect(int x, int y, HotkeyNodeValue key, int delayMs = 0) : this(new Vector2(x, y), key, delayMs)
    {
    }

    public MouseMoveAndPressKeySideEffect(Vector2 position, Keys key, int delayMs = 0) : this(position, new HotkeyNodeValue(key), delayMs)
    {
    }

    public MouseMoveAndPressKeySideEffect(int x, int y, Keys key, int delayMs = 0) : this(new Vector2(x, y), new HotkeyNodeValue(key), delayMs)
    {
    }

    public SideEffectApplicationResult Apply(RuleState state)
    {
        if (!state.InternalState.CanPressKey)
        {
            return SideEffectApplicationResult.UnableToApply;
        }

        // Check if we already have the same action pending
        if (state.InternalState.MouseMoveToPosition == Position &&
            state.InternalState.KeyToPress == Key)
        {
            return SideEffectApplicationResult.AppliedDuplicate;
        }

        // Check if there's already a different key to press
        if (state.InternalState.KeyToPress != null && state.InternalState.KeyToPress != Key)
        {
            return SideEffectApplicationResult.UnableToApply;
        }

        // Set both move and key press with delay
        state.InternalState.MouseMoveToPosition = Position;
        state.InternalState.KeyToPress = Key;
        state.InternalState.MouseActionDelayMs = DelayMs;
        return SideEffectApplicationResult.AppliedUnique;
    }

    public override string ToString() => DelayMs > 0 ? $"Move to ({Position.X}, {Position.Y}) and press key {Key} after {DelayMs}ms" : $"Move to ({Position.X}, {Position.Y}) and press key {Key}";
}
