using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class Movement : MonoBehaviour
{
    // [SerializeField] private AudioClip characterMoveSoundClip;
    Rigidbody2D rgb2d;

    [HideInInspector]
    public Vector3 movementVector;

    Animate animate;

    [HideInInspector]
    public float lastHorizontalDeCoupledVector;

    [HideInInspector]
    public float lastVerticalDeCoupledVector;
    [HideInInspector]
    public float lastHorizontalCoupledVector;

    [HideInInspector]
    public float lastVerticalCoupledVector;

    [SerializeField] public float speed = 3f;

    // Start is called before the first frame update
    private void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    private void Start()
    {
        // lastHorizontalVector = -1f;
        lastHorizontalDeCoupledVector = 1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = 1f;
        lastVerticalCoupledVector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");


        if (movementVector.x != 0 || movementVector.y != 0)
        {
            // if (Time.frameCount % 60 == 0) { SFXManager.instance.PlaySoundFXClip(characterMoveSoundClip, transform, 1f); }
            lastHorizontalCoupledVector = movementVector.x;
            lastVerticalCoupledVector = movementVector.y;
        }

        if (movementVector.x != 0)
            lastHorizontalDeCoupledVector = movementVector.x;
        if (movementVector.y != 0)
            lastVerticalDeCoupledVector = movementVector.y;

        animate.horizontal = movementVector.x;

        movementVector *= speed;
        rgb2d.velocity = movementVector;
    }
}
