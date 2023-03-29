using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    float h; //input horizontal movement
    float v; //input vertical movement
    bool isJumping = false;
    Animator anim;
    Rigidbody playerRigidbody;
    Transform playerTransform;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        //mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor"); 

        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();

        //Mendapatkan komponen Transform
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        //Mendapatkan nilai input horizontal (-1,0,1)
        h = Input.GetAxisRaw("Horizontal");

        //Mendapatkan nilai input vertical (-1,0,1)
        v = Input.GetAxisRaw("Vertical");

        //Mendapatkan nilai input jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //Method player dapat berjalan
    void Move(float h, float v)
    {
        //Set nilai x dan y
        movement.Set(h, 0f, v);

        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;

        //Move to position
        playerRigidbody.MovePosition(transform.position + movement);

        //Jump
        if (isJumping)
        {
            playerRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJumping = false;
        }
    }

    void Turning()
    {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Buat raycast untuk floorHit
        RaycastHit floorHit;

        //Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Mendapatkan vector daro posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Rotasi player
            playerRigidbody.MoveRotation(newRotation);
            
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}