using System;
using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Core;
using UIToolkit.Tooltip.Example.Data.Enums;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.Data.Factories
{
    public class TooltipsFactory : SingletonMono<TooltipsFactory>
    {
        // Here may be your approach for prefabs loader (Resources, Addressables, etc.)
        [SerializeField] 
        private VisualTreeAsset textTooltipTemplate;
        
        [SerializeField] 
        private VisualTreeAsset toolTooltipTemplate;
        
        [SerializeField] 
        private VisualTreeAsset toolCursorTooltipTemplate;

        private Dictionary<TooltipType, VisualTreeAsset> visualTreeDict = new();
        
        //private GameResourcesService gameResourcesService;

        private Dictionary<TooltipType, TemplateContainer> cachedTemplates = new();

        /*public TooltipsFactory(GameResourcesService gameResourcesService)
        {
            this.gameResourcesService = gameResourcesService;
        }*/

        private void OnValidate()
        {
            if (textTooltipTemplate == null)
            {
                Debug.LogError("TextTooltipTemplate is not found.");
            }
            
            if (toolTooltipTemplate == null)
            {
                Debug.LogError("ToolTooltipTemplate is not found.");
            }
            
            if (toolCursorTooltipTemplate == null)
            {
                Debug.LogError("ToolCursorTooltipTemplate is not found.");
            }
        }
        
        private void Start()
        {
            visualTreeDict[TooltipType.TextTooltipTemplate] = textTooltipTemplate;
            visualTreeDict[TooltipType.ToolTooltipTemplate] = toolTooltipTemplate;
            visualTreeDict[TooltipType.ToolCursorTooltipTemplate] = toolCursorTooltipTemplate;
        }

        private TemplateContainer GetTooltip(TooltipType type)
        {
            if (cachedTemplates.TryGetValue(type, out var template))
            {
                return template;
            }
            
            if (!visualTreeDict.TryGetValue(type, out var tooltipTemplate))
            {
                Debug.LogError("Template not found");
                return null;
            }

            var templateInstance = tooltipTemplate.Instantiate();
            cachedTemplates[type] = templateInstance;
            return templateInstance;
        }

        public TextTooltip CreateTextTooltip(TextTooltipData data)
        {
            var tooltipTemplate = GetTooltip(TooltipType.TextTooltipTemplate);
            if (tooltipTemplate == null)
            {
                return default;
            }
        
            return new TextTooltip(tooltipTemplate, data);
        }
    
        public ToolTooltip CreateToolTooltip(ToolTooltipData data)
        {
            var tooltipTemplate = GetTooltip(TooltipType.ToolTooltipTemplate);
            if (tooltipTemplate == null)
            {
                return default;
            }

            return new ToolTooltip(tooltipTemplate, data);
        }

        public ToolCursorTooltip CreateToolCursorTooltip(ToolCursorTooltipData data)
        {
            var tooltipTemplate = GetTooltip(TooltipType.ToolCursorTooltipTemplate);
            if (tooltipTemplate == null)
            {
                return default;
            }

            return new ToolCursorTooltip(tooltipTemplate, data, GameResourcesService.Instance);
        }

        public ITooltipInstance CreateTooltip(ITooltipData data)
        {
            return data switch
            {
                TextTooltipData textData => CreateTextTooltip(textData),
                ToolTooltipData toolData => CreateToolTooltip(toolData),
                ToolCursorTooltipData toolData => CreateToolCursorTooltip(toolData),
                _ => throw new NotImplementedException()
            };
        }
    }
}