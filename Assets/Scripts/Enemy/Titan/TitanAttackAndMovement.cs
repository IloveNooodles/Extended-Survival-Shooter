using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAttackAndMovement : MonoBehaviour
{
    public TitanAudio titanAudio;
    public bool haveLeftArm = true;
    public bool haveRightArm = true;
    public int attackDamage = 10;
    public float attackDelay = 10f;

    GameObject player;
    PlayerHealth playerHealth;
    Transform playerTransform;
    float playerDistance;
    float lastHitTime = 0f;

    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
    bool isAttacking = false;
    bool isWalkingSoundPlayed = false;
    float walkingSoundTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        //Get Player data
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerTransform = player.GetComponent<Transform>();

        //Get Titan data
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(isAttacking ){
            return;
        }
        playerDistance = Vector3.Distance(playerTransform.position, transform.position);
        if(TitanHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && playerDistance > 35f)
        {
            anim.SetBool("isWalking", true);
            nav.SetDestination(playerTransform.position);
        }
        else
        {
            anim.SetBool("isWalking", false);
            gameObject.transform.LookAt(playerTransform);
            nav.enabled = false;
            int randomAttack = Random.Range(0, 4);
            if(randomAttack == 0){
                LeftArmAttack();
            }
            else if(randomAttack == 1){
                LeftFootAttack();
            }
            else if(randomAttack == 2){
                RightArmAttack();
            }
            else if(randomAttack == 3){
                RightFootAttack();
            }
            isAttacking = true;
        }
    }

    public void LeftArmAttack(){
        anim.SetTrigger("LeftArmAttack");
        StartCoroutine(ArmAttackDelay());
    }

    public void LeftFootAttack(){
        anim.SetTrigger("LeftFootAttack");
        StartCoroutine(FootAttackDelay());
    }

    public void RightArmAttack(){
        anim.SetTrigger("RightArmAttack");
        StartCoroutine(ArmAttackDelay());
    }

    public void RightFootAttack(){
        anim.SetTrigger("RightFootAttack");
        StartCoroutine(FootAttackDelay());
    }

    public void HitPlayer(){
        if(Time.time - lastHitTime < 1f){
            return;
        }
        lastHitTime = Time.time;
        playerHealth.TakeDamage(attackDamage);
        player.GetComponent<FPSMovement>().KnockBack();   
    }

    IEnumerator FootAttackDelay(){
        yield return new WaitForSeconds(2);
        titanAudio.Woosh();
        yield return new WaitForSeconds(attackDelay-2);
        isAttacking = false;
    }

    IEnumerator ArmAttackDelay(){
        yield return new WaitForSeconds(3.5f);
        titanAudio.Woosh();
        yield return new WaitForSeconds(attackDelay-3.5f);
        isAttacking = false;
    }

    IEnumerator AttackDelay(){
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }
}
