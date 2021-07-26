using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadPlayer());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private IEnumerator LoadPlayer()
	{
		yield return new WaitForSeconds(2.0f);
		 PhotonNetwork.Instantiate("Player", new Vector3(0.71f,1.16f,0f), Quaternion.identity,0);
	}
}
