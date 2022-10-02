using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float thrust = 100f;
    [SerializeField] float rotate = 100f;
    [SerializeField] AudioClip thrustAudio;

    [SerializeField] ParticleSystem thrustParticles; 
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;      



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotate();
    }

    void processThrust(){

        if(Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopThrusting();
        }
    }

    void processRotate(){

        if(Input.GetKey(KeyCode.A))
        {
            PressingA();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            PressingD();
        }
    }

    private void ApplyThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudio);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    private void PressingA()
    {
        applyRotation(rotate);
        rightParticles.Stop();
        leftParticles.Play();
    }

    private void PressingD()
    {
        applyRotation(-rotate);
        leftParticles.Stop();
        rightParticles.Play();
    }

    void applyRotation(float roatetionThrust){
        rb.freezeRotation = true;   //freeze the rotation caused by the physics system
        transform.Rotate(Vector3.forward*roatetionThrust*Time.deltaTime);
        rb.freezeRotation = false;  //unfreeze the rotation cause by the physics system
    }
}
