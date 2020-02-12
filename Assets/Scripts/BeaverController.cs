using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverController : MonoBehaviour
{
    
    public int score = 10;
    public AudioClip smashClip;
    
    private bool kicked;
    private GameManager gameManager;

    private Animator animator;

    void Start()
    {
        kicked = false;
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(spawn());
        
    }

    void OnMouseDown()
    { 
        StartCoroutine(destroyBeaver());
    }

     IEnumerator destroyBeaver()
    {
        gameManager.UpdateScore(score);
        animator.SetBool("isDie", true);
        kicked = true;
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().PlayOneShot(smashClip, 1f);

        yield return new WaitForSeconds(1.9f);
        Destroy(gameObject);
        
    }

   IEnumerator spawn()
    {
        yield return new WaitForSeconds(2.0f);
        int targetPosition = 100;
        while (targetPosition > 0 && kicked == false)
        {
            transform.Translate(new Vector3(0, -0.1f, 0), Space.World);
            targetPosition--;
            yield return null;
        }
        
    }
    
}
