using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

	public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthbar;
	public float moveSpeed;
	public bool rushing = false;
	private float speedMod = 0;

	float timeLeft = 2f;

	private Rigidbody2D myRigidBody;

	private Animator myAnim;

	public GameObject bubbles;

	private Chest currentChest;
	public Text moneyText;
	public int money = 0;
	public int KeyCount = 0;
	public Text keyText;

	// Use this for initialization
	void Start (){
		myRigidBody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
		currentHealth = maxHealth;
		healthbar.SetMaxHealth(maxHealth);
		UpdateMoneyUI();
	}
	
	// Update is called once per frame
	void Update (){

		resetBoostTime ();
		controllerManager ();
		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidBody.velocity.x));

		if(Input.GetKeyDown(KeyCode.Z))
		{
			TakeDamage(20);
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		Debug.Log($"Player took damage. Current health: {currentHealth}");
		healthbar.SetHealth(currentHealth);
		hurt();

		if(currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void HealDamage(int heal)
	{
		currentHealth += heal;
		healthbar.SetHealth(currentHealth);

		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Chest")) {
			currentChest = collision.GetComponent<Chest>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Chest")) {
			currentChest = null;
		}
	}


	void controllerManager (){
		if (Input.GetAxisRaw ("Horizontal") > 0f) {
			transform.localScale = new Vector3(1f,1f,1f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {			
			transform.localScale = new Vector3(-1f,1f,1f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Vertical") > 0f) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, moveSpeed, 0f);
		} else if (Input.GetAxis ("Vertical") < 0f) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, -moveSpeed, 0f);
		}

		if(Input.GetButtonDown("Jump") && !rushing ){
			rushing = true;
			speedMod = 2;
			Instantiate (bubbles, gameObject.transform.position, gameObject.transform.rotation);
			movePlayer ();
		}	
	}

	void movePlayer(){
		if (transform.localScale.x == 1) {
			myRigidBody.velocity = new Vector3 (moveSpeed + speedMod, myRigidBody.velocity.y, 0f);	
		} else {
			myRigidBody.velocity = new Vector3 (- (moveSpeed + speedMod), myRigidBody.velocity.y, 0f);
		}	
	}

	void resetBoostTime(){
		if (timeLeft <= 0) {
			timeLeft = 2f;
			rushing = false;
			speedMod = 0;
		} else if(rushing) {
			timeLeft -= Time.deltaTime;
		}	
	}

	public void hurt(){
		if(!rushing){
			gameObject.GetComponent<Animator> ().Play ("PlayerHurt");		
		}

	}

	public void AddMoney(int amount)
	{
		money += amount;
		UpdateMoneyUI();
		Debug.Log("Player now has " + money + " money!");
	}

	void UpdateMoneyUI()
	{
		if (moneyText != null)
		{
			moneyText.text = "" + money;
		}
	}

	void UpdateKeyUI()
	{
		if (keyText != null)
		{
			keyText.text = "" + KeyCount;
		}
	}

	public void PickupKey()
	{
		KeyCount++;
		UpdateKeyUI();
		Debug.Log("Picked up a key");
	}

	public void UseKey()
	{
		KeyCount--;
		Debug.Log("Used a key");
	}


}
