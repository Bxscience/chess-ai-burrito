using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingButton : MonoBehaviour
    
{
    public static bool playerAI2;
    void Update() {
        Cursor.visible = true;
    }
    public void Button1() {
        playerAI2 = true;
        SceneManager.LoadScene("GameScene");
    }
    public void Button2() {
        playerAI2 = false;
        SceneManager.LoadScene("GameScene");
    }
}