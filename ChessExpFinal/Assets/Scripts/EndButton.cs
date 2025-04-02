using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
    
{
    public static bool playerAI2;
    void Update() {
        Cursor.visible = true;
    }
    public void ButtonEnd() {
        playerAI2 = true;
        SceneManager.LoadScene("ResignScene");
    }
}