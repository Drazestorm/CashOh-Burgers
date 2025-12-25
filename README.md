# CashOh Burrger 🍔

![Logo](/images/Logo.png)

A 3D kitchen cooking arcade game built with Unity where players manage kitchen tasks, prepare dishes, and serve orders under time pressure.

### Main Screen
![Main_menu](/images/Screenshot%202025-12-22%20at%203.52.33 PM.png)

### In game UI
![in_game_ui](/images/Screenshot%202025-12-22%20at%203.54.43 PM.png)

---

## 📖 For Users

### Overview
CashOh Burrger is an exciting cooking simulation game where you take on the role of a chef in a busy kitchen. Interact with various kitchen counters, prepare ingredients, cook delicious meals, and deliver orders to satisfy your customers!

### Game Features
- **Interactive Kitchen Environment**: Work with multiple counter types (cutting, stove, plates, trash, etc.)
- **Dynamic Camera System**: Smart camera that tracks and follows the player throughout the kitchen
- **Dynamic Cooking System**: Cut ingredients, cook on stoves, and assemble plates
- **Order Management**: Receive and fulfill customer orders with time limits
- **Score System**: Track your performance and completed deliveries
- **Sound Effects & Music**: Immersive audio experience with background music and interactive sounds
- **Visual Feedback**: Animated counters, progress bars, and UI elements
- **Countdown Timer**: Race against the clock to complete as many orders as possible

<!-- 
PUT GAMEPLAY FEATURES SCREENSHOT HERE
Example: ![Game Features](./docs/images/features.png)
-->

### How to Play

#### Controls
- **Movement**: WASD or Arrow Keys
- **Interact**: E
- **Chopping**: F

#### Gameplay
1. Start the game and wait for the countdown
2. Pick up ingredients from designated counters
3. Process ingredients (cut, cook, etc.) at the appropriate stations
4. Assemble complete dishes on plates
5. Deliver finished orders to the delivery counter
6. Complete as many orders as possible before time runs out!

<!-- 
PUT CONTROLS/UI SCREENSHOT HERE
Example: ![Game Controls](./docs/images/controls.png)
-->

### System Requirements
- **OS**: Windows 10/11, macOS 10.13+
- **Graphics**: DirectX 11 or Metal capable GPU
- **Storage**: 500 MB available space
- **Additional**: Keyboard required for input

### Installation
Go to my itch.io, you can play it on the web: "https://drazestorm.itch.io/cashohs-burgers"

---

## 🛠️ For Developers

### Project Information
- **Engine**: Unity 2022.3+ (LTS recommended)
- **Render Pipeline**: Universal Render Pipeline (URP)
- **C# Version**: 9.0
- **Target Platform**: PC (Windows, macOS, Linux)

### Technologies Used
- Unity Input System
- Cinemachine (Camera management)
- TextMesh Pro (UI text rendering)
- Unity AI Navigation (NavMesh)
- Unity Burst Compiler (Performance optimization)
- Visual Scripting

### Project Structure
```
Assets/
├── Scripts/
│   ├── Player/           # Player movement and animations
│   ├── Counter/          # Various counter types (Cutting, Stove, Plate, etc.)
│   ├── Manager/          # Game managers (GameManager, DeliveryManager, etc.)
│   ├── UI/               # UI components and controllers
│   ├── KitchenObjects/   # Kitchen object classes
│   ├── Sounds/           # Audio management
│   └── CounterVisual/    # Visual effects for counters
├── Prefabs/              # Game object prefabs
├── Scenes/               # Game scenes
├── Materials/            # 3D materials
├── Animators/            # Animator controllers
├── ScriptableObjects/    # SO instances (recipes, audio clips, etc.)
├── SOScripts/            # ScriptableObject definitions
└── _Assets/              # External assets (models, textures, audio)
```

### Key Systems

#### Core Gameplay
- **Player Controller**: Handles player movement, interaction, and object handling
- **Counter System**: Modular counter types inheriting from `BaseCounter`
  - Cutting Counter
  - Stove Counter
  - Plate Counter
  - Container Counter
  - Trash Counter
  - Delivery Counter

#### Game Management
- **KitchenGameManager**: Controls game states (Waiting, Countdown, Playing, GameOver)
- **DeliveryManager**: Manages recipe orders and delivery system
- **SoundManager**: Centralized audio control with volume settings

#### Object System
- **KitchenObject**: Base class for all interactive kitchen items
- **IKitchenObjectParent**: Interface for objects that can hold kitchen items
- **ScriptableObject-based Recipes**: Data-driven recipe system

### Setup Instructions

#### Prerequisites
- Unity Hub installed
- Unity Editor 2022.3 LTS or later
- Git (for version control)

#### Getting Started
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Kitchen-Arcade-3D
   ```

2. Open Unity Hub and add the project

3. Open the project in Unity Editor

4. Open the main scene:
   - Navigate to `Assets/Scenes/`
   - Open the main game scene

5. Press Play in the Unity Editor to test

### Building the Game

#### Windows Build
1. File → Build Settings
2. Select "Windows, Mac, Linux"
3. Choose "Windows" as target platform
4. Click "Build" and select output folder

#### macOS Build
1. File → Build Settings
2. Select "Windows, Mac, Linux"
3. Choose "macOS" as target platform
4. Click "Build" and select output folder

### Code Style Guidelines
- Use PascalCase for public methods and properties
- Use camelCase for private fields
- Prefix interfaces with "I" (e.g., `IKitchenObjectParent`)
- Use events for decoupled communication between systems
- Document complex logic with XML comments

### Architecture Patterns
- **Event-Driven**: Heavy use of C# events for loose coupling
- **Inheritance**: Base classes for counters and kitchen objects
- **Interfaces**: For shared behavior (e.g., `IKitchenObjectParent`)
- **Singleton Pattern**: Used for managers (GameManager, DeliveryManager, SoundManager)
- **ScriptableObject Pattern**: For data-driven design (recipes, settings)

### Contributing
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Submit a pull request

### Known Issues
- List any known bugs or limitations here

### Future Enhancements
- Multiplayer support
- Additional recipes and ingredients
- More kitchen equipment types
- Level progression system
- Customizable kitchens

---

## 📝 License
[Add your license information here]

## 🙏 Credits
- **CodeMonkey**: Tutorial assets and base implementation
- **Unity Technologies**: Unity Engine and packages

## 📧 Contact
[Add your contact information here]

---

### 📸 Image Placement Guide

Create a `docs/images/` folder in your project root and place images as follows:

1. **banner.png** (Line 3): Main game logo/banner - 1200x400px
2. **gameplay-1.png** (Line 10): Main gameplay screenshot - 1920x1080px
3. **features.png** (Line 29): Features showcase - 1920x1080px
4. **controls.png** (Line 46): Controls/UI reference - 1280x720px

Then uncomment the image lines in the README and update the paths.
