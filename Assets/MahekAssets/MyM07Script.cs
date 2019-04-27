using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyM07Script : MonoBehaviour {

    private Animator mo7Anim;

	// Use this for initialization
	void Start () {

        mo7Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
		bool walking = false;
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
			walking = true;
		}
		mo7Anim.SetBool("walking", walking);
        if (Input.GetMouseButtonDown(0))
        {
            mo7Anim.SetTrigger("hit");
        } else if (Input.GetMouseButtonDown(1))
        { 
            mo7Anim.SetTrigger("attack");
        }
    
    }
}
