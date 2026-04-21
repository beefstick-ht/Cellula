using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelTransition : MonoBehaviour
{
    public string triggererTag = "Player";
    public string playerSpawnTransformName = "NOT SET";
    public float enterSpeed = 1f;
    public string sceneToLoad;
    public GameObject fadeAnimation;

    private Canvas canvas;
    private Animator transitionAnimator;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        if(sceneToLoad == null)
        {
            throw new MissingReferenceException(name + " has no sceneToLoad set");
        }
        if(fadeAnimation == null)
        {
            throw new MissingReferenceException(name + " has no fadeAnimation set for the transition");
        }
    }

    private void Update()
    {
        //check if animation is done
        if(transitionAnimator != null)
        {
            //change levels when anim is done
            if(transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                LevelEvents.levelExit.Invoke(sceneToLoad, playerSpawnTransformName);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == triggererTag)
        {
            IDamageable playerDamageable = collider.gameObject.GetComponent<IDamageable>();

            if(playerDamageable != null)
            {
                //player cannot be hit
                playerDamageable.Invincible = true;
            }

            //player cannot move
            Rigidbody playerBody = collider.gameObject.GetComponent<Rigidbody>();
            playerBody.isKinematic = true;

            //player will walk towards the entrance direction
            Vector2 entranceDirection = (transform.position - playerBody.transform.position).normalized;

            playerBody.linearVelocity = entranceDirection * enterSpeed;

            transitionAnimator = Instantiate(fadeAnimation, canvas.transform).GetComponent<Animator>();
        }
    }
}
