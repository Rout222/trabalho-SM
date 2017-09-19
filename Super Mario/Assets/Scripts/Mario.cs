using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mario : MonoBehaviour {

    public float VelocidadeAndando;
    public float ForcaSalto;
    public GameObject LabelVidas;
	public GameObject pontoProChao;

	public AudioSource AudioPulo;
	public AudioSource AudioMoeda;

    public Text vidasText;
    public Text moedasText;

    private bool ViradoDireita;
	private Animator anim;
	private float distProChao;
    private void Start()
    {
        ViradoDireita = true;
        if(!PlayerPrefs.HasKey("TotalMoedas"))
        {
            PlayerPrefs.SetInt("TotalMoedas", 0);
        }

        if(!PlayerPrefs.HasKey("Vidas"))
        {
            PlayerPrefs.SetInt("Vidas", 5);
        }
        moedasText.text = "Moedas: " + PlayerPrefs.GetInt("TotalMoedas");
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float pulo = Input.GetAxisRaw("Pulo");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Animator anim = GetComponent<Animator>();
        // Verifica se vai aplicar força para andar
        if ((x < 0 && rb.velocity.x > -VelocidadeAndando) || (x>0 && rb.velocity.x < VelocidadeAndando))
        {
            rb.AddForce(new Vector2(x, 0), ForceMode2D.Impulse);
        }

        // Verifica se irá rotacionar a animação
        if((x < 0 && ViradoDireita) || (x > 0 && !ViradoDireita)) {
            ViradoDireita = !ViradoDireita;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
        }
 
        // Verifica se pode pular
		if (pulo > 0 && noChao())
        {
			AudioPulo.Play();
            rb.velocity = new Vector2(rb.velocity.x, ForcaSalto);
        }

        // Estados para animação
        anim.SetBool("Andando", Mathf.Abs(rb.velocity.x) > 2);
        anim.SetBool("Subindo", rb.velocity.y > 1);
        anim.SetBool("Descendo", rb.velocity.y < -1);
		anim.SetBool("NoChao", noChao());

        // Atualiza rótulo
        vidasText.text = "Vidas: " + PlayerPrefs.GetInt("Vidas");
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Se colidir com uma moeda, incremente
        if(col.gameObject.name.StartsWith("Moeda"))
        {
			PegarMoeda ();
			Destroy(col.gameObject);
        }
    }

	private void PegarMoeda(){
		AudioMoeda.Play ();
        int coins = PlayerPrefs.GetInt("TotalMoedas");
        PlayerPrefs.SetInt("TotalMoedas", coins + 1);
        if(coins % 5 == 0)
        {
            PlayerPrefs.SetInt("Vidas", PlayerPrefs.GetInt("Vidas")+1);
        }
        moedasText.text = "Moedas: " + PlayerPrefs.GetInt("TotalMoedas");
    }

	private bool noChao(){
		return Physics2D.Raycast(pontoProChao.transform.position, -Vector2.up, -1f, 1 << LayerMask.NameToLayer("Ground"));
	}

}
