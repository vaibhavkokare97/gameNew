# ARCHITECTURE.md

## Overview

The project follows a simple controller-based architecture.

The game is divided into three main parts:

* GameController - manages overall game flow.
* CardController - generates the board and handles matching logic.
* CardView - handles card visuals, input, and flip animations.

This separation keeps gameplay logic independent from presentation logic and makes the code easier to maintain and test.

## Data Flow

```text
Player Click
    ↓
CardView
    ↓
CardController
    ↓
Match Check
    ↓
Score Update / Card Removal
```

## Board Generation

Board layouts are generated using a seeded `System.Random` instance and Fisher-Yates shuffle.

This ensures:

* Same seed always generates the same layout.
* Different seeds generate different layouts.
* Board generation is deterministic and easy to test.

## Save System

The save system stores:

* Selected board size
* Current score
* Card states
* Seed

The game automatically saves when the application is paused and restores the previous state when reopened.

## Testing

EditMode unit tests were written for:

* Match validation
* Score calculation
* Board generation

These tests focus on gameplay logic rather than visual behaviour.

## Trade-Off

For simplicity, some systems communicate through direct references instead of events.

This reduced implementation complexity and development time, but if the project were expanded, I would move to a more event-driven approach to reduce coupling between systems.
