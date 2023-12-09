using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private UIController uIController;

	public static GameManager instance;
	[SerializeField]
	private int enemiesInGame;
	private bool playerHasWeapon;
	[SerializeField]
	private bool playerIsDead;

	public bool PlayerHasWeapon
	{
		get => playerHasWeapon;
		set
		{
			playerHasWeapon = value;
			if(enemiesInGame != 0)
			{
				uIController.ActiveWeaponText();
			}
		}
	}

	public int EnemiesInGame
	{
		get => enemiesInGame;
		set
		{
			enemiesInGame = value;
			Debug.Log("Uno menos");
			if (enemiesInGame == 0)
			{
				uIController.ActiveWinText();
			}
		}
	}

	public bool PlayerIsDead
	{
		get => playerIsDead;
		set
		{
			playerIsDead = value;
			if (playerIsDead == true)
			{
				uIController.ActiveLoseText();
			}
		}
	}

	private void Awake(){
		if (instance == null){
			instance = this;
		}
	}

	private void Update(){
		ResetGame();
	}

	private void ResetGame(){
		if (playerIsDead && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
	}

	public void EndLevel()
	{
		if (enemiesInGame == 0)
		{
			SceneManager.LoadScene(0);
		}
		
	}

}