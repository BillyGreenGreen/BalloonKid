using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")){
            //playerController.Kill();
        }
    }
}
