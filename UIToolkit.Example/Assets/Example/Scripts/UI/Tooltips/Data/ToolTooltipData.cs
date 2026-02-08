using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Data
{
    public record ToolTooltipData(string title, string? description, int moneyRequirement) : ITooltipData;
}