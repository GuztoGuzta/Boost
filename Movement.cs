using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float mainThrust = 1000f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(); 
        ProcessRotation();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RoateRight();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void StopThrusting()
    {
        MainBooster.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!LeftBooster.isPlaying)
        {
            LeftBooster.Play();
        }
    }

    private void RoateRight()
    {
        ApplyRotation(-rotationSpeed);
    }

    private void ApplyRotation(float rotationframe)
    {
        rb.freezeRotation = true; //freeze rotation so can manually rotate
        transform.Rotate(Vector3.forward * rotationframe * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze to enable physic
    }
}
