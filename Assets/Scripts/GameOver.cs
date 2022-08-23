using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Transform player—oordinates;
    [SerializeField] private Transform startPoint;


    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //if (EventManager.GameOverEvent!= null)
        //{
        //    EventManager.GameOverEvent.Invoke();
        //}
    }

    private void Update()
    {
        GameOverMenu();
    }
    private void GameOverMenu()
    {
        player.GameOver();
    }



}
