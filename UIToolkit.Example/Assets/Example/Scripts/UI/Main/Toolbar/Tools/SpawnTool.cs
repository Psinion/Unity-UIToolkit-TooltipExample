using System;
using UIToolkit.Tooltip.Example.Data.Factories;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools
{
    public class SpawnTool : ITool, IDisposable
    {
        private ITooltipData cachedTooltipData;
        private bool tooltipNeedsUpdate = true;
        private bool isSubscribed;
        
        private GameResourcesService resourcesService;
    
        public ButtonData data { get; set; }

        public SpawnTool(GameResourcesService resourcesService)
        {
            this.resourcesService = resourcesService;
        }

        public void Select(ButtonData data)
        {
            if (isSubscribed)
            {
                UnsubscribeToResourceChanges();
            }
            
            this.data = data;
            tooltipNeedsUpdate = true;

            SubscribeToResourceChanges();
        }
    
        public void Apply(Vector3 position)
        {
            if (data.TrySpendResources(resourcesService))
            {
                SpawnDummy(position);
            }
            else
            {
                Debug.Log("Resources are not enough!");
            }
        }

        private void SpawnDummy(Vector3 position)
        {
            FiguresFactory.Instance.CreateFigure(data.Key, position);
        }
    
        public (ITooltipData data, bool cached) CreateTooltipData()
        {
            if (tooltipNeedsUpdate)
            {
                tooltipNeedsUpdate = false;
                cachedTooltipData = new ToolCursorTooltipData(data.Title, data.MoneyRequirement);
                return (cachedTooltipData, false);
            }
        
            return (cachedTooltipData, true);
        }

        public void Cancel()
        {
            UnsubscribeToResourceChanges();
        }
    
        private void SubscribeToResourceChanges()
        {
            resourcesService.OnMoneyChanged += OnResourceChanged;
        }

        private void UnsubscribeToResourceChanges()
        {
            resourcesService.OnMoneyChanged -= OnResourceChanged;
        }
    
        private void OnResourceChanged(int amount)
        {
            tooltipNeedsUpdate = true;
        }

        public void Dispose()
        {
            UnsubscribeToResourceChanges();
        }
    }
}