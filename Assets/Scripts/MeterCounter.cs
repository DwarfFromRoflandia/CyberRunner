using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCounter : MonoBehaviour
{
    private Text text;
    private float speed = 1;//чем больше значение данной переменной, тем с большей скоростью будут набираться метры у игрока
    private float meterCounter = 0;//переменная в которую будет присваиваться значение того, сколько пробежал игрок
    private Player player;

    private void Start()
    {
        text = GetComponent<Text>();

        InvokeRepeating("Counter", 0, 1 / speed);//метод InvokeRepeating() вызывает метод Counter() начиная с 0 секунд после запуска игры и повторяет вызов данного метода и интервалом, который задаём в переменной speed
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Counter()
    {
        if (player.IsGameOver == false && player.Paused == true)
        {
            meterCounter += 1;
            text.text = meterCounter.ToString();
        }
       
    }

}
