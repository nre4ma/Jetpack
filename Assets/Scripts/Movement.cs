using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

  [SerializeField]
  float mainThrust = 100f;
  [SerializeField]
  float rotationThrust = 1f;
  [SerializeField]
  AudioClip mainEngine;
  [SerializeField]
  ParticleSystem mainBoosterParticles;
  [SerializeField]
  ParticleSystem leftThrusterParticles;
  [SerializeField]
  ParticleSystem rightThrusterParticles;

  Rigidbody rb;
  AudioSource m_MyAudioSource;

  // Start is called before the first frame update
  void Start() {
    rb = GetComponent<Rigidbody>();

    //Fetch the AudioSource from the GameObject
    m_MyAudioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update() {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust() {
    if (Input.GetKey(KeyCode.Space))
    {
      StartThrusting();
    }
    else
    {
      StopThrusting();
    }
  }

  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!m_MyAudioSource.isPlaying)
    {
      m_MyAudioSource.PlayOneShot(mainEngine);
    }
    if (!mainBoosterParticles.isPlaying)
    {
      mainBoosterParticles.Play();
    }
  }
void StopThrusting()
  {
    m_MyAudioSource.Stop();
    mainBoosterParticles.Stop();
  }
  void ProcessRotation() {
    if (Input.GetKey(KeyCode.A))
    {
      RotateLeft();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      RotateRight();
    }
    else
    {
      StopRotating();
    }
  }

  void StopRotating()
  {
    rightThrusterParticles.Stop();
    leftThrusterParticles.Stop();
  }

  void RotateLeft()
  {
    ApplyRotation(rotationThrust);
    if (!rightThrusterParticles.isPlaying)
    {
      rightThrusterParticles.Play();
    }
  }
  void RotateRight()
  {
    ApplyRotation(-rotationThrust);
    if (!leftThrusterParticles.isPlaying)
    {
      leftThrusterParticles.Play();
    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // freezing rotation so we can manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
  }
}
