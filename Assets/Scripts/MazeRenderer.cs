
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    private Transform wall = null;

    [SerializeField]
    private Transform floor = null;

    [SerializeField]
    private Transform coin = null;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate();
        DrawInitialMaze(maze);
        PlaceCoin();
    }
    private void PlaceCoin()
    {
        var location = new Vector3(-5, 0, 0);// position of the coin
        var c = Instantiate(coin, transform) as Transform;
        coin.position = location;
    }
    //Draw the initial maze structure with all posible walls
    private void DrawInitialMaze(Statuss[,] maze)
    {
        for (int i=0; i < 10; i++)
        {
            for (var j=0; j < 10; j++)
            {
                var location = new Vector3(-5 + i, 0, -5 + j);// position of each cell is ofset from center
                var cell = maze[i, j];
                var bottom  = Instantiate(floor, transform) as Transform;
                bottom.position = location;
               

                if (j == 0) //last vertical wall
                {
                    if (cell.HasFlag(Statuss.D))
                    {
                        var down = Instantiate(wall, transform) as Transform;
                        down.localScale = new Vector3(1, down.localScale.y, down.localScale.z);
                        down.position = location + new Vector3(0, 0, -0.5f);
                    }
                }
                if (cell.HasFlag(Statuss.U)) //all vertical walls
                {
                    var up = Instantiate(wall, transform) as Transform;
                    up.localScale = new Vector3(1, up.localScale.y, up.localScale.z);
                    up.position = location + new Vector3(0, 0, 0.5f);
                  
                }
                if (i == 9)//last horizontal wall
                {
                    if (cell.HasFlag(Statuss.R))
                    {
                        var right = Instantiate(wall, transform) as Transform;
                        right.eulerAngles = new Vector3(0, 90, 0);
                        right.position = location + new Vector3(+0.5f, 0, 0);
                        right.localScale = new Vector3(1, right.localScale.y, right.localScale.z);
                    }
                }
                if (cell.HasFlag(Statuss.L))//all horizontal walls
                {
                    var left = Instantiate(wall, transform) as Transform;
                    left.eulerAngles = new Vector3(0, 90, 0);
                    left.position = location + new Vector3(-0.5f, 0, 0);
                    left.localScale = new Vector3(1, left.localScale.y, left.localScale.z);                 
                    
                }
            
            }
        }

    }
 
}
