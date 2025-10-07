University of Belgrade, School of Electrical Engineering
# Paper: Techniques for Drawing Clouds

This project is the practical implementation for a seminar paper on the topic **"Techniques for Drawing Clouds"**. The goal of the project was to explore and demonstrate the capabilities of real-time atmospheric phenomena visualization using the **Unity** engine and its **High Definition Render Pipeline (HDRP)**.

The application is an interactive scene that provides the user with full programmatic control over the appearance of volumetric clouds and the dynamic lighting of the scene.

## Key Features

-   **Dynamic Cloud System:** Allows switching between 7 predefined weather profiles (0-6) using the numeric keypad. The modes include:
    -   **Clear Weather (1-3):** White, puffy, and translucent clouds with varying densities and sky coverage.
    -   **Overcast Weather (4-6):** Grey, dense, and dark clouds simulating rainy conditions at different altitudes.
    -   **No Clouds (0):** Completely disables the cloud system for a clear sky.

-   **Dynamic Day Cycle:** Full control over the Sun (`Directional Light`).
    -   **Automated Animation (R):** A smooth 10-second animation from sunrise to sunset, featuring realistic changes in sky color and light intensity.
    -   **Manual Positioning:** Instantly set the sun to key positions: Sunrise (`P`), Morning (`O`), Noon (`I`), Afternoon (`U`), and Sunset (`Y`).

-   **Free Camera:** Complete freedom of movement within the 3D scene.
    -   Movement with `WASD` and `QE` keys.
    -   Rotation with the arrow keys.
    -   Movement speed control with `+` and `-` on the numeric keypad.
    -   Zoom by changing the Field of View (FOV) with the mouse scroll wheel.

-   **User Interface:** A persistent UI in the top-left corner displays all available commands and the current camera movement speed.

-   **Realistic Scene:** The scene features a seemingly infinite ground plane with a non-reflective grass texture that receives dynamic shadows from the clouds and changes color based on the time of day.

## Controls

| Action | Key(s) |
| -------------------------- | ------------------------------------- |
| **Movement** | `W`, `A`, `S`, `D` |
| **Up / Down** | `E` / `Q` |
| **Camera Rotation** | `←`, `↑`, `→`, `↓` (Arrow Keys) |
| **Zoom** | Mouse Scroll Wheel |
| **Increase/Decrease Speed** | `+` / `-` (on the Numpad) |
| **Change Cloud Mode** | `0` - `6` (on the Numpad) |
| **Animate Day Cycle** | `R` |
| **Set Sun: Sunrise** | `P` |
| **Set Sun: Morning** | `O` |
| **Set Sun: Noon** | `I` |
| **Set Sun: Afternoon** | `U` |
| **Set Sun: Sunset** | `Y` |


