using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text timer;
    public float jumpForce;
    public float jumpLimit;
    public LayerMask ground;
    public SphereCollider col;

    private Rigidbody rb;
    private int count;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        col = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveJump = Input.GetAxis("Jump");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (IsGrounded() && !(jumpLimit > moveJump))
        {
            //Vector3 jump = movement + new Vector3(0, moveJump, 0);
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, ground);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    void setCountText ()
    {
        countText.text = "Coins: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
        }
    }
}
