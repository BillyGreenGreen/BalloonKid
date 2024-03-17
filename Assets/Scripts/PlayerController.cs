using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float upwardsForce = 10f;
    public float blowForce;
    Vector3 mouseVectorPos;
    Vector2 forceVector;
    bool isDying = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDying){
            Vector3 force = new Vector3(0, upwardsForce, 0);
            rb.velocity = force * upwardsForce * Time.deltaTime;
            if (Input.GetMouseButton(0)){
                Blow();
            }
        }
        
    }

    void Blow(){
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
        mouseVectorPos = Camera.main.ScreenToWorldPoint(mousePos);
        forceVector = -(mouseVectorPos - transform.position).normalized;
        
        //screen constraints
        if (forceVector.x > 0.5f){
            forceVector.x = 0.5f;
        }
        else if (forceVector.x < -0.5f){
            forceVector.x = -0.5f;
        }
        if (forceVector.y > 0.5f){
            forceVector.y = 0.5f;
        }
        else if (forceVector.y < -0.5f){
            forceVector.y = -0.5f;
        }
        //Debug.Log(forceVector);
        rb.AddForce(forceVector * -blowForce);
    }

    void Kill(){
        isDying = true;
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")){
            Kill();
        }
        
    }
}
