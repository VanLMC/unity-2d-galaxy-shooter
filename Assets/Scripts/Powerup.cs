using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
            if(other.tag != "Player") return;
            Player player = other.GetComponent<Player>();
            if(player == null) return;
            player.TripleShotPowerupOn();
            Destroy(this.gameObject);
    }
}
