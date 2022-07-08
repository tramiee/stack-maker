using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public Vector3 targetPos;
    public GameObject stackHolder;
    public Transform characterTrans;
    public GameObject stackPrefab;
    public GameObject groundPrefab;

    public GameObject winEffects;

    private int numOfStacks = 0;
    void Start()
    {
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.Instance.swipeLeft)
            {
                direction = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
            {
                direction = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.swipeDown)
            {

                direction = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.swipeUp)
            {
                direction = Vector3.forward;
            }
            TargetPosition(direction);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                transform.position = targetPos;
                direction = Vector3.zero;
            }
        }

    }

    public void TargetPosition(Vector3 direction)
    {
        int layermask = LayerMask.GetMask("Wall");
        if(Physics.Raycast(transform.position, direction, out RaycastHit hit, Mathf.Infinity, layermask))
        {
            targetPos = transform.position + direction * (hit.distance - 0.5f);
            Debug.Log(targetPos);
        }
    }


    public void AddStack()
    {
        if (numOfStacks != 0)
        {
            characterTrans.position += Vector3.up * 0.3f;
        }
        GameObject newStack = Instantiate(stackPrefab) as GameObject;
        newStack.transform.SetParent(stackHolder.transform);
        newStack.transform.position = stackHolder.transform.position + Vector3.up * numOfStacks * 0.3f;
        numOfStacks += 1;
    }

    public void RemoveStack()
    {
        if(numOfStacks == 0)
        {
            return;
        }
        else
        {
            characterTrans.position -= Vector3.up * 0.3f;
            Destroy(stackHolder.transform.GetChild(stackHolder.transform.childCount - 1).gameObject);
            numOfStacks -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dash")
        {
            AddStack();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Undash")
        {
            RemoveStack();
            GameObject newGround = Instantiate(groundPrefab) as GameObject;
            newGround.transform.SetParent(other.transform);
            newGround.transform.position = other.transform.position + Vector3.up * 0.06f;
            other.gameObject.tag = "Untagged";
        }
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Finish");
            foreach (Transform stack in stackHolder.transform)
            {
                Destroy(stack.gameObject);
                characterTrans.position -= Vector3.up * 0.3f;
            }

            foreach (Transform effect in winEffects.transform)
            {
                effect.GetComponent<ParticleSystem>().Play();
            }
            characterTrans.GetComponent<Animator>().Play("Take 2");
            Invoke("Finish", 4);
        }
    }

    public void Finish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
