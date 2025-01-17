﻿using UnityEngine;
using System.Collections;

public class Balade : MonoBehaviour {

    //SPECIAL PAPILLON////
    public bool Papillon;

    //BALADE///////////////////////////////////////////////////////////////////////////

        //Indiquateur d'une balade en cours
        public bool balade_en_cours;

        //TAILLE DE LA BALADE//////////////////////////////////////////////////////////////
            // Entier permettant de dire quel est l'envergure de la prochaine destination
            [Range(-4, 4)]
            public float taille_balade;
            
            // Si la balade est aléatoire? Si taille_balade est aléatoire
            public bool balade_aleatoire;
                //Si la balade est aléatoire permet de mettre un min et un max 
                [Range(0.2f,10)]
                public float min_distance; //0.2f est un minimum
                public float max_distance;

            

        //TEMPS DE LA BALADE//////////////////////////////////////////////////////////////
        public float temps_ballade; //Le temps laissé pour la balade
        public bool temps_ballade_aleatoire; //Si le temps est aléatoire
        public float min_temps_ballade;
        public float max_temps_ballade;

    //DEPLACEMENT/////////////////////////////////////////////////////////////////////////

        //La destination
        public Vector3 destination;
        //La distance entre le point actuel et la destination
        private float distance;


    //PAUSE///////////////////////////////////////////////////////////////////////////////
        
        public bool pause_en_cours;

        //TEMPS DE LA BALADE//////////////////////////////////////////////////////////////
        public float temps_pause; //Le temps laissé pour la balade
        public bool temps_pause_aleatoire; //Si le temps est aléatoire
        public float min_temps_pause;
        public float max_temps_pause;

    //BOOL KEYBOARD/////////////////

        public bool rotate;

        public Vector3 velocity;
        
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        private bool move;
        private bool mirror;

        public float moveSpeed;
        public float max_moveSpeed;
        public float acceleration;
        public float desceleration;
        
        //Gameobject
        public GameObject source_sprite;    

        //Animation
        public Animator anim;

        //KNOCKBACK
        private Vector3 knockback_direction;
        private float knockback_force;

		private float delta = 0.2f;


	// Use this for initialization
	void OnEnable()
    {

        up = false;
        down = false;
        left = false;
        right = false;


        //On lance une pause, 
        balade_en_cours = false; //
        pause_en_cours = false; //
        StartCoroutine(pause_balade());

        //rigidbody2D.velocity *= 0;

        if (anim != null)
        {
            anim = source_sprite.GetComponent<Animator>();
            anim.SetBool("moving", false);
        }

    }

