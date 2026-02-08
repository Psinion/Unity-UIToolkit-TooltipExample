using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Tooltips;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Enums;
using UIToolkit.Tooltip.Example.UI.Tooltips.Extensions;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Main
{
    public class ResourcesPanel : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
    
        [CreateProperty]
        public int MoneyCurrent { get; set; }
    
        private void OnValidate()
        {
            if (uiDocument == null)
            {
                Debug.LogError("UiDocument is not found.");
            }
        }

        private void Awake()
        {
            uiDocument!.rootVisualElement.dataSource = this;
        
            var resourcesService = GameResourcesService.Instance;
            MoneyCurrent = resourcesService.MoneyCurrent;
        
            resourcesService.OnMoneyChanged += OnMoneyChanged;

            var config = new TooltipConfig(new Vector2(10, 0), TooltipPosition.Left);
            uiDocument!.rootVisualElement.Q<VisualElement>("money").AddTooltip(
                new TextTooltipData("Money", "Some resource. +25 every second"), config);
        }
    
        private void OnMoneyChanged(int value)
        {
            MoneyCurrent = value;
        }
    }
}