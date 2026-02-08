using System;
using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Data.Enums;
using UIToolkit.Tooltip.Example.UI.Buttons.Data;
using UIToolkit.Tooltip.Example.UI.Buttons.Wrappers;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Enums;
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
        [SerializeField] private UIDocument uiDocument;
        
        private IToolManager toolManager;
    
        private VisualElement buttonsContainer;
        
        private readonly List<ButtonWrapper> buttonWrappers = new();
    
        private void OnValidate()
        {
            if (uiDocument == null)
            {
                Debug.LogError("UiDocument is not found.");
            }
        }

        private void Start()
        {
            toolManager = ToolManager.Instance;
            
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
        
            CreateExampleButton(
                nameof(FigureType.Cube), 
                "CB", 
                "Spawn cube", 
                "Some description", 
                25
                );
            CreateExampleButton(
                nameof(FigureType.Sphere), 
                "SP", 
                "Spawn sphere", 
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", 
                200
                );
            CreateExampleButton(
                nameof(FigureType.Capsule), 
                "CP", 
                "Spawn capsule", 
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 
                80
                );
            
            foreach (var buttonWrapper in buttonWrappers)
            {
                buttonsContainer.Add(buttonWrapper.Button);
            }
        }
    
        private ButtonWrapper CreateExampleButton(
            string key,
            string displayText,
            string title,
            string description,
            int moneyRequirement)
        {
            var buttonData = new ExampleButtonData(
                key,
                displayText,
                title,
                description,
                moneyRequirement
            );

            buttonData.OnClick = () =>
            {
                toolManager.SelectTool(ToolType.Spawn, buttonData);
            };
        
            var wrapper = new ButtonWrapper(buttonData);
            buttonWrappers.Add(wrapper);
            return wrapper;
        }
    }
}