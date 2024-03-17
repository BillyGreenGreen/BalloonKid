using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float upwardsForce;
    public float blowForce;
    Vector3 mouseVectorPos;
    Vector2 forceVector;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = new Vector3(0, upwardsForce, 0);
        rb.velocity = force * upwardsForce * Time.deltaTime;
        if (Input.GetMouseButton(0)){
            Blow();
        }
    }

    void Blow(){
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
        mouseVectorPos = Camera.main.ScreenToWorldPoint(mousePos);
        forceVector = -(mouseVectorPos - transform.position).normalized;
        rb.AddForce(forceVector * -blowForce);
        
        //rb.AddForce(force);

        //rb.AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 10, ForceMode.Impulse);
    }
}
