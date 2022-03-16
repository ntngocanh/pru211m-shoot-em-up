using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
	public AudioSource audioSource;
	public void LoadLevel1(){
		SceneManager.LoadScene("Level1");
	}
}
