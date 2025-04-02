using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    public GameObject GameManager;
    [SerializeField] public TMP_Text turns;
    [SerializeField] public TMP_Text state;
    
    void Start() {
        GameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        turns.text = "Turn: " + GameManager.GetComponent<GameManager>().turn;
        state.text = "State: " + GameManager.GetComponent<GameManager>().state;
    }
}
