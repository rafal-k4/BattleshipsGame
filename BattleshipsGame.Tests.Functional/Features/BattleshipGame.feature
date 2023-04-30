Feature: BattleshipGame

Battleship offers a user to play against computer in one-sided game mode. 
User goal is to provide a valid coordinates that are targeting computer ships.
When all ships are sunk, game is over

Scenario: Created game should display empty board
	Given game is created with given seed 420
	When game board is returned
	Then board should looks like following
	| Board                 |
	| 0 1 2 3 4 5 6 7 8 9   |
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
