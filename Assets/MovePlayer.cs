using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
	
	private PhotonView pv;
	public float speed = 0.01f;
	// Use this for initialization
	void Start () {
		pv = gameObject.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		if(pv.isMine)
		{
			if(Input.GetKey(KeyCode.A))
			{
				gameObject.transform.position+=new Vector3(0.01f,0f,0f);
			}
			else if(Input.GetKey(KeyCode.S))
			{
				gameObject.transform.position+=new Vector3(0f,0f,0.01f);
			}
			else if(Input.GetKey(KeyCode.W))
			{
				gameObject.transform.position+=new Vector3(0f,0f,-0.01f);
			}
			else if(Input.GetKey(KeyCode.D))
			{
				gameObject.transform.position+=new Vector3(-0.01f,0f,0f);
			}
			
			
			if(Input.touchCount >0)
			{
				Touch touch = Input.GetTouch(0);
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				//gameObject.transform.position = touchPosition;
				float step = speed *Time.deltaTime;
				gameObject.transform.position = Vector3.MoveTowards(transform.position, touchPosition, step);
			}
		}
	}
}
