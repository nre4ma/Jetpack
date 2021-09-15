using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatDebug : MonoBehaviour
{
    bool collisionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
      if(Input.GetKeyDown(KeyCode.L)) {
        LoadNextLevel();
      } 
      else if(Input.GetKeyDown(KeyCode.C)) {
        collisionDisabled = !collisionDisabled;
        Debug.Log("Cheat Activated - collisionDisabled = "+ collisionDisabled);
      }
    }

  void LoadNextLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex +1;

    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
      nextSceneIndex = 0;
    }
    
    SceneManager.LoadScene(nextSceneIndex);
  }

  public bool isCollisionDisabled() {
    return this.collisionDisabled;
  }
}
