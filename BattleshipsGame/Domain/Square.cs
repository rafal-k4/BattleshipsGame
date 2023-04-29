﻿using BattleshipsGame.Domain.Core;
using OneOf;

namespace BattleshipsGame.Domain;

public class Square
{
    internal SquareState State { get; private set; }
    internal int Row { get; }
    internal int Column { get; }

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        State = SquareState.Empty;
    }

    internal void Occupy()
    {
        State = SquareState.Occupied;
    }

    internal OneOf<Hit, Miss, AlreadyHit> HitAndGetResult()
    {
        if (State == SquareState.Empty)
            return new Miss();

        if (State == SquareState.Occupied)
            return new Hit();

        return new AlreadyHit();
    }

    internal char GetDisplayChar(bool shouldDisplayOccupied)
    {
        switch (State)
        {
            case SquareState.Empty:
                return '-';
            case SquareState.Occupied:
                return shouldDisplayOccupied
                    ? 'O'
                    : '-';
            case SquareState.Hit:
                return 'X';
            case SquareState.Missed:
                return '*';
            default:
                throw new InvalidOperationException("Invalid square state.");
        }
    }
}

public enum SquareState
{
    Empty,
    Occupied,
    Hit,
    Missed
}