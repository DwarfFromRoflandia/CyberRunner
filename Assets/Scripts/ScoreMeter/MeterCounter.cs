using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCounter : MonoBehaviour
{
    private Text text;
    public float speedCounter = 1;//чем больше значение данной переменной, тем с большей скоростью будут набираться метры у игрока
    public float SpeedCounter { get { return speedCounter; } set { speedCounter = value; } }

    private int meterCount = 0;//переменная в которую будет присваиваться значение того, сколько пробежал игрок
    public int MeterCount { get => meterCount;}

    private Player player;
    

    private void Start()
    {
        text = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        StartCoroutine(ChangeSpeedCounterMeter());
        StartCoroutine(CounterMeter());
    }

    private void Update()
    {
        if (player.IsGameOver == false && player.IsPauseOn == false)
        {
            text.text = meterCount.ToString();
        }
    }

    private IEnumerator ChangeSpeedCounterMeter()//метод отвечающий за постепенное увеличение скорости игры
    {
        while (player.IsGameOver == false && player.IsPauseOn == false)
        {
            yield return new WaitForSeconds(5f);
            speedCounter -= 0.01f;
        }
    }

    private IEnumerator CounterMeter()
    {
        while (player.IsGameOver == false && player.IsPauseOn == false)
        {
            yield return new WaitForSeconds(speedCounter);
            text.text = meterCount.ToString();
            meterCount++;
        }
    }

    
}
