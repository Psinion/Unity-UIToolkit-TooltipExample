using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Data.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.Data.Factories
{
    public class TooltipsFactory : MonoBehaviour
    {
        // Here may be your approach for prefabs loader (Resources, Addressables, etc.)
        [SerializeField] 
        private VisualTreeAsset textTooltipTempalate;

        private Dictionary<TooltipType, VisualTreeAsset> visualTreeDict;
        
        //private GameResourcesService gameResourcesService;

        private Dictionary<TooltipType, TemplateContainer> cachedTemplates = new();

        /*public TooltipsFactory(GameResourcesService gameResourcesService)
        {
            this.gameResourcesService = gameResourcesService;
        }*/

        private void Start()
        {
            if (textTooltipTempalate != null)
            {
                visualTreeDict[TooltipType.TextTooltipTemplate] = textTooltipTempalate;
            }
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

        /*public TextTooltip CreateTextTooltip(TextTooltipData data)
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
        
            return new ToolTooltip(tooltipTemplate, data, prefabsDb);
        }
    
        public ToolCursorTooltip CreateToolCursorTooltip(ToolCursorTooltipData data)
        {
            var tooltipTemplate = GetTooltip(TooltipType.ToolCursorTooltipTemplate);
            if (tooltipTemplate == null)
            {
                return default;
            }
        
            return new ToolCursorTooltip(tooltipTemplate, data, prefabsDb, gameResourcesService);
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
        }*/
    }
}