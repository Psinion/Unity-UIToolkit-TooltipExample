using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools
{
    public class SpawnTool : ITool
    {
        private ITooltipData cachedTooltipData;
        private bool tooltipNeedsUpdate = true;
        
        private GameResourcesService resourcesService;
    
        public ButtonData data { get; set; }

        public SpawnTool(GameResourcesService resourcesService)
        {
            this.resourcesService = resourcesService;
        }

        public void Select(ButtonData data)
        {
            this.data = data;

            SubscribeToResourceChanges();
        }
    
        public void Apply(Vector2 position)
        {
            if (data.TrySpendResources(resourcesService))
            {
                SpawnDummy(position);
            }
            else
            {
                Debug.Log("Resources are not enough!");
            }
        }

        private void SpawnDummy(Vector2 position)
        {
            /*var spawnBuilding = buildingsFactory.SpawnBuilding(data.Key, cell.Coordinates);
            if (spawnBuilding != null)
            {
                spawnBuilding.Entity.AddComponent(new SpawnGrowAnimation(0, 1, 3f));
                ref var scaleComponent = ref spawnBuilding.Entity.GetComponent<WorldScaleComponent>();
                scaleComponent.value = Vector3.zero;
            
                ref var transformRefComponent = ref spawnBuilding.Entity.GetComponent<TransformRef>();
                transformRefComponent.value.localScale = Vector3.zero;
            }*/
        }
    
        public (ITooltipData data, bool cached) CreateTooltipData()
        {
            if (tooltipNeedsUpdate)
            {
                tooltipNeedsUpdate = false;
                cachedTooltipData = new ToolCursorTooltipData(data.Title, data.MoneyRequirement);
                return (cachedTooltipData, false);
            }
        
            return (cachedTooltipData, true);
        }

        public void Cancel()
        {
            UnsubscribeToResourceChanges();
        }
    
        private void SubscribeToResourceChanges()
        {
            resourcesService.OnMoneyChanged += OnResourceChanged;
        }

        private void UnsubscribeToResourceChanges()
        {
            resourcesService.OnMoneyChanged -= OnResourceChanged;
        }
    
        private void OnResourceChanged(int amount)
        {
            tooltipNeedsUpdate = true;
        }
    }
}