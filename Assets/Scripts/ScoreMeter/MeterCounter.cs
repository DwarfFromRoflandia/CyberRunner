using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCounter : MonoBehaviour
{
    private Text text;

    private float speedCounter = 1;//чем меньше значение данной переменной, тем больше скорость счётчика

    private int meterCount = 0;//переменная в которую будет присваиваться значение того, сколько пробежал игрок
    public int MeterCount { get => meterCount;}

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public IEnumerator MeterCounterCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedCounter);
            meterCount++;
            text.text = meterCount.ToString();
        }
    }

    public IEnumerator MeterCounterSpeedCoroutine()
    {
        while (speedCounter > 0.3f)
        {
            yield return new WaitForSeconds(8f);
            speedCounter -= 0.02f;
        }
    }
}
