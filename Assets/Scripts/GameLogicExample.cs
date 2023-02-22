

/******
 * Author: Santa Bartuðçvica
 * Summary: Game logic for the example scene. 
 * Includes scene switching after collecting the coin
 */

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLogicExample : MonoBehaviour
{
  
    //Go to the next scene after collecting the coin
    public void GetCoin()
    {
        SceneManager.LoadScene(2);
    }
 

}
