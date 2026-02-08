using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Instances
{
    public struct TextTooltip : ITooltipInstance
    {
        private readonly TemplateContainer tooltipTemplate;
        private readonly Label titleLabel;
        private readonly Label titleDescription;
    
        public TextTooltip(TemplateContainer tooltipTemplate, TextTooltipData data)
        {
            this.tooltipTemplate = tooltipTemplate;
            titleLabel = tooltipTemplate.Q<Label>("tooltip-title");
            titleDescription = tooltipTemplate.Q<Label>("tooltip-description");

            BindInternal(data);
        }

        public void Bind(ITooltipData data)
        {
            if (data is TextTooltipData text)
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
            tooltipTemplate.visible = false;
            titleLabel.visible = false;
            titleDescription.visible = false;
        }

        private void BindInternal(TextTooltipData data)
        {
            if (data.title == null)
            {
                titleLabel.style.display = DisplayStyle.None;
                titleLabel.visible = false;
            }
            else
            {
                titleLabel.style.display = DisplayStyle.Flex;
                titleLabel.visible = true;
                titleLabel.text = data.title;
            }
        
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
        }
    }
}