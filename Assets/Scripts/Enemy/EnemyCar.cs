using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCar : Enemy
{

    [SerializeField] private float speed = 0;
    [SerializeField] private float SignalStartDistance = 400;
    private Rigidbody rb;
     


    // Start is called before the first frame update
    void Start()
    {
        ObstacleDamage = 0.4f;
        rb = GetComponent<Rigidbody>();

    }
    


    // Update is called once per frame
    void FixedUpdate()
    {


        rb.velocity = transform.forward*speed * Time.fixedDeltaTime;
        
    }
}
