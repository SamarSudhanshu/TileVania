using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpsfx;
    [SerializeField] private int pointsForCoinPickup = 100;
    bool wasCollected;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !wasCollected) {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickUpsfx, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
