using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public float speed;
    public float speedStop = 0f;
    private Rigidbody rb;
    private bool isMoving = false;
    private Vector3 direction;
    //public GameObject dashParent;
    //public GameObject prevDash;
    //public GameObject mainStack;
   // public GameObject addStack;

    //public GameObject playerStack;
    //public GameObject playerBody;
    //public Transform characterPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
        if (isMoving)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.Instance.swipeLeft)
            {
                isMoving = true;
                direction = Vector3.left;
                //rb.velocity = Vector3.left * speed;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.Instance.swipeRight)
            {
                isMoving = true;
                direction = Vector3.right;

                //rb.velocity = Vector3.right * speed ;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.Instance.swipeDown)
            {
                isMoving = true;
                direction = Vector3.back;

                //rb.velocity = Vector3.back * speed ;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.Instance.swipeUp)
            {
                isMoving = true;
                direction = Vector3.forward;

                //rb.velocity = Vector3.forward * speed ;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void AddStack()
    {

    }

    public void RemoveStack()
    {

    }

    public void ResetStack()
    {
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Debug.Log("wall");
            //speed = speedStop;
            //direction = Vector3.zero;
            //rb.velocity = Vector3.zero;
            isMoving = false;
            //direction = Vector3.zero;
        }
    }/

    /*public void TakeDash(GameObject dash)
    {
        dash.transform.SetParent(dashParent.transform);
        Vector3 pos = prevDash.transform.localPosition;
        pos.y -= 0.25f;
        dash.transform.localPosition = pos;

        Vector3 characterPos = transform.localPosition;
        characterPos.y += 0.25f;
        transform.localPosition = characterPos;
        prevDash = dash;
        prevDash.GetComponent<BoxCollider>().isTrigger = false;
    }


    public void AddStack()
    {
        //tang chieu cao cua character
        Vector3 character = characterPos.localPosition;
        character.y += 0.2f;
        characterPos.localPosition = character;
        //tao them 1 stack duoi chan
        Instantiate(addStack, transform.position, Quaternion.identity);
       

    }

    public void RemoveStack()
    {
        //check con stack duoi chan khong
        //neu co giam chieu cao nhan vat, tru stack
        //k co thi thoi
    }

    public void ResetStack()
    {
        //Reset toan bo nhan vat va stack
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dash")
        {
            //other.gameObject.tag = "Normal";
            //PlayerControl.instance.TakeDash(other.gameObject);
            //Vector3 pos = other.transform.position;
            //pos.y = transform.position.y;
            //transform.position = pos;
            //other.gameObject.AddComponent<Rigidbody>();
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //other.gameObject.AddComponent<StackScripts>();
            //Destroy(this);

            AddStack();
            Destroy(other.gameObject);

        }
        if (other.tag == "Undash")
        {
            RemoveStack();
        }

        if(other.gameObject.tag == "Wall")
        {
            isMoving = false;
        }
    }*/
}
