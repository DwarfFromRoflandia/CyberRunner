using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShotGun : MonoBehaviour
{
    [SerializeField] private AudioSource SourceShot; // ссылка на ппоигрыватель 
    [SerializeField] private AudioClip ClipShot; // клип выстрела
   
    [SerializeField] private GameObject  BulletPrefab; // летящий снаряд
   
    [SerializeField] private Animator Player_Anim;
  
    [SerializeField]private Text QuantityBullets; // количество патрон
	void Start()
    {
      if(QuantityBullets!=null)
        QuantityBullets.text = PlayerPrefs.GetInt("Bullets").ToString();
    }
    public void ShotStart()
    {
    
        Player_Anim.SetBool("Shot",true);
       

        return;

    }
    public void ShotFinish()
    {
       

        Player_Anim.SetBool("Shot", false);
        return;


    }    // Update is called once per frame

    public void Shot()
    {
        if (Convert.ToInt32(QuantityBullets.text) > 0)
        {

            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(90, 0, 0));
            SourceShot.PlayOneShot(ClipShot);
            Destroy(bullet, 20f);
            PlayerPrefs.SetInt("Bullets", Convert.ToInt32(QuantityBullets.text) - 1);
            QuantityBullets.text = PlayerPrefs.GetInt("Bullets").ToString();
        }


    }

  
}
