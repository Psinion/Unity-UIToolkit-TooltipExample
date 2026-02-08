using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Data
{
    public record TextTooltipData(string title = null, string description = null) : ITooltipData;
}