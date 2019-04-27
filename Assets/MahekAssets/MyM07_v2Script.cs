using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyM07_v2Script : MonoBehaviour {

    private Animator mo7v2Anim;
    // Use this for initialization
    void Start () {
        mo7v2Anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        bool walking = Input.GetKey(KeyCode.W);
        mo7v2Anim.SetBool("walk", walking);
    }
}
