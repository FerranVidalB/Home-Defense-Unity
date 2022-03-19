using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    private GameObject goal;
    private Animator anim;
    private NavMeshAgent agent;
    private bool baseCollided;
    private float loadTime = 2f;
    private float stopTimer = 0.1f;
    private bool dead;
    private Vector3 velocity;


    public int startHealth = 100;
    public Canvas canvas;
    public float health = 100;
    public int attackDMG=1;
    public int worth = 35;
    public Image healthBar;

    void Start() {
        goal = GameObject.Find("Base");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;
        anim.SetBool("Run Forward", true);
        velocity = Vector3.zero;
        baseCollided = false;
        health = startHealth;
        dead = false;
    }

    private void Update() {
        //canvas.transform.rotation = Quaternion.Inverse(transform.rotation);
        //canvas.transform.rotation = Quaternion.EulerAngles(60, Quaternion.Inverse(transform.rotation).y, Quaternion.Inverse(transform.rotation).z);
        //canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.rotation = Camera.main.transform.rotation;
        /*NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(goal.position, path);
        Debug.Log(path.status);*/
        //Debug.Log(agent.hasPath);
        /* if (!baseCollided) {
             if (agent.velocity == Vector3.zero && loadTime <= 0f) {
                 agent.enabled = false;
                 Vector3 dir = goal.position - transform.position;
                 Quaternion lookRotation = Quaternion.LookRotation(dir);
                 Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
                 transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                 transform.position = Vector3.SmoothDamp(transform.position, goal.position, ref velocity, 4f);
             } else {
                 loadTime -= Time.deltaTime;
             }
         }*/
        if (!baseCollided) {
            if (agent.velocity == Vector3.zero && loadTime <= 0f && !dead) {
                if (stopTimer <= 0f) {
                    agent.enabled = false;
                    Vector3 dir = goal.transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(dir);
                    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
                    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                    transform.position = Vector3.SmoothDamp(transform.position, goal.transform.position, ref velocity, 4f);
                }
                stopTimer -= Time.deltaTime;
            } else {
                loadTime -= Time.deltaTime;
                stopTimer = 0.1f;
            }
        }
        //if(Vector3.Distance(transform.position, goal.position) < 3

        /*if (Input.GetMouseButton(0))
            anim.SetBool("Walk Forward", true);
        else if (Input.GetMouseButton(1))
            anim.SetBool("Walk Forward", false);


        if (anim.GetBool("Walk Forward"))
            agent.isStopped = true;
        else
            agent.isStopped = false;*/
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Base") {
            baseCollided = true;
            StartCoroutine(AttackBase());
        }
    }

    public void GetShooted() {
        health -= 35;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0) {
            
            StartCoroutine(Die());
        } else {

        }
    }

    IEnumerator Die() {
        health = 0;
        canvas.enabled = false;
        dead = true;
        goal.GetComponent<Base>().SetTotalCoins(worth);
        anim.SetBool("Run Forward", false);
        agent.isStopped = true;
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    IEnumerator AttackBase() {
        if (!agent.enabled) {
            agent.enabled = true;
        }
        agent.isStopped = true;
        anim.SetBool("Run Forward", false);
        anim.SetBool("Attack 02", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack 02", false);
        if (health > 0)
        {
            goal.GetComponent<Base>().Damage(attackDMG);
        }
        StartCoroutine(Die());
    }
}
