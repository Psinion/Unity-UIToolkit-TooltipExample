using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Data
{
    public record ToolCursorTooltipData(string title, int moneyRequirement) : ITooltipData;
}