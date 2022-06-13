using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource ad;
    [SerializeField] AudioClip mainengine;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float mainThrust = 1000f;

    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ad = GetComponent<AudioSource>();
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

        else
        {
            StopRotateParticle();
        }
    }

    private void StartThrusting()
    {
        if (!ad.isPlaying)
        {
            ad.PlayOneShot(mainengine);
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!MainBooster.isPlaying)
        {
            MainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        ad.Stop();
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
        if (!RightBooster.isPlaying)
        {
            RightBooster.Play();
        }
    }

    private void StopRotateParticle()
    {
        LeftBooster.Stop();
        RightBooster.Stop();
    }

    private void ApplyRotation(float rotationframe)
    {
        rb.freezeRotation = true; //freeze rotation so can manually rotate
        transform.Rotate(Vector3.forward * rotationframe * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze to enable physic
    }
}
