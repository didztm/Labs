using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum e_state
{
    INVALID = -1,
    JUMPING,
    FORWARDING,
    BACKING,
    CROUNCHING,
    STANDING,
    RUNNING,
    LEFT,
    RIGHT
}
public class Cube : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void
    e_state e_cube_state = e_state.STANDING;
    public LayerMask groundLayers;
    #endregion

    #region System

    void Awake()
    {
        

    }
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = transform;
    }
    void Update()
    {
        //Ici on défini les touches et les direction vers les états
        if (Input.GetKeyDown(KeyCode.Return)) {
            e_cube_state = e_state.JUMPING;
            
        }
        if (Input.GetKeyUp(KeyCode.Return)) {
            e_cube_state = e_state.STANDING;
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            e_cube_state = e_state.RUNNING;

        }
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            e_cube_state = e_state.STANDING;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            e_cube_state = e_state.JUMPING;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
            e_cube_state = e_state.FORWARDING;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            e_cube_state = e_state.LEFT;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            e_cube_state = e_state.BACKING;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            e_cube_state = e_state.RIGHT;
        if (Input.GetKeyDown(KeyCode.RightShift))
            e_cube_state = e_state.CROUNCHING;
        //Debug.Log(e_cube_state);
        //Ici on appelle l'état
        StateAction(e_cube_state);
        

    }
    private void FixedUpdate()
    {
        
    }
    #endregion

    #region Tools Debug and Utility
    private void StateAction(e_state state)
    {
        switch (state)
        {
            case e_state.STANDING:
                Move(m_speed);
                break;
            case e_state.FORWARDING:
                Move(m_speed);
                break;
            case e_state.LEFT:
                Move(m_speed);
                break;
            case e_state.RIGHT:
                Move(m_speed);
                break;
            case e_state.CROUNCHING:
                break;
            case e_state.JUMPING:
                Jump(jumpForce);
                e_cube_state = e_state.STANDING;
                break;
            case e_state.BACKING:
                Move(m_speed);
                break;
            case e_state.RUNNING:
                Move(10f);
                break;
            default:
                break;
        }
    }
    private void Move(float speed) {    
        //Attention à la position du gameobject sur la scène, cela peut changer les axes xyz pour les déplacements
        Vector3 v = Vector3.zero;
        v.x = m_transform.position.x+ Input.GetAxisRaw("Vertical") * Time.deltaTime*speed;
        v.z = m_transform.position.z+-(Input.GetAxisRaw("Horizontal") * Time.deltaTime*speed);
        v.y = m_transform.position.y;
        m_transform.position=v;
    }
    private void Jump(float jumpForce) {
        //TODO -> Mettre une condition qui détecte le sol car saut infini (cf collider.Raycast)
        m_rigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Force);
    }
    

    
    #endregion

    #region Private and Protected Members
    private Transform m_transform;
    private float jumpForce =500.0f;
    private float m_speed=5f;
    private Rigidbody m_rigidbody;
    #endregion
}
/*
  Axes Movements 
  --------------
     Vertical
         X 
         Up
         1     
Left  -1 0 1 Right  Z  Horizontal 
        -1
        Down
  Axes Jump
  ---------

   Y
   1
   0
*/