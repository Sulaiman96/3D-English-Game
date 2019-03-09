using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float transitionTime = 1;
    public bool isSprinting;

    private float speed;

    Rigidbody rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        var x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(x, 0, z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = runSpeed;
        }
        else if (speed != walkSpeed)
        {
            isSprinting = false;
            speed = walkSpeed;
        }
        #endregion

        #region Animation
        if (isSprinting)
        {
            AnimationUpdate("Horizontal", "Direction", 1);
            AnimationUpdate("Vertical", "Speed", 1);
        }
        else
        {
            AnimationUpdate("Horizontal", "Direction", 0.5f);
            AnimationUpdate("Vertical", "Speed", 0.5f);
        }

        #endregion
    }



    #region Functions
    private void AnimationUpdate(string axisName, string motionName, float motionSpeed)
    {
        if (Input.GetAxis(axisName) > 0)
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), motionSpeed, transitionTime * Time.deltaTime)); //Set the animation for positive values
        }
        else if (Input.GetAxis(axisName) < 0)
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), -motionSpeed, transitionTime * Time.deltaTime)); //Set the animation for negative values
        }
        else
        {
            anim.SetFloat(motionName, Mathf.Lerp(anim.GetFloat(motionName), 0, transitionTime * Time.deltaTime)); //Set the animation for idle
        }
    }

    private void VelocityUpdate(string axisName, float velocity)
    {
        if (axisName == "Vertical")
            rb.AddForce(transform.forward * velocity, ForceMode.Force);

        if (axisName == "Horizontal")
            rb.AddForce(transform.right * velocity, ForceMode.Force);

    }
    #endregion
}
