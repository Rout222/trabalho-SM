using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour {

    public GameObject Item;
	private Animator anim;
	private void Start(){
		anim = GetComponent<Animator> ();
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 contato, centro;
        GameObject drop;

        if (col.gameObject.name.StartsWith("Mario"))
        {
            contato = col.contacts[0].point;
            centro = GetComponent<Renderer>().bounds.center;

            // Verifica se o contato foi na base no bloco
            // Para arressar o item para fora
			if (centro.y > contato.y && !anim.GetBool("esvaziar"))
            {
                drop = Instantiate(Item, transform.position, Quaternion.identity);
                drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 10),ForceMode2D.Impulse);
				anim.SetBool ("esvaziar", true);
            }
        }
    }
}
