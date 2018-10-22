using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public bool onGround;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        onGround = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void JumpIt()
    {
        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0f, 5f, 0f);
                onGround = false;
            }
        }
    }
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Terreno"))
            {
                onGround = true;

            }
        }
}
