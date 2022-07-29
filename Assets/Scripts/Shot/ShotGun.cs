using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] private AudioSource SourceShot; // ссылка на ппоигрыватель 
    [SerializeField] private AudioClip ClipShot; // клип выстрела
    [SerializeField] private float FireRate=1; // врем€ средующего выстрела
    [SerializeField] private GameObject  BulletPrefab; // лет€щий снар€д
    [SerializeField] private ParticleSystem  ParticleBullet; // столкновение
    [SerializeField] private Animator Player_Anim;
    [SerializeField] private int PatronQuantity; // количество патрон
    void Start()
    {
        
    }
    public void ShotStart()
    {
        print("—трел€ет");
        StartCoroutine(Shot());
        Player_Anim.SetBool("Shot",true);
        return;

    }
    public void ShotFinish()
    {
        print("ѕерестал стрел€ть ");
        StopAllCoroutines();
        Player_Anim.SetBool("Shot", false);
        return;


    }    // Update is called once per frame

    public IEnumerator Shot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(90, 0, 0));


            yield return new WaitForSeconds(FireRate);
        }
    
    }

    void Update()
    {
        
    }
}
