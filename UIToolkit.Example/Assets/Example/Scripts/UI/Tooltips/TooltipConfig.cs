using UIToolkit.Tooltip.Example.UI.Tooltips.Enums;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Tooltips
{
    public struct TooltipConfig
    {
        public Vector2 offset;
        public TooltipPosition position;
        public float maxWidth;
        public int showDelay;

        public TooltipConfig(
            Vector2 offset, 
            TooltipPosition position = TooltipPosition.Auto, 
            float maxWidth = 300, 
            int showDelay = 15
        )
        {
            this.offset = offset;
            this.position = position;
            this.maxWidth = maxWidth;
            this.showDelay = showDelay;
        }
    
        public static readonly TooltipConfig Default = new(new Vector2(20, -5), TooltipPosition.Top);
    
        public static readonly TooltipConfig TooltipUI = new(new Vector2(0, 10), TooltipPosition.Top);
    }
}