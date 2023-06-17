using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Coin")){
            collectSoundEffect.Play(); 
            Destroy(collision.gameObject); 
            PlayerController.numberOfCoins++;
        }
    }
}
