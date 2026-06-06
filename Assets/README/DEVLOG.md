# DEVLOG.md

## Overview

This project is a prototype memory-card matching game built in Unity 6.3 LTS. The primary goal was to satisfy the gameplay and engineering requirements while keeping the implementation small, maintainable, and deterministic.

The largest focus during development was correctness under rapid user interaction, as the assignment explicitly requires continuous input and resilience against race conditions.

---

## Major Decisions

### 1. Deterministic Board Generation

I implemented board generation using a seeded `System.Random` instance and Fisher-Yates shuffle.

Reasons:

* Same seed always produces the same board layout.
* Simplifies debugging and testing.
* Makes save/load easier because the board can be reconstructed from saved state.

For odd-sized boards (3×3 and 5×5), I chose to use a non-playable blocker cell rather than introducing an unmatched card. This preserves the pair-matching gameplay model and avoids edge cases around win conditions.

---

### 2. Separation of View and Gameplay Logic

I separated card presentation from game-state management.

Responsibilities:

* CardView: rendering, input handling, animations.
* CardController/GameController: matching, score management, board state, and game flow.

This reduced coupling between visual behaviour and game rules and made the core logic easier to test.

---

### 3. Manual Animation Implementation

The assignment prohibited tween libraries, so card animations were implemented using coroutines and AnimationCurves.

Reasons:

* Full control over timing.
* No external dependencies.
* Easy to tune animation feel through the inspector.

Sprite swapping occurs at the midpoint of the rotation rather than at a fixed timestamp to ensure correct visual behaviour regardless of the chosen animation curve.

---

### 4. Save System

The save system stores only gameplay-relevant data.

Persisted state includes:

* Board dimensions
* Seed
* Card states
* Matched cards
* Score
* Current game progress

Transient animation states are not persisted because they can be reconstructed when gameplay resumes.

---

### 5. Dynamic Board Sizes

The game supports multiple layouts:

* 2×2
* 2×3
* 3×3
* 4×3
* 4×4
* 5×3
* 5×4
* 5×5

Board generation and layout scaling are data-driven rather than hardcoded per difficulty.

---

## Challenges

### Continuous Input

The most difficult requirement was supporting rapid user interaction without breaking game state.

The final implementation prevents duplicate state transitions while allowing interaction to remain responsive.

---

### Odd-Sized Boards

Memory games are naturally pair-based, so odd-sized layouts required a design decision.

I chose to include a non-playable blocker tile because it keeps pair counts consistent and simplifies completion checks.

---

## Approach I Abandoned

I considerered writing multiple singletons as manager scripts to handle different aspects of the game (e.g. AudioManager, UIManager, GameManager). 
However, I ultimately decided against this approach and instead opted for a more modular design with clear separation of concerns. 
This decision was driven by the desire to avoid tight coupling and improve maintainability, especially as the project grows in complexity.

---

## If Given More Time

I would improve:

* Additional unit test coverage around save/load and edge cases.
* More polished feedback through audio and visual effects.
* Improved mobile-specific UI scaling and accessibility options.

---
