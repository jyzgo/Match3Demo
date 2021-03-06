﻿using UnityEngine;
using System.Collections;


public static class Constants
{
	public const int MAX_ROWS = 7;
	public const int MAX_COLS = 9;
	public const float CELL_SIDE = 1.4f;
	public const float CELLS_LEFT = -5.65f;
	public const float CELLS_BOTTOM = -4.7f;
	public const float SWAP_TIME = 0.3f;
	public const float FORM_TIME = 0.33f;

	public const float UNIT_ELIM_TIME = 0.13f;

	public const int CORLOR_NUM = 7;

	public const bool DEBUG_MODE = true;
}

public enum UnitColor {
	None = 0,
	Red  = 1,
	Blue = 2,
	Green = 3,
	Brown = 4,
	Purple = 5,
	Yellow = 6,
	All
};

public enum UnitType
{
	None,
	Brick,
	Bomb
}

public static class Zorders
{
	public const float CellZorder = 0f;
	public const float UnitZorder = -1f;
}

public enum BombType
{
	None,
	Horizontal,
	Vertical,
	Square,
	Fish,
	Color,
	Coloring
}