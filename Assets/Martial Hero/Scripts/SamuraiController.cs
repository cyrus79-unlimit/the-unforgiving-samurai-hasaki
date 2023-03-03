using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			animator.SetTrigger("isAttacking1");
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			animator.SetTrigger("isAttacking2");
		}


	}
}
