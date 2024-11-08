using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator; // Reference to the Animator for scene transitions
    // private Player player; // Reference to the Player

    void Awake()
    {
        // Get the Player component from children
        // player = GetComponentInChildren<Player>();
        transitionAnimator.enabled = false;

        // if (player == null)
        // {
        //     Debug.LogError("Player component not found in children of LevelManager!");
        // }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        transitionAnimator.enabled = true;
        // Start the fade-out animation
        
        transitionAnimator.SetTrigger("FadeIn");
        // Wait for the animation to complete
        yield return new WaitForSeconds(1);

        // Load the new scene asynchronously
        SceneManager.LoadSceneAsync(sceneName);

        // Wait for a frame to ensure the scene is loaded
        yield return null;

        // Reset player's position in the new scene
        Player.Instance.transform.position = new Vector3(0, -4.5f, 0);

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
