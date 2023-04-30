Feature: BattleshipGame

Battleship offers a user to play against computer in one-sided game mode. 
User goal is to provide a valid coordinates that are targeting computer ships.
When all ships are sunk, game is over

Scenario: Created game should display empty board
	Given game is created with given seed 420
	When game board is returned
	Then board should looks like following
	| Board                 |
	|   0 1 2 3 4 5 6 7 8 9 |
	| A - - - - - - - - - - |
	| B - - - - - - - - - - |
	| C - - - - - - - - - - |
	| D - - - - - - - - - - |
	| E - - - - - - - - - - |
	| F - - - - - - - - - - |
	| G - - - - - - - - - - |
	| H - - - - - - - - - - |
	| I - - - - - - - - - - |
	| J - - - - - - - - - - |

Scenario: Ship was sunk
	Given game is created with given seed 420
	And coordinates are 'h1 h0 g1 i1 h2 h3 h4'
	When rockets shots are fired
	And game board is returned
	Then result should be 'sunk'
	And board should looks like following
	| Board                 |
	|   0 1 2 3 4 5 6 7 8 9 |
	| A - - - - - - - - - - |
	| B - - - - - - - - - - |
	| C - - - - - - - - - - |
	| D - - - - - - - - - - |
	| E - - - - - - - - - - |
	| F - - - - - - - - - - |
	| G - * - - - - - - - - |
	| H * X X X X - - - - - |
	| I - * - - - - - - - - |
	| J - - - - - - - - - - |

Scenario: All ship sunk - game is finished
	Given game is created with given seed 420
	And coordinates are 'h1 h2 h3 h4 a2 b2 c2 d2 e2 b3 c3 d3 e3' 
	When rockets shots are fired
	And game board is returned
	Then game status should be as finished
	And board should looks like following
	| Board                 |
	|   0 1 2 3 4 5 6 7 8 9 |
	| A - - X - - - - - - - |
	| B - - X X - - - - - - |
	| C - - X X - - - - - - |
	| D - - X X - - - - - - |
	| E - - X X - - - - - - |
	| F - - - - - - - - - - |
	| G - - - - - - - - - - |
	| H - X X X X - - - - - |
	| I - - - - - - - - - - |
	| J - - - - - - - - - - |

Scenario: Hit are missed
	Given game is created with given seed 420
	And coordinates are 'a0 a1 g4 g5'
	When rockets shots are fired
	Then result should be 'miss'

Scenario: Hit was in target
	Given game is created with given seed 420
	And coordinates are 'a2 b2 h1'
	When rockets shots are fired
	Then result should be 'hit'

Scenario: Hit was in the same place
	Given game is created with given seed 420
	And coordinates are 'a1 a1'
	When rockets shots are fired
	Then result should be 'AlreadyHit'

Scenario: Coordinates out of the board boundary
	Given game is created with given seed 420
	And coordinates are 'a50 z0'
	When rockets shots are fired
	Then result should be 'CoordinatesNotInRange'

Scenario: Invalid coordinates
	Given game is created with given seed 420
	And coordinates are '-- 00 aa ** {}'
	When rockets shots are fired
	Then result should be 'InvalidCoordinates'