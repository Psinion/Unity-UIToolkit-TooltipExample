using System;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Buttons.Wrappers
{
    public class ButtonWrapper : IDisposable
    {
        private readonly GameResourcesService resourcesService;
    
        private readonly ButtonData data;
        private Button button;
        public Button Button => button;
    
        private TooltipTrigger tooltipTrigger;
    
        private bool isAvailable;
        public bool IsAvailable => isAvailable;

        public ButtonWrapper(ButtonData data)
        {
            this.data = data;
            resourcesService = GameResourcesService.Instance;
        
            CreateButton();
            CreateTooltipTrigger();
            SubscribeToResources();
        }
    
        private void CreateButton()
        {
            var btn = new Button
            {
                text = data.DisplayText
            };
            btn.AddToClassList("psi-button");
        
            btn.clicked += OnButtonClicked;
            button = btn;
        }
    
        private void CreateTooltipTrigger()
        {
            var tooltipData = data.CreateTooltipData();
            tooltipTrigger = new TooltipTrigger(button, tooltipData, TooltipConfig.TooltipUI);
        }
    
        private void SubscribeToResources()
        {
            if (data.MoneyRequirement > 0)
            {
                resourcesService.OnMoneyChanged += OnResourceChanged;
            }
        }
    
        private void UnsubscribeFromResources()
        {
            resourcesService.OnMoneyChanged -= OnResourceChanged;
        }
    
        private void OnResourceChanged(int newAmount)
        {
            Debug.Log("Resource changed");
            // TODO: UpdateButtonState();
        }
    
        private void OnButtonClicked()
        {
            data.OnClick?.Invoke();
        }
    
        public void Dispose()
        {
            button.clicked -= data.OnClick;
            button.RemoveFromHierarchy();
            tooltipTrigger.Dispose();
            UnsubscribeFromResources();
        }
    }
}