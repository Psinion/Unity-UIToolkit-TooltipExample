using System;
using UIToolkit.Tooltip.Example.UI.Tooltips;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Enums;
using UIToolkit.Tooltip.Example.UI.Tooltips.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar
{
    public class ToolbarPanel : MonoBehaviour
    {
        public static readonly TooltipConfig ToolbarTooltipConfig = new()
        {
            offset = new Vector2(0, 10),
            position = TooltipPosition.Top,
            maxWidth = 300f,
            showDelay = 350,
        };
        
        [SerializeField] private UIDocument uiDocument;
        
        //private IToolManager toolManager = null!;
    
        private VisualElement buttonsContainer;
    
        private void OnValidate()
        {
            if (uiDocument == null)
            {
                Debug.LogError("UiDocument is not found.");
            }
        }

        private void Start()
        {
            Draw();
        }

        public void Draw()
        {
            var root = uiDocument!.rootVisualElement;
        
            buttonsContainer = root.Q<VisualElement>("buttons");
            if (buttonsContainer == null)
            {
                throw new InvalidOperationException("ButtonsContainer not found.");
            }
        
            buttonsContainer!.Clear();
        
            RegisterButton("BTN1", new ToolTooltipData("BTN1", "BTN1 some description", 25), null);
            RegisterButton("BTN2", new ToolTooltipData("BTN2", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", 200), null);
            RegisterButton("BTN3", new ToolTooltipData("BTN3", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 80), null);
        }
    
        private void RegisterButton(string buttonText, ITooltipData tooltipData, Action onClick)
        {
            var button = new Button();
            button.text = buttonText;
            button.AddToClassList("psi-button");
            
            button.AddTooltip(tooltipData, ToolbarTooltipConfig);

            button.clicked += onClick;
            
            buttonsContainer!.Add(button);
        }
    }
}