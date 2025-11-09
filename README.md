# RPG-Adventure

## Project Objective

The objective of this project is to design and develop a small but feature-complete Unity game prototype that simulates a simple RPG adventure experience. The game will integrate essential gameplay systems such as player movement, camera control, dialogue interactions, menus, and a basic saving/loading system. The project’s goal is to provide a cohesive gameplay experience where players can explore, interact, and progress through the adventure. The final product should highlight the development of a functional prototype that incorporates these systems seamlessly to create an engaging RPG experience.

## Team Members

* Tyler.
* April.
* John.

## Features 

### Menu and Options Menu (April)

- [ ] The game will feature a main menu allowing players to start a new game, load a saved game, or exit.
- [ ] The options menu will enable players to adjust sound settings, graphic preferences, and gameplay settings.

### Saving and Loading Options (April)

- [ ] The game will allow players to save their menu preferences, such as sound settings, graphic settings, and key bindings.
- [ ] These settings will persist across sessions, ensuring that the player’s preferred configurations are automatically loaded when the game starts.
- [ ] The system will store these preferences in a file format, enabling easy retrieval and modification.

### Player Movement (John)

- [ ] The player can move freely within the game world using keyboard inputs (WASD or arrow keys).
- [ ] The Player can change speeds between crouch and sprint.
- [ ] Movement is smooth and responsive, with a simple collision detection system to prevent the player from passing through objects.

### Camera Control (John)

- [ ] The camera will follow the player, providing a dynamic perspective of the game world.
- [ ] The camera's position and rotation will adjust smoothly as the player moves, ensuring a clear and focused view of the action.
- [ ] The player can optionally adjust the camera angle and zoom in/out using the mouse.

### Dialogue System (Tyler)

- [ ] Players can read through text-based responses and make choices that affect the conversation.
- [ ] Dialogue options will trigger different responses or actions based on player decisions.

### Interaction (Tyler)

- [ ] The player can interact with NPCs, objects, and environments through simple prompts.
- [ ] Interactions will trigger actions such as opening doors, picking up items, or starting dialogues.
- [ ] The interaction system will be simple and context-sensitive, adapting to the object or NPC being interacted with.

### Stats and Levelling (John)

- [ ] The player will have stats such as health, experience points (XP), and level, which improve as they progress.
- [ ] The levelling system will improve stats as the player advances.

### Saving and Loading Stats (April)

- [ ] Player stats will be saved during the game save process.
- [ ] Stats will be stored in a file for easy management and retrieval when loading the game.
- [ ] Any changes to the player’s stats (such as levelling up) will be automatically saved.
- [ ] The player’s position, rotation, and other transform-related data will be saved when the game is saved.
- [ ] When the game is loaded, the player will return to their exact last location and state, preserving the continuity of gameplay.

### Respawn (Tyler)

- [ ] When the player’s character dies, they will respawn at a predefined location, such as a checkpoint or starting area.
- [ ] Respawn will restore the player's health to a default value.
- [ ] The respawn system will include a short delay before the player is returned to the game world to avoid instant re-engagement after death.
- [ ] The game will provide a visual or audio cue to indicate the respawn event, ensuring the player is aware of their return to the game.
- [ ] Player stats, including experience points and level, will remain intact upon respawn, maintaining the player’s progression despite death.

## Branching Convention

Further reading about [why Git workflow impacts almost everyone in the delivery journey](https://www.atlassian.com/git/tutorials/why-git).

Further reading about [GitFlow Workflow for Branching](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow).

Each **Branch Name** should reflect its purpose and promise.

**Main Branch**  
Format: - `main`  
Example: - main

**Developer Branches**  
Format: - `develope`  
Example: - develope

**Release Branches**  
Format: - `release/{version-number}`  
Example: - release/v1.2.0

**Feature Branch**  
Format: - `feature/{feature-name}`  
Example: - feature/user-authentication

**Hotfix Branches**  
Format: - `hotfix/{bug-description}`  
Example: - hotfix/fix-login-error

## Commit Convention

**[Mandatory]** Each **Git Commit Title** should start with one of these words, plus a brief statement of what it covers (50 characters maximum). 

- `Add` a new feature, story or file.
- `Change` an existing feature, story or file.
- `Refactor` to improve code with changing behaviour.
- `Deprecate` notice to remove a feature, story or file.
- `Remove` a feature, story or file. It is gone.
- `Fix` a Bug, Defect or unreasonable Limit.
- `Security` improvement against vulnerabilities.

**[Optional]** Each **Git Commit Description** should contain a brief list of changes included within the commit (72 characters maximum, per line). Be nice to your future self.

## Coding Convention

Following standard Microsoft C# conventions for [naming](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names#naming-conventions) and [coding style](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).

- PascalCase for class and method names; also known as **UpperCamelCase**.
- **lowerCamelCase** for variables.
- Clear, descriptive names for assets.

## Version Convention

Unity asset files should use a `_v1` style suffix.

Git and Application releases should use `Major.Minor.Patch.Early` [Semantic Versioning](https://www.datamates.wtf/wordpress/development/semantic-version/).
