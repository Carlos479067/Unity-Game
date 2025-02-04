using UnityEngine;

public class Chest : MonoBehaviour {
    public Animator animator;  // Reference to the chest's Animator component
    private bool isOpen = false;  // To keep track of whether the chest is open
    public int moneyAmount = 50; // Amount of money in the chest
    public int healthAmount = 25;

    // Method called by the player to open the chest
    public void OpenChest() {
        if (!isOpen) {  // Only open if the chest is not already open
            animator.SetBool("Open", true);  // Set the "Open" bool to true
            isOpen = true;  // Mark the chest as opened

            // Add money to the player's total
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                if(gameObject.CompareTag("MoneyChest"))
                {
                    player.AddMoney(moneyAmount);
                }
                else if(gameObject.CompareTag("HealthChest"))
                {
                player.HealDamage(healthAmount);
                }
                else if(gameObject.CompareTag("KeyChest"))
                {
                    player.PickupKey();
                }
            }
        }
    }
    // Method called by the player to toggle the chest state
    public void ToggleChest() {
        if (isOpen) {
            CloseChest();
        } else {
            OpenChest();
        }
    }
    // Method to close the chest
    public void CloseChest() {
        animator.SetBool("Open", false);  // Set the "Open" bool to false
        isOpen = false;  // Mark the chest as closed
    }
}

    
            

