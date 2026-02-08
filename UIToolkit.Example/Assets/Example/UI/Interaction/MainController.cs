using UIToolkit.Tooltip.Example.UI.Interaction.Inputs;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar;
using UIToolkit.Tooltip.Example.UI.Tooltips;
using UIToolkit.Tooltip.Example.UI.Tooltips.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIToolkit.Tooltip.Example.UI.Interaction
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        private ToolManager toolManager;
        
        [SerializeField]
        private TooltipService uiTooltipService;
        private readonly TooltipConfig tooltipConfig = new()
        {
            offset = new Vector2(24f, 24f),
            position = TooltipPosition.Right,
            maxWidth = 300f,
        };
    
        [SerializeField] 
        private UILayerManager uiLayerManager;
        
        private MainInputActions localMapInputActions = new();
        
        private InteractableView lastHoveredObject;
    
        private const float MouseHoverUpdateInterval = 0.20f;
        private float mouseHoverTimeCurrent;
        private const float TooltipShowDelay = 0.25f;
        private float hoverAccumulatedTime;          
    
        private InteractableView currentHoverTarget;

        public void OnEnable()
        {
            mouseHoverTimeCurrent = Random.Range(0f, MouseHoverUpdateInterval);
            
            localMapInputActions.Enable();
        }
        
        private void OnDisable()
        {
            localMapInputActions.Disable();
        }

        private void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            var mousePosition = localMapInputActions.mousePositionAction.ReadValue<Vector2>();
            Debug.Log(mousePosition);
            HandleMouseHover(mousePosition);
            
            if (localMapInputActions.leftClickAction.IsPressed())
            {
                LeftClick(mousePosition);
            }
            else if (localMapInputActions.rightClickAction.WasPressedThisFrame())
            {
                RightClick(mousePosition);
            }
        
            if (toolManager.ActiveTool != null && !uiTooltipService.IsUiHover)
            {
                UpdateActiveToolTooltip(mousePosition);
            }
        }
        
        private void LeftClick(Vector3 mousePosition)
        {
            if (uiLayerManager.IsOverInterfaceUI(mousePosition))
            {
                return;
            }
        
            var inputRay = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                if (toolManager.ActiveTool != null)
                {
                    toolManager.ActiveTool.Apply(new Vector2(mousePosition.x, mousePosition.z));
                }
            }
        }

        private void RightClick(Vector3 mousePosition)
        {
            if (uiLayerManager.IsOverInterfaceUI(mousePosition))
            {
                return;
            }

            toolManager.CleanActiveTool();
        }

        private void HandleMouseHover(Vector3 mousePosition)
        {
            if (toolManager.ActiveTool != null)
            {
                return;
            }
        
            if (mouseHoverTimeCurrent > 0)
            {
                mouseHoverTimeCurrent -= Time.deltaTime;
                return;
            }
            mouseHoverTimeCurrent = MouseHoverUpdateInterval;
        
            if (uiLayerManager.IsOverInterfaceUI(mousePosition))
            {
                ClearHover();
                return;
            }
            
            var inputRay = Camera.main.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(inputRay, out var hit))
            {
                return;
            }
            
            var interactable = hit.collider.GetComponent<InteractableView>();
            if (interactable != currentHoverTarget)
            {
                ClearHover();
                currentHoverTarget = interactable;
            }
        
            if (currentHoverTarget != null)
            {
                hoverAccumulatedTime += mouseHoverTimeCurrent;
                // Показываем подсказку только после задержки
                if (hoverAccumulatedTime >= TooltipShowDelay && !uiTooltipService.IsActive)
                {
                    ShowTooltip(mousePosition, currentHoverTarget);
                }
            }
        }
    
        private void ClearHover()
        {
            if ((currentHoverTarget != null || uiTooltipService.IsActive) && !uiTooltipService.IsUiHover)
            {
                uiTooltipService.Hide();
                currentHoverTarget = null;
                hoverAccumulatedTime = 0f;
            }
        }
    
        private void ShowTooltip(Vector3 mousePosition, InteractableView interactable)
        {
            var tooltipInstance = interactable.GetTooltip();
            uiTooltipService.ShowAtScreenPosition(tooltipInstance, mousePosition, tooltipConfig);
        }
    
        private void UpdateActiveToolTooltip(Vector3 mousePosition)
        {
            var (tooltipData, cached) = toolManager.ActiveTool!.CreateTooltipData();
            
            if (!uiTooltipService.ShouldRedraw && cached)
            {
                uiTooltipService.UpdatePosition(mousePosition, tooltipConfig);
            }
            else
            {
                uiTooltipService.ShowAtScreenPosition(tooltipData, mousePosition, tooltipConfig);
            }
        }
    }
}