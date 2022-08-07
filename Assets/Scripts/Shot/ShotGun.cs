using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] private AudioSource SourceShot; // ссылка на ппоигрыватель 
    [SerializeField] private AudioClip ClipShot; // клип выстрела
    [SerializeField] private float FireRate=1; // время средующего выстрела
    [SerializeField] private GameObject  BulletPrefab; // летящий снаряд
   
    [SerializeField] private Animator Player_Anim;
    [SerializeField] private int PatronQuantity; // количество патрон
    void Start()
    {
         
    }
    public void ShotStart()
    {
        
        StartCoroutine(Shot());
        Player_Anim.SetBool("Shot",true);
        return;

    }
    public void ShotFinish()
    {
       
        StopAllCoroutines();
        Player_Anim.SetBool("Shot", false);
        return;


    }    // Update is called once per frame

    public IEnumerator Shot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(90, 0, 0));

            SourceShot.PlayOneShot(ClipShot);
            yield return new WaitForSeconds(FireRate);
        }
    
    }

    void Update()
    {
        
    }
}
