# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Lucid** is a Unity 5.4.0f3 3D adventure/puzzle game prototype built around a light-vs-darkness survival mechanic. The player (a boy character) must stay within light sources or hold illuminated objects (matches, candle spheres) to survive. Extended time in darkness triggers a fade-to-black death and checkpoint respawn.

## Development Environment

This is a Unity project — there are no CLI build or test commands. All building, running, and testing is done through the **Unity Editor (version 5.4.0f3)**. Open `Assets/Scenes/Prototype2/` scenes in the editor to work with the active game.

Active scenes (in play order):
- `SplashScreen` → `RootMenu` → `Hospital` → `Level1` → `Stasis` → `VerticalSlice`

Test/dev scenes: `PlayerTest.unity`, `TestRayCasterScene.unity`

## Code Architecture

All scripts are C# MonoBehaviours under `Assets/Scripts/`. The `Assets/Scripts/Unused/` folder contains ~20 deprecated scripts — ignore them.

### Core Systems

**`GameStats.cs`** (`Scripts/General/`) — The global game state singleton. Uses `DontDestroyOnLoad` to persist across scene loads. Owns: player HP, match count, current checkpoint position, and target level. Also renders the match HUD. Everything that needs to persist between scenes goes here.

**`PlayerController.cs`** (`Scripts/General/`, 925 lines) — Monolithic player script. Handles movement/jumping/crouching/climbing, the match draw-and-throw mechanic (with 0.67s animation delay), object pickup (right-click), cube pushing, and the death timer. Reads match count from `GameStats`. Animation states are managed via an internal enum to prevent mid-animation interruption.

**`RayCastLight.cs`** (`Scripts/General/`) — Attached to every light source in the scene. Each frame, raycasts toward the player and checks angle against a configurable cone threshold. Increments/decrements the player's `InLightSource` counter on enter/exit. The player is safe if `InLightSource > 0` OR `holdingMatch == true`; otherwise a 10-second death countdown begins.

**`Cam.cs`** (`Scripts/Camera/`) — Third-person chase camera with zoom support. Follows the player with a configurable offset. Switch between standard and zoomed states at runtime.

**`FadeScript.cs`** (`Scripts/Camera/`) — Screen fade-to-black/clear. Used by death sequences and scene transitions.

### Level Scripts

Each level has its own script under `Scripts/Hospital/`, `Scripts/Level1/`, `Scripts/Stasis/`, `Scripts/VerticalSlice/`. Level scripts handle scene-specific triggers, elevator sequences, and progression logic. They typically call `GameStats` to set the next level and `FadeScript` to transition.

### Key Patterns

- **Light safety counter:** `inLightSource` (int on the player) is incremented by each `RayCastLight` that can see the player. Never set it directly — always increment/decrement via `RayCastLight`.
- **Scene transitions:** Always go through `FadeScript` (fade out) → `GameStats.LoadLevel()` → `FadeScript` (fade in) in the new scene.
- **Runtime lookups:** Existing code uses `GameObject.Find()` extensively. Follow the same pattern when adding new cross-object references rather than introducing a different wiring mechanism.
- **Prefabs:** Player is `Prefabs/Boy.prefab`. Key interactables: `CandleSphere.prefab`, `CheckPoint.prefab`, `Darkness.prefab`.
