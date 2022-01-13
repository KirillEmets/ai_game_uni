using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{
    private PlayerController Player { get; set; }

    public Text arrowCountText;
    
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        UpdateArrowCount(Player.ArrowCount);
        Player.OnArrowCountChange += UpdateArrowCount;
    }

    void UpdateArrowCount(int count)
    {
        arrowCountText.text = count.ToString();
    }
}
