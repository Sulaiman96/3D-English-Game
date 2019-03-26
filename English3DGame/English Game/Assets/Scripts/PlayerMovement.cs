using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Vairables
    private float speed;
    public float walkingSpeed = 0.05f;
    public float runningSpeed = 0.1f;
    public float rotationSpeed = 0.05f;
    private bool answeringQuestions;
    Rigidbody rb;
    Animator anim;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);
        #region Animation
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;

            //Running animation controls
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Running", true);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", true);
                anim.SetBool("Running", false);
            }
        }
        else
        {
            speed = walkingSpeed;

            //Walking animation controls
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Running", false);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", true);
                anim.SetBool("Running", false);
            }
        }
        #endregion
    }
    public void PlayerStartedQuestions(bool val)
    {
        answeringQuestions = val;
    }
}
