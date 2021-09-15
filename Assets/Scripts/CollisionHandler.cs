using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField]
  float levelLoadDelay = 2f;
  [SerializeField]
  AudioClip crash;
    [SerializeField]
  AudioClip success;
  [SerializeField]
  ParticleSystem crashParticles;
  [SerializeField]
  ParticleSystem successParticles;

  AudioSource m_MyAudioSource;

  GameObject debugGameObject;

  CheatDebug cheatDebugScript;

  bool isTransitioning = false; // state
  bool collisionDisabled = false;

  void Start() {
    m_MyAudioSource = GetComponent<AudioSource>();
    debugGameObject = GameObject.FindWithTag("Cheats");
  
    if(debugGameObject != null) {
      Debug.Log("debugGameObject is not null");
      cheatDebugScript = debugGameObject.GetComponent<CheatDebug>();
    }
  }

  void Update() {
    collisionDisabled = cheatDebugScript.isCollisionDisabled();
  }

  void OnCollisionEnter(Collision other) {
    if (isTransitioning || collisionDisabled) { return; }

    switch(other.gameObject.tag) {
      case "Friendly":
        Debug.Log("This thing is friendly");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      case "Fuel":
        Debug.Log("You picked up fuel.");
        break;
      default:
        StartCrashSequence();
        break;
    }
    
  }

  void StartSuccessSequence() {
    isTransitioning = true;
    m_MyAudioSource.Stop(); // Stop all existing sound effects before playing success
    m_MyAudioSource.PlayOneShot(success);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void StartCrashSequence() {
    isTransitioning = true;
    m_MyAudioSource.Stop(); // Stop all existing sound effects before playing crash
    crashParticles.Play();
    m_MyAudioSource.PlayOneShot(crash);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }
  void ReloadLevel() {

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex +1;

    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
      nextSceneIndex = 0;
    }
    
    SceneManager.LoadScene(nextSceneIndex);
  }
}
