using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Instances
{
    public struct ToolCursorTooltip : ITooltipInstance
    {
        private GameResourcesService gameResourcesService;
    
        private readonly TemplateContainer tooltipTemplate;
        private readonly Label titleLabel;
        private readonly VisualElement resourceRequirements;
    
        public ToolCursorTooltip(
            TemplateContainer tooltipTemplate, 
            ToolCursorTooltipData data, 
            GameResourcesService gameResourcesService
        )
        {
            this.gameResourcesService = gameResourcesService;
            this.tooltipTemplate = tooltipTemplate;
            titleLabel = tooltipTemplate.Q<Label>("title");
            resourceRequirements = tooltipTemplate.Q<VisualElement>("resource-requirements");

            BindInternal(data);
        }

        public void Bind(ITooltipData data)
        {
            if (data is ToolCursorTooltipData text)
            {
                BindInternal(text);
            }
        }

        public void Integrate(VisualElement tooltipContainer)
        {
            var container = tooltipContainer.Q<VisualElement>("tooltip-container");
            container.Clear();
        
            container.Add(tooltipTemplate);
        }

        public void Hide()
        {
            titleLabel.visible = false;
            resourceRequirements.visible = false;
        }

        private void BindInternal(ToolCursorTooltipData data)
        {
            titleLabel.style.display = DisplayStyle.Flex;
            titleLabel.visible = true;
            titleLabel.text = data.title;
            
            resourceRequirements.visible = true;
        
            resourceRequirements.Clear();
            var resourceItem = CreateResourceItem(data.moneyRequirement);
            resourceRequirements.Add(resourceItem);
        }

        private VisualElement CreateResourceItem(int moneyRequirement)
        {
            var container = new VisualElement();
            container.AddToClassList("resource-item");
        
            if (gameResourcesService.MoneyCurrent < moneyRequirement)
            {
                container.AddToClassList("missing");
            }
        
            var moneyLabel = new Label("Money:");
            moneyLabel.AddToClassList("resource-item-title");
            container.Add(moneyLabel);

            var amountLabel = new Label(moneyRequirement.ToString());
            amountLabel.AddToClassList("resource-item-amount");
        
            container.Add(amountLabel);

            return container;
        }
    }
}