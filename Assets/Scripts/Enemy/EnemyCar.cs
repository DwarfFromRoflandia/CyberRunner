using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class EnemyCar : Enemy
{
    
    [SerializeField] private float speed = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        ObstacleDamage = 0.3f;
    
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        
    }
}
