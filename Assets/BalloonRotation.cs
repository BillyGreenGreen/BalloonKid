using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonRotation : MonoBehaviour
{
    public float speed = 20f;
    public Transform followPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotTarget = Quaternion.LookRotation(followPos.position - transform.position);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotTarget, speed * Time.deltaTime);
    }
}
