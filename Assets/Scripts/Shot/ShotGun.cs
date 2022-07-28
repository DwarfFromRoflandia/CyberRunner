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

    [SerializeField] private int PatronQuantity; // количество патрон
    void Start()
    {
        
    }
    public void ShotStart()
    {
        print("—трел€ет");
        StartCoroutine(Shot());

    }
    public void ShotFinish()
    {
        print("ѕерестал стрел€ть ");
        StopAllCoroutines();
    
    
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
