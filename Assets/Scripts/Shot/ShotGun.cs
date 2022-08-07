using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] private AudioSource SourceShot; // ������ �� ������������� 
    [SerializeField] private AudioClip ClipShot; // ���� ��������
    [SerializeField] private float FireRate=1; // ����� ���������� ��������
    [SerializeField] private GameObject  BulletPrefab; // ������� ������
   
    [SerializeField] private Animator Player_Anim;
    [SerializeField] private int PatronQuantity; // ���������� ������
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
