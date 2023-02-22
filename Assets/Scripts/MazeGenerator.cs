/******
 * Author: Santa Bartuðçvica
 * Summary: Maze generator for the labyrinth test. 
 * Includes maze generation using recursive backtracking algorithm.
 */
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

//coordinates
public struct Location
{
    public int X;
    public int Y;
}

//Neighbour cell parameters
public struct Neighbour
{
    public Location Location;
    public Statuss SharedWall;
}

//generate maze visit all unvisited cells. Choose a random unvisited neighbour cell. Iteratively visit all cells
public static class MazeGenerator
{  
    private static Statuss[,] ApplyBacktracker(Statuss[,] maze)
    {
        var random = new System.Random();
        var positionStack = new Stack<Location>();
        var position = new Location { X = random.Next(0, 10), Y = random.Next(0, 10) };

        maze[position.X, position.Y] |= Statuss.VISITED;  // 1000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randomIndex = random.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randomIndex];

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
    
    // Returns list of unvisited neighbours
    private static List<Neighbour> GetUnvisitedNeighbours(Location pos, Statuss[,] maze)
    {
        var list = new List<Neighbour>();

        if (pos.X > 0) // left
        {
            if (!maze[pos.X - 1, pos.Y].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location {X = pos.X - 1,Y = pos.Y},
                    SharedWall = Statuss.L
                });
            }
        }

        if (pos.Y > 0) // DOWN
        {
            if (!maze[pos.X, pos.Y - 1].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location{X = pos.X,Y = pos.Y - 1},
                    SharedWall = Statuss.D
                });
            }
        }

        if (pos.Y < 9) // UP
        {
            if (!maze[pos.X, pos.Y + 1].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location{X = pos.X,Y = pos.Y + 1},
                    SharedWall = Statuss.U
                });
            }
        }

        if (pos.X < 9) // RIGHT
        {
            if (!maze[pos.X + 1, pos.Y].HasFlag(Statuss.VISITED))
            {
                list.Add(new Neighbour
                {
                    Location = new Location{X = pos.X + 1,Y = pos.Y},
                    SharedWall = Statuss.R
                });
            }
        }

        return list;
    }

    //Initial maze with all walls (statuss 1111) and apply backtracking algorithm
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

