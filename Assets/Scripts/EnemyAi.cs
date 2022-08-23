using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f; 

    [SerializeField]
    private GameObject enemyExplosionAnimation;

    private UIManager _uiManager;

    void Start(){
      _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
        
    void Update()
    {
        Movement();
    }

    private void Movement(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float randomNumber = Random.Range(-8.51f, 8.5f);
        if(transform.position.y <= -6.9f){
           Destroy(this.gameObject);
        }
    }
     private void OnTriggerEnter2D(Collider2D other){

         if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            if(player == null) return;
            player.Damage();
            Destroy(this.gameObject);
         }
         else if(other.tag == "Laser"){
            Laser laser = other.GetComponent<Laser>();
            if(laser == null) return;
            Destroy(laser.gameObject);
            Destroy(this.gameObject);
            IncreaseScore();
         }
        
        PlayExplosionAnimation();
     }

     private void PlayExplosionAnimation() {
            Instantiate(enemyExplosionAnimation, this.transform.position, Quaternion.identity);
     }

     public void IncreaseScore(){
         if(_uiManager == null)  return;
            _uiManager.UpdateScore();
     }

}
