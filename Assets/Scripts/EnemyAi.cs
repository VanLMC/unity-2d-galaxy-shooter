using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f; 
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomNumber = Random.Range(-8.51f, 8.5f);
        if(transform.position.y <= -6.9f){
            transform.position = new Vector3(randomNumber, 6.69f, 0);
        }
    }
     private void OnTriggerEnter2D(Collider2D other){
        float randomNumber = Random.Range(-8.51f, 8.5f);
 
         if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            if(player == null) return;
            player.Damage();
            this.transform.position = new Vector3(randomNumber, 6.69f, 0);
         }
         else if(other.tag == "Laser"){
            Laser laser = other.GetComponent<Laser>();
            if(laser == null) return;
            this.transform.position = new Vector3(randomNumber, 6.69f, 0);
            Destroy(laser.gameObject);
         }
     }

}
