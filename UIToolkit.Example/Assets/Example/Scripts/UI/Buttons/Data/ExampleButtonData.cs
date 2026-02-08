using System;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;

namespace UIToolkit.Tooltip.Example.UI.Buttons.Data
{
    public class ExampleButtonData : ButtonData
    {
        public ExampleButtonData(
            string key,
            string displayText,
            string title,
            string description,
            int moneyRequirement,
            Action onClick = null)
            : base(key, displayText, title, description, moneyRequirement, onClick)
        {
        }

        public override ITooltipData CreateTooltipData()
        {
            return new ToolTooltipData(Title, Description, MoneyRequirement);
        }
    }
}