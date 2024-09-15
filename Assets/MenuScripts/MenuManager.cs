using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Option()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ExitGame()
    {
        Debug.Log("已退出游戏");
    }    
    
    public void Mobile()
    {
        SceneManager.LoadScene(3);
        Debug.Log("已进MobileJoyTest界面");
    }
}
