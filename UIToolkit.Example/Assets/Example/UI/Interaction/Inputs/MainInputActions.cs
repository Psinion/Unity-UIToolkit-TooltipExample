using System;
using UnityEngine.InputSystem;

namespace UIToolkit.Tooltip.Example.UI.Interaction.Inputs
{
    public class MainInputActions : IDisposable
    {
        private InputActionMap mainActionMap;
        
        public InputAction leftClickAction;
        public InputAction rightClickAction;
        public InputAction mousePositionAction;

        public MainInputActions()
        {
            mainActionMap = new InputActionMap("Main");

            leftClickAction = mainActionMap
                .AddAction(nameof(GameInputActionType.LeftClick), InputActionType.Button);
            leftClickAction.AddBinding("<Mouse>/leftButton");
        
            rightClickAction = mainActionMap
                .AddAction(nameof(GameInputActionType.RightClick), InputActionType.Button);
            rightClickAction.AddBinding("<Mouse>/rightButton");
        
            mousePositionAction = mainActionMap
                .AddAction(nameof(GameInputActionType.MousePosition));
            mousePositionAction.AddBinding("<Mouse>/position");
        }

        public void Enable()
        {
            mainActionMap.Enable();
        }
    
        public void Disable()
        {
            mainActionMap.Disable();
        }

        public void Dispose()
        {
            mainActionMap?.Dispose();
            leftClickAction?.Dispose();
            rightClickAction?.Dispose();
            mousePositionAction?.Dispose();
        }
    }
}