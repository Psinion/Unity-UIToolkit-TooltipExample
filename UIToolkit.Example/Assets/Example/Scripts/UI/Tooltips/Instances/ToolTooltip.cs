using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Instances
{
    public struct ToolTooltip : ITooltipInstance
    {
        private readonly TemplateContainer tooltipTemplate;
        private readonly Label titleLabel;
        private readonly VisualElement resourceRequirements;
        private readonly Label titleDescription;
    
        public ToolTooltip(TemplateContainer tooltipTemplate, ToolTooltipData data)
        {
            this.tooltipTemplate = tooltipTemplate;
            titleLabel = tooltipTemplate.Q<Label>("title");
            resourceRequirements = tooltipTemplate.Q<VisualElement>("resource-requirements");
            titleDescription = tooltipTemplate.Q<Label>("description");

            BindInternal(data);
        }

        public void Bind(ITooltipData data)
        {
            if (data is ToolTooltipData text)
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
            titleDescription.visible = false;
            resourceRequirements.visible = false;
        }

        private void BindInternal(ToolTooltipData data)
        {
            titleLabel.style.display = DisplayStyle.Flex;
            titleLabel.visible = true;
            titleLabel.text = data.title;
        
            if (data.description == null)
            {
                titleDescription.style.display = DisplayStyle.None;
                titleDescription.visible = false;
            }
            else
            {
                titleDescription.style.display = DisplayStyle.Flex;
                titleDescription.visible = true;
                titleDescription.text = data.description;
            }
            
            resourceRequirements.visible = true;
        
            resourceRequirements.Clear();
            var resourceItem = CreateResourceItem(data.moneyRequirement);
            resourceRequirements.Add(resourceItem);
        }

        private VisualElement CreateResourceItem(int moneyRequirement)
        {
            var container = new VisualElement();
            container.AddToClassList("resource-item");
            
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