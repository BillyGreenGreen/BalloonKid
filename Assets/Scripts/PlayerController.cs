using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float blowForce = 5000f;
    private Rigidbody rb;
    bool isDying = false;
    public float upwardsForce = 10f;

    private Vector3 currentVelocity;
    private Vector3 originalUpwardsVelocity;
    private Coroutine blowCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalUpwardsVelocity = new Vector3(0, upwardsForce, 0) * upwardsForce;
        currentVelocity = originalUpwardsVelocity;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            // Get direction from center to mouse, then invert for opposite
            Vector3 direction = (screenCenter - mousePos).normalized;
            Vector3 blowVelocity = new Vector3(direction.x, direction.y, 0) * blowForce;

            if (blowCoroutine != null)
                StopCoroutine(blowCoroutine);

            blowCoroutine = StartCoroutine(BlowAndReturn(blowVelocity));
        }
    }

    void FixedUpdate()
    {
        if (!isDying)
        {
            rb.velocity = currentVelocity;
        }
    }

    IEnumerator BlowAndReturn(Vector3 blowVelocity)
    {
        float blowDuration = 0.3f;
        float returnDuration = 0.5f;
        float timer = 0f;

        // Set blow velocity
        currentVelocity = blowVelocity;
        yield return new WaitForSeconds(blowDuration);

        // Smoothly return to original upwards velocity
        Vector3 startVelocity = currentVelocity;
        timer = 0f;
        while (timer < returnDuration)
        {
            currentVelocity = Vector3.Lerp(startVelocity, originalUpwardsVelocity, timer / returnDuration);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        currentVelocity = originalUpwardsVelocity;
    }

    public void Kill()
    {
        if (!isDying)
        {
            isDying = true;
            rb.velocity = Vector3.zero;
        }
    }
}