using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSlider : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public TMP_Text text;
    public static int game_depth;
    // Start is called before the first frame update
    void Update()
    {
        text.text = "Depth: " + (int) slider.value;
        game_depth = (int) slider.value;
    }
}
