using System;
using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Core;
using UIToolkit.Tooltip.Example.Data.Enums;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Interaction;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances;
using UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UIToolkit.Tooltip.Example.Data.Factories
{
    public class FiguresFactory : SingletonMono<FiguresFactory>
    {
        // Here may be your approach for prefabs loader (Resources, Addressables, etc.)
        [SerializeField] 
        private InteractableView cube;
        
        [SerializeField] 
        private InteractableView sphere;
        
        [SerializeField] 
        private InteractableView capsule;

        private Dictionary<string, InteractableView> figures = new();

        [SerializeField]
        private Transform spawnNode;
        
        private void OnValidate()
        {
            if (cube == null)
            {
                Debug.LogError("Cube is not found.");
            }
            
            if (sphere == null)
            {
                Debug.LogError("Sphere is not found.");
            }
            
            if (capsule == null)
            {
                Debug.LogError("Capsule is not found.");
            }
        }
        
        private void Start()
        {
            figures[nameof(FigureType.Cube)] = cube;
            figures[nameof(FigureType.Sphere)] = sphere;
            figures[nameof(FigureType.Capsule)] = capsule;
        }

        public InteractableView CreateFigure(string figureType, Vector3 position)
        {
            if (!figures.TryGetValue(figureType, out InteractableView figure))
            {
                Debug.LogError("Figure is not found.");
                return null;
            }
            
            var figureInstance = Instantiate(figure, spawnNode);
            figureInstance.transform.position = position;
            
            return figureInstance;
        }
    }
}