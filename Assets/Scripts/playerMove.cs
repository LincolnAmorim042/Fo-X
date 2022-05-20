using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class playerMove : MonoBehaviour{
    public int pontuacao;

    public SpriteRenderer player;
    public Rigidbody2D playerBody;
    public bool chao;
    public Animator animator;

    private AudioSource source;
    public AudioClip jump;
    public AudioClip point;

    public float velocidade;
    private float dirX = 0f;

    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards
    private float contactThreshold = 30;          // Acceptable difference in degrees

    // Start is called before the first frame update
    void Start(){
        chao = true;
        velocidade = 4;
        pontuacao = 0;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
        dirX = Input.GetAxisRaw("Horizontal");
        if(Mathf.Approximately(dirX, 0.0f)){
            dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        }
        playerBody.velocity = new Vector2(dirX * velocidade, playerBody.velocity.y);
        
        if (dirX < 0){
            animator.SetBool("andando", true);
            player.flipX = true;
        } else if (dirX > 0){
            animator.SetBool("andando", true);
            player.flipX = false;
        } else{
            animator.SetBool("andando", false);
        }

        if ((Input.GetButtonDown("Jump") && chao) || (CrossPlatformInputManager.GetButtonDown("Jump") && chao)){
            playerBody.velocity = new Vector2(playerBody.velocity.x, velocidade*2);
            source.PlayOneShot(jump, 1);

            chao = false;
            animator.SetBool("pulando", true);
        } 

        if (Input.GetKey(KeyCode.Escape)){
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("start")){
                SceneManager.LoadScene("start");
            } else {
                Application.Quit();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "chao" || other.gameObject.tag == "errada" || other.gameObject.tag == "certa"){
            for (int k = 0; k < other.contacts.Length; k++){
                if (Vector3.Angle(other.contacts[k].normal, validDirection) <= contactThreshold){
                    chao = true;
                    animator.SetBool("pulando", false);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "goal"){
            SceneManager.LoadScene("start");
        }
        if (other.gameObject.tag == "1"){
            SceneManager.LoadScene("first");
        }
        if (other.gameObject.tag == "2"){
            SceneManager.LoadScene("second");
        }
        if (other.gameObject.tag == "3"){
            SceneManager.LoadScene("third");
        }
        if(other.gameObject.tag == "4"){
            SceneManager.LoadScene("fourth");
        }
        if (other.gameObject.tag == "certa"){
            pontuacao++;
            source.PlayOneShot(point, 1);
            Destroy(other.gameObject);
        }
    }
}