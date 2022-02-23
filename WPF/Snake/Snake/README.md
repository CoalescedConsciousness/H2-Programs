### __Version 1.0.2__

## Change Log:
[1.0.2]
- Minor adjustments, but otherwise considered presentation-ready.
  - Worth noting that despite it not being apparent in this verion, attempts were made to subdivide this project into smalle segments (i.e. OOP)
    However, due to the inherent link that WPF makes with code-behind, this quickly becomes all but impossible, 
    as the g.i.cs file is reinstantiated on every run
    - My efforts were met with minor success, albeit glaring issues were still present, 
      such as when code-behind was tasked with populating the view, as opposed to it inherently being there in the XAML.

[1.0.1]
- Added logic for deleting and restoring highscore lists.
- Added logic for pausing and resuming certain soundbites at certain intervals.

[Initial Commit]
- Added different boards for greater challenge.
- Added various sound effects and music for greater entertainment.
- Added persistent storage for storing Highscore data.
- Pause now correctly pauses the game.
