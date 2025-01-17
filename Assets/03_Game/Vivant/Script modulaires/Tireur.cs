﻿using UnityEngine;
using System.Collections;

public class Tireur : MonoBehaviour
{
	public GameObject bullet_prefab;
	private GameObject bullet;
	private Bullet bullet_script;

	public float frequence;
	public float vitesse;

	private Vector3 dir;

	private GameObject m_Player;

	public Animator anim;

	public bool IsAllowToShoot;

	public Tarentulette_IA IA_script;



	void Start()
	{
		m_Player = GameObject.FindGameObjectWithTag("Player");
		IA_script = GetComponent<Tarentulette_IA>();
	}

	

	public void Tir()
	{

		StartCoroutine(launch());
		/*
		//INSTANTIATION
		bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
		bullet.transform.parent = this.gameObject.transform.parent.parent.parent;

		

		SoundManagerEvent.emit(SoundManagerType.SPIDER_SPLIT);
		//if(IA_script.mirror==true)
		//{
		//	bullet.transform.localPosition = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 1, this.transform.position.z);
		//}
		//else
		//{
		//	bullet.transform.localPosition = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 1, this.transform.position.z);
		//}
		

		dir = new Vector3(m_Player.transform.position.x - this.gameObject.transform.parent.transform.position.x, m_Player.transform.position.y - 1 - this.gameObject.transform.parent.transform.position.y, m_Player.transform.position.z).normalized;
		bullet.transform.right = -dir;
		bullet.transform.localPosition = this.transform.position+dir * 0.5f+Vector3.up*1;
		//ENVOI dans une direction
		bullet.GetComponent<Rigidbody2D>().AddForce(dir * vitesse * 0.001f);

		anim.ResetTrigger("shoot");
		anim.SetTrigger("shoot");



		IA_script.IsShooting = IsAllowToShoot;*/
	}


	IEnumerator launch ()
	{
		//INSTANTIATION
		bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
		bullet.transform.parent = this.gameObject.transform.parent.parent.parent;
		dir = new Vector3(m_Player.transform.position.x - this.gameObject.transform.parent.transform.position.x, m_Player.transform.position.y - 2 - this.gameObject.transform.parent.transform.position.y, m_Player.transform.position.z).normalized;
		bullet.transform.right = -dir;
		bullet.transform.localPosition = this.transform.position + dir * 0.5f + Vector3.up * 1;
		//ENVOI dans une direction
		bullet.GetComponent<Rigidbody2D>().AddForce(dir * vitesse * 0.001f);
		yield return new WaitForSeconds(0.2f);
		
		//INSTANTIATION
		bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
		bullet.transform.parent = this.gameObject.transform.parent.parent.parent;
		dir = new Vector3(m_Player.transform.position.x - this.gameObject.transform.parent.transform.position.x, m_Player.transform.position.y - 1 - this.gameObject.transform.parent.transform.position.y, m_Player.transform.position.z).normalized;
		bullet.transform.right = -dir;
		bullet.transform.localPosition = this.transform.position + dir * 0.5f + Vector3.up * 1;
		//ENVOI dans une direction
		bullet.GetComponent<Rigidbody2D>().AddForce(dir * vitesse * 0.001f);
		yield return new WaitForSeconds(0.2f);
		
		//INSTANTIATION
		bullet = Instantiate(bullet_prefab, this.gameObject.transform.position, bullet_prefab.transform.rotation) as GameObject; //Instantiation
		bullet.transform.parent = this.gameObject.transform.parent.parent.parent;
		dir = new Vector3(m_Player.transform.position.x - this.gameObject.transform.parent.transform.position.x, m_Player.transform.position.y - this.gameObject.transform.parent.transform.position.y, m_Player.transform.position.z).normalized;
		bullet.transform.right = -dir;
		bullet.transform.localPosition = this.transform.position + dir * 0.5f + Vector3.up * 1;
		//ENVOI dans une direction
		bullet.GetComponent<Rigidbody2D>().AddForce(dir * vitesse * 0.001f);


		anim.ResetTrigger("shoot");
		anim.SetTrigger("shoot");



		IA_script.IsShooting = IsAllowToShoot;
	}



}