    IEnumerator haut_bas()
    {
        while(balade_en_cours==false)
        {
            up = !up;
            down = !down;
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator pause_balade()
    {
        if (Papillon == true)
        {
            StartCoroutine(haut_bas());
        }
        //TEMPS/////////////////////////////////////////////////////////////

        if (temps_pause_aleatoire == true) //Si le temps est aléatoire
        {
            temps_pause = Random.Range(min_temps_pause, max_temps_pause);

        }

        pause_en_cours = true;

        if(anim!=null)
			anim.SetBool("moving", false);

		GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(temps_pause);
        pause_en_cours = false;

        StartCoroutine(balade());
    }

    IEnumerator balade()
    {
        if(temps_ballade_aleatoire==true) //Si le temps est aléatoire
        {
            temps_ballade = Random.Range(min_temps_ballade, max_temps_ballade);
        }

		//On ne peut aller que dans la zone délimitée par l'écran: de (-5.55, -2.52) à (5.55, 2.52)
		destination = new Vector3(Random.Range(-5.55f, 5.55f), Random.Range(-2.52f, 2.52f) , this.gameObject.transform.position.z);
        distance = Vector3.Distance(transform.position, destination);

		//Si on est trop près, on va plus loin. Attention, ça peut faire aller en dehors de la zone.
		if (distance < delta * delta * 1.5f)
		{
			destination = transform.position + (destination - transform.position).normalized * delta * delta * 1.5f;
			distance = delta * delta * 1.5f;
		}
       
        balade_en_cours = true;
        yield return new WaitForSeconds(temps_ballade);
        balade_en_cours = false;
        StartCoroutine(pause_balade());
    }

    void Update()
    {
        if (balade_en_cours == true)
        {
			//préventions bords d'écran
            deplacement();
            vecteur();
            acc_des();
            animation();
        }

        if(balade_en_cours==false)
        {
            up = false;
            down = false;
            left = false;
            right = false;

            if (anim != null)
			anim.SetBool("moving", false);

			GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            if (knockback_force > 0)
            {
                knockback_force -= Time.deltaTime * 5;
            }
            else
            {
                knockback_force = 0;
            }

			moveSpeed = 0;
        }

    }

    #region deplacement

    void deplacement()
    {
		if (destination.x == transform.position.x)
		{
			left = false; right = false;
		}
		else
		{
			if (Mathf.Abs(transform.position.x - destination.x) < delta)
			{
				left = false; right = false;
			}
			else
			{
				if (destination.x > transform.position.x)
				{
					left = false;
					right = true;
				}
				if (destination.x < transform.position.x)
				{
					right = false;
					left = true;
				}
			}

			
		
		}


        if (destination.y == transform.position.y) 
        {
            up = false; down = false;

        }
		else
		{
			if (Mathf.Abs(transform.position.y - destination.y) < delta)
			{
				up = false; down = false;
			}
			else
			{
				if (destination.y < transform.position.y)
				{
					up = false;
					down = true;

				}
				if (destination.y > transform.position.y)
				{
					down = false;
					up = true;

				}
			}
		}
            
    }
    void vecteur()
    {
        //Diagonals
        if (up && left)
        {
			velocity = new Vector3(-1, 1, 0).normalized;
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 45); }
        }
        if (up && right)
        {
			velocity = new Vector3(1, 1, 0).normalized;
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 315); }
        }
        if (down && left)
        {
			velocity = new Vector3(-1, -1, 0).normalized;
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 135); }
        }
        if (down && right)
        {
			velocity = new Vector3(1, -1, 0).normalized;
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 225); }
        }


        //Simple direction
        if (up == true && !left && !right)
        {
            velocity = new Vector3(0, 1, 0);
            
        }
        if (down == true && !left && !right)
        {
            velocity = new Vector3(0, -1, 0);
        }
        if (left == true && !up && !down)
        {
            velocity = new Vector3(-1, 0, 0);
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90); } 

        }
        if (right == true && !up && !down)
        {
            velocity = new Vector3(1, 0, 0);
            if (rotate == true) { this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);}
        }

		if (!up && !down && !right && !left)
		{
			velocity *= 0;
			if(anim!=null)
			anim.SetBool("moving", false);
		}

    }
    void acc_des()
    {
        //On ajoute une acceleration
        if ((up || down || left || right)&&distance>1)
        {
            moveSpeed += acceleration*Time.deltaTime;
            if (moveSpeed >= max_moveSpeed)
            {
                moveSpeed = max_moveSpeed;
            }
        }
        if(distance<1) //ou on descelere
        {
			moveSpeed -= desceleration * Time.deltaTime;
            if (moveSpeed <= 0)
            {
                moveSpeed = 0;
            }
        }

		if (distance < 0.5f) //ou on descelere
		{
			moveSpeed = 0;
		}

        //Déplacements
        GetComponent<Rigidbody2D>().velocity = velocity * moveSpeed;
        
    }

    void animation()
    {
        if (up || down || left || right)
        {
            move = true;
        }
        if(up==false && down ==false && left==false && right==false)
        {
            move = false;
        }

        if(right==true)
        {
            mirror = true;
        }
        if(left==true)
        {
            mirror = false;
        }

        if (anim != null)
        {
            anim.SetBool("moving", true);
            anim.SetBool("mirror", mirror);
        }


		if (anim != null)
		{
			anim.SetBool("moving", move);
			anim.SetBool("mirror", mirror);
		}
 
    }
#endregion 
	/*
    void OnCollisionEnter2D(Collision2D other)
    {
		
		if(other.gameObject.tag=="mur")
		{
			Debug.Log("collision");
			balade_en_cours = false;
			StopCoroutine("balade");
			StartCoroutine("pause_balade");
		}
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "mur")
		{
			Debug.Log("collision");
			balade_en_cours = false;
			StopCoroutine("balade");
			StartCoroutine("pause_balade");
		}
    }
	*/

}
