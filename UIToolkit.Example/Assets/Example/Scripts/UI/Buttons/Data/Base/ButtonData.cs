using System;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;

namespace UIToolkit.Tooltip.Example.UI.Buttons.Data.Base
{
    public abstract class ButtonData
    {
        public string Key { get; }
        public string DisplayText { get; }
        public string Title { get; }
        public string Description { get; }
        public int MoneyRequirement { get; }
        public Action OnClick { get; set; }

        protected ButtonData(
            string key,
            string displayText,
            string title,
            string description,
            int moneyRequirement,
            Action onClick = null)
        {
            Key = key;
            DisplayText = displayText;
            Title = title;
            Description = description;
            MoneyRequirement = moneyRequirement;
            OnClick = onClick;
        }
    
        public virtual bool CanAfford(GameResourcesService resourcesService)
        {
            if (resourcesService.MoneyCurrent < MoneyRequirement)
            {
                return false;
            }
        
            return true;
        }
    
        public virtual bool TrySpendResources(GameResourcesService resourcesService)
        {
            if (!CanAfford(resourcesService))
            {
                return false;
            }
            
            SpendResource(resourcesService, MoneyRequirement);
        
            return true;
        }
    
        private void SpendResource(GameResourcesService service, int amount)
        {
            service.AddMoney(-amount);
        }
    
        public abstract ITooltipData CreateTooltipData();
    }
}