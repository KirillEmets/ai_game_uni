using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{
    private PlayerController Player { get; set; }

    public Text arrowCountText;
    public Text killCounterText;
    public GameObject deathScreen;

    
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        UpdateArrowCount(Player.ArrowCount);
        
        Player.OnDeathEvent += OnPlayerDeath;
        Player.OnArrowCountChange += UpdateArrowCount;
    }

    void UpdateArrowCount(int count)
    {
        arrowCountText.text = count.ToString();
    }
    
    void OnPlayerDeath()
    {
        deathScreen.SetActive(true);
    }

    public void UpdateKillCounter(int count)
    {
        killCounterText.text = count.ToString();
    }
}
