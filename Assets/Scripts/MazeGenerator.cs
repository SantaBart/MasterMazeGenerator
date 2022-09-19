using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Statuss
{
    // 0000 -> NO WALLS
    // 1111 -> LEFT,RIGHT,UP,DOWN
    L = 1, // 0001 LEFT
    R = 2, // 0010 RIGHT
    U = 4, // 0100 UP
    D = 8, // 1000 DOWN

    VISITED = 128, // 1000 0000
}

public struct Location
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Location Location;
    public Statuss SharedWall;
}

public static class MazeGenerator
{
    

    private static Statuss[,] ApplyBacktracker(Statuss[,] maze)
    {
        // here we make changes
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Location>();
        var position = new Location { X = rng.Next(0, 10), Y = rng.Next(0, 10) };

        maze[position.X, position.Y] |= Statuss.VISITED;  // 1000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Location;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOpposite(randomNeighbour.SharedWall);
                maze[nPosition.X, nPosition.Y] |= Statuss.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }
    //Get opposite wall
    private static Statuss GetOpposite(Statuss wall)
    {

        if (wall == Statuss.R) { return Statuss.L; }
        else if (wall == Statuss.D) { return Statuss.U; }
        else if (wall == Statuss.L) { return Statuss.R; }
        else if (wall == Statuss.U) { return Statuss.D; }
        else return Statuss.L;

    }
    private static List<Neighbour> GetUnvisitedNeighbours(Location p, Statuss[,] maze)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) // left
        {
            if (!maze[p.X - 1, p.Y].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = Statuss.L
                });
            }
        }

        if (p.Y > 0) // DOWN
        {
            if (!maze[p.X, p.Y - 1].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = Statuss.D
                });
            }
        }

        if (p.Y < 9) // UP
        {
            if (!maze[p.X, p.Y + 1].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = Statuss.U
                });
            }
        }

        if (p.X < 9) // RIGHT
        {
            if (!maze[p.X + 1, p.Y].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = Statuss.R
                });
            }
        }

        return list;
    }

    public static Statuss[,] Generate()
    {
        Statuss[,] maze = new Statuss[10, 10];
        Statuss initial = Statuss.R | Statuss.L | Statuss.U | Statuss.D;
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                maze[i, j] = initial;  // 1111
            }
        }

        return ApplyBacktracker(maze);
    }
}

