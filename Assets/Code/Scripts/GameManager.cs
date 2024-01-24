using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private UIController uIController;

	public static GameManager instance;
	[SerializeField] private int enemiesInGame;
	[SerializeField] private bool playerIsDead;

	private bool playerHasWeapon;

	public bool PlayerHasWeapon{
		get => playerHasWeapon;
		set{
			playerHasWeapon = value;
			if(enemiesInGame != 0){
				uIController.ActiveWeaponText();
			}
		}
	}

	public int EnemiesInGame{
		get => enemiesInGame;
		set{
			enemiesInGame = value;
			if (enemiesInGame == 0){
				uIController.ActiveWinText();
			}
		}
	}

	public bool PlayerIsDead{
		get => playerIsDead;
		set{
			playerIsDead = value;
			if (playerIsDead == true){
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
		EndGame();
	}

	private void ResetGame(){
		if (playerIsDead && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void EndGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SceneManager.LoadScene(0);
		}
    }
	public void EndLevel(){
		if (enemiesInGame == 0){
			SceneManager.LoadScene(0);
		}
	}

}