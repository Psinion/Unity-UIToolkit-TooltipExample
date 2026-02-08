# UIToolkit Tooltip Example

![example](https://github.com/Psinion/Unity-UIToolkit-TooltipExample/blob/main/Images/Example.gif)

## About the Project

Optimized tooltip system for Unity UIToolkit, designed with strategy and colony simulator games in mind. It powers the UI in **[Covenant of Ascension](https://t.me/covenant_of_ascension)** (WIP, in Russian).

## Features

- **Multiple Tooltip Types:** Predefined templates for buttons, world objects, and a dynamic cursor-following tooltip.
- **Smart Positioning:** Supports four fixed positions (top, bottom, left, right) and an **auto-positioning mode** to prevent off-screen clipping.
- **Unified System:** Works seamlessly for both **UI elements** and **world-space objects**.
- **Real-time Validation:** Dynamic tooltips can check resources/costs and provide instant visual feedback (e.g., text turns red if unaffordable).
- **Performance-Optimized:** Updates only on hover or data change, not every frame.
- **Clean Architecture:** Clear separation between data, logic, and presentation for easy extension.

## How It Works

- **TooltipService:** The main manager. Creates tooltips and positions them on screen based on a `TooltipConfig`.
- **TooltipTrigger:** Attached to UI buttons. Handles the cursor enter/exit logic to show/hide tooltips.
- **TooltipsFactory & ITooltipData:** The factory acts as a dispatcher. It takes `ITooltipData` (the model) and creates the corresponding `ITooltipInstance` (the view controller).
- **ITooltipInstance:** Responsible for populating a specific VisualElement (defined in a UXML template) with data from `ITooltipData`.
- **MainController:** In the example, this component intercepts player mouse input for world interactions.
- **InteractableView:** A base MonoBehaviour class for world-space GameObjects to define and display their tooltips. Can be replaced with a custom interface implementation.

## Tech Stack

- Unity 6000.3.4f1
- C# 10
- UIToolkit

## Tags

unity, ui toolkit, tooltip, hover, uidocument, uxml, uss, visualelement, runtime