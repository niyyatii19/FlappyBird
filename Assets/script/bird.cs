using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour {
    public float jumpforce =3.5f; 
    bool added;
    public GameObject playbut;
    Rigidbody2D mybody; 
    Animator myanim;
    AudioSource audio;
    public AudioClip ping, die, flapping;

    // Use this for initialization
    void Start () {
        myanim = GetComponent<Animator> ();
        mybody = GetComponent<Rigidbody2D> ();

        audio = GetComponent<AudioSource> ();
    }
    
    // Update is called once per frame
    void FixedUpdate () { 
           
             if (Input.GetKeyDown(KeyCode.Escape)){
                  Application.Quit();
              }
            if (added == false) {
                //gameObject.AddComponent<Rigidbody2D> ();
    
                added = true;
            }
            float angel;  
            if (mybody.velocity.y > 0) {
                angel =    Mathf.Lerp (0, 90, mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler (0, 0, angel);
            } else if (mybody.velocity.y <= 0) {
                angel = Mathf.Lerp (0, -90, -mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler (0, 0, angel);
            }

    }
    public void birdjump(){
        mybody.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

            if (myanim.GetBool ("died") == false) {

                mybody.velocity = new Vector2 (mybody.velocity.x, jumpforce);
                audio.PlayOneShot (flapping);  
            }

    
        }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "space") {
            audio.PlayOneShot (ping);
            gameeditor.instance.score++;

        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "pipe") {
            if (myanim.GetBool ("died") == false) {
                audio.PlayOneShot (die);
            }
            myanim.SetBool ("died", true);// biến died là true
            Instantiate(playbut);

        }
    }

}