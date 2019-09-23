using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC_VAR

    #endregion

    #region PRIVATE_VAR
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    Animator anim;
    bool damageMade;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isIdle", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
        damageMade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            anim.SetBool("isAttacking", true);
            damageMade = true;
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") == true && damageMade == true)
        {


            Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
            if (hittedObjects.Length > 0)
            {
                int check = 0;
                for (int i = 0; i < hittedObjects.Length; i++)
                {
                    if (hittedObjects[i].gameObject != gameObject)
                    {

                        check = 1;
                        EnemyMovement enemy = hittedObjects[i].gameObject.GetComponent<EnemyMovement>();
                        if (enemy != null)
                        {
                            enemy.health -= damage;
                            enemy.hurted = true;
                        }
                        Debug.Log(enemy.health);
                    }
                }
                if (check == 1)
                    damageMade = false;

            }
        }

        float movement = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        if (movement != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack") == false)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);


            if (movement > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.Translate(transform.right * movement);
            }
            else if (movement < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.Translate(transform.right * movement);
            }
        }
    
        else if (movement == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack") == false)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
 
        }



    }


}
