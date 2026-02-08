using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Core;
using UIToolkit.Tooltip.Example.Data.Factories;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Enums;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips
{
    public class TooltipService : SingletonMono<TooltipService>
    {
        [SerializeField] 
        private UIDocument uiDocument;
        
        [SerializeField]
        private TooltipsFactory tooltipsFactory;
 
        private TooltipConfig defaultConfig = TooltipConfig.Default;
    
        private VisualElement tooltipContainer;
        private bool isUiHover;
    
        private ITooltipInstance tooltipInstance;

        public bool IsActive => tooltipContainer.visible;
        public bool IsUiHover => isUiHover;
    
        private bool shouldRedraw;
        public bool ShouldRedraw => shouldRedraw;

        private void OnValidate()
        {
            if (uiDocument == null)
            {
                Debug.LogError("UiDocument is not found.");
            }

            if (tooltipsFactory == null)
            {
                Debug.LogError("TooltipsFactory is not found.");
            }
        }
    
        public void Start()
        {
            var root = uiDocument!.rootVisualElement;
        
            tooltipContainer = root.Q<VisualElement>("tooltip-container");
        
            tooltipContainer.pickingMode = PickingMode.Ignore;
            tooltipContainer.RegisterCallback<MouseEnterEvent>(e => e.StopPropagation());
        
            Hide();
        }
    
        public void UpdatePosition(Vector2 screenPosition, TooltipConfig? config = null)
        {
            CancelTransitions();
        
            var currentConfig = config ?? defaultConfig;
            PositionAtScreen(screenPosition, currentConfig);
        
            RestoreTransitions();
        }
    
        public void ShowAtScreenPosition(ITooltipData data, Vector2 screenPosition, 
            TooltipConfig? config = null)
        {
            ShowInternal(data, config ?? defaultConfig, screenPosition: screenPosition);
        }

        public void ShowAtUIElement(ITooltipData data, VisualElement element, 
            TooltipConfig? config = null)
        {
            ShowInternal(data, config ?? defaultConfig, targetElement: element);
        }
    
        private void ShowInternal(
            ITooltipData tooltipData, 
            TooltipConfig? config = null, 
            Vector2? screenPosition = null, 
            VisualElement targetElement = null)
        {
            var currentConfig = config ?? defaultConfig;
        
            tooltipInstance = tooltipsFactory.CreateTooltip(tooltipData);
            tooltipInstance!.Integrate(tooltipContainer);
        
            tooltipContainer.style.maxWidth = currentConfig.maxWidth;
            tooltipContainer.visible = true;
            tooltipContainer.style.opacity = 0f;

            shouldRedraw = false;
            isUiHover = targetElement != null;
            
            CancelTransitions();
            tooltipContainer.MarkDirtyRepaint();
        
            tooltipContainer.schedule.Execute(() => {
                PositionAndShow(currentConfig, screenPosition, targetElement);
                RestoreTransitions();
                tooltipContainer.style.opacity = 1f;
            }).ExecuteLater(currentConfig.showDelay);
        }
    
        private void PositionAndShow(TooltipConfig config, Vector2? screenPosition, VisualElement targetElement)
        {
            if (targetElement != null)
            {
                PositionForElement(targetElement, config);
            }
            else if (screenPosition.HasValue)
            {
                PositionAtScreen(screenPosition.Value, config);
            }
        }
    
        private void PositionForElement(VisualElement element, TooltipConfig config)
        {
            Rect elementRect = element.worldBound;
            
            Vector2 finalPosition = CalculatePosition(elementRect, config);
            ApplyPosition(finalPosition);
        }
    
        private void PositionAtScreen(Vector2 screenPos, TooltipConfig config)
        {
            var root = uiDocument!.rootVisualElement;
            float scale = root.resolvedStyle.scale.value.x;
            
            Vector2 panelPos = new Vector2(screenPos.x / scale, 
                (Screen.height - screenPos.y) / scale);
            
            Vector2 finalPosition = CalculatePosition(
                new Rect(panelPos, Vector2.zero), 
                config);
            
            ApplyPosition(finalPosition);
        }
    
        private Vector2 CalculatePosition(Rect targetRect, TooltipConfig config)
        {
            var root = uiDocument!.rootVisualElement;
            Rect viewport = root.contentRect;
            Rect tooltipRect = tooltipContainer.worldBound;
        
            return config.position switch
            {
                TooltipPosition.Top => PositionTop(targetRect, tooltipRect, config.offset, viewport),
                TooltipPosition.Bottom => PositionBottom(targetRect, tooltipRect, config.offset, viewport),
                TooltipPosition.Left => PositionLeft(targetRect, tooltipRect, config.offset, viewport),
                TooltipPosition.Right => PositionRight(targetRect, tooltipRect, config.offset, viewport),
                _ => PositionAuto(targetRect, tooltipRect, config, viewport)
            };
        }

        private Vector2 PositionTop(Rect target, Rect tooltip, Vector2 offset, Rect viewport)
        {
            float y = target.y - tooltip.height - offset.y;
        
            if (y < 0)
            {
                y = target.y + target.height + offset.y;
            }
            
            return CenterHorizontally(target, tooltip, viewport, y);
        }

        private Vector2 PositionBottom(Rect target, Rect tooltip, Vector2 offset, Rect viewport)
        {
            float y = target.y + target.height + offset.y;
            
            if (y + tooltip.height > viewport.height)
            {
                y = target.y - tooltip.height - offset.y;
            }
        
            return CenterHorizontally(target, tooltip, viewport, y);
        }

        private Vector2 PositionLeft(Rect target, Rect tooltip, Vector2 offset, Rect viewport)
        {
            float x = target.x - tooltip.width - offset.x;
            
            if (x < 0)
            {
                x = target.x + target.width + offset.x;
            }
        
            return CenterVertically(target, tooltip, viewport, x);
        }

        private Vector2 PositionRight(Rect target, Rect tooltip, Vector2 offset, Rect viewport)
        {
            float x = target.x + target.width + offset.x;
            
            if (x + tooltip.width > viewport.width)
            {
                x = target.x - tooltip.width - offset.x;
            }
        
            return CenterVertically(target, tooltip, viewport, x);
        }   
        
        private Vector2 PositionAuto(Rect target, Rect tooltip, TooltipConfig config, Rect viewport)
        {
            Vector2 lastPos = default;
            
            // Select first visible
            foreach (var pos in GetAutoPositions(target, tooltip, config.offset, viewport))
            {
                lastPos = pos;
                if (pos.x >= 0 && pos.x + tooltip.width <= viewport.width &&
                    pos.y >= 0 && pos.y + tooltip.height <= viewport.height)
                {
                    return pos;
                }
            }
            
            return lastPos;
        }

        private IEnumerable<Vector2> GetAutoPositions(Rect target, Rect tooltip, Vector2 offset, Rect viewport)
        {
            yield return PositionBottom(target, tooltip, offset, viewport);
            yield return PositionTop(target, tooltip, offset, viewport);
            yield return PositionRight(target, tooltip, offset, viewport);
            yield return PositionLeft(target, tooltip, offset, viewport);
        }
        
        private Vector2 CenterHorizontally(Rect target, Rect tooltip, Rect viewport, float y)
        {
            float x = target.x + target.width / 2 - tooltip.width / 2;
            return ClampToViewport(x, y, tooltip, viewport);
        }

        private Vector2 CenterVertically(Rect target, Rect tooltip, Rect viewport, float x)
        {
            float y = target.y + target.height / 2 - tooltip.height / 2;
            return ClampToViewport(x, y, tooltip, viewport);
        }

        private Vector2 ClampToViewport(float x, float y, Rect tooltip, Rect viewport)
        {
            x = Mathf.Clamp(x, 0, viewport.width - tooltip.width);
            y = Mathf.Clamp(y, 0, viewport.height - tooltip.height);
            return new Vector2(x, y);
        }
    
        private void ApplyPosition(Vector2 position)
        {
            tooltipContainer.style.left = position.x;
            tooltipContainer.style.top = position.y;
        }

        public void Hide()
        {
            tooltipContainer.style.opacity = 0;
            tooltipContainer.visible = false;
        
            isUiHover = false;

            if (tooltipInstance != null)
            {
                tooltipInstance.Hide();
            }
        }
    
        public void MarkForRedraw() => shouldRedraw = true;
    
        private void CancelTransitions()
        {
            tooltipContainer.style.transitionDuration = StyleKeyword.None;
            tooltipContainer.style.transitionDelay = StyleKeyword.None;
            tooltipContainer.style.transitionTimingFunction = StyleKeyword.None;
        }
    
        private void RestoreTransitions()
        {
            tooltipContainer.style.transitionDuration = StyleKeyword.Null;
            tooltipContainer.style.transitionDelay = StyleKeyword.Initial;
            tooltipContainer.style.transitionTimingFunction = StyleKeyword.Initial;
        }
    }
}