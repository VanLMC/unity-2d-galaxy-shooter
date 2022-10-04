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

   [SerializeField]
    private AudioClip _audioClip;

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
            DestroyLaser(other);
            Destroy(this.gameObject);
            IncreaseScore();
         }
        
        Explode();
     }

     private void Explode() {
            Instantiate(enemyExplosionAnimation, this.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f); 
            //PlayClip at point plays a soundclip that is passed to it on the main camera position
            //The AudioSource is not used in this cause because its parent object would soon be destroyed and the audio would not be heard
     }

     private void DestroyLaser(Collider2D other){
         Laser laser = other.GetComponent<Laser>();
         if(laser == null) return;
         Destroy(laser.gameObject);
     }

     private void IncreaseScore(){
         if(_uiManager == null)  return;
            _uiManager.UpdateScore();
     }



}
