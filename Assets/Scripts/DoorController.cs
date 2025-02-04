using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "Main Menu";
    public bool isOpen;
    public Animator animator;

    public string dialogueMessage = "The door is locked."; // Message to show

    public void OpenDoor(GameObject obj)
    {
        if(!isOpen)
        {
            PlayerController controller = obj.GetComponent<PlayerController>();
            if(controller)
            {
                if(controller.KeyCount > 0)
                {
                    isOpen = true;
                    controller.UseKey();
                    animator.SetBool("Open", isOpen);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call a method to complete the level
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        // Optionally, add a delay or play a sound/animation here
        SceneManager.LoadScene(mainMenuSceneName);
    }
}

