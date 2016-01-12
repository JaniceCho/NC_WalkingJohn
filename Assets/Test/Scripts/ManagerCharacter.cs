using UnityEngine;
using System.Collections;

public class ManagerCharacter : MonoBehaviour 
{
    public enum State
    {
        Idle, Right, Left, Up, Down, Dead
    }

    public float movePixel = 32.0f;
    public float moveSpeed = 1.0f;
    public State state = State.Idle;

    public GameObject johnGhost;

    [HideInInspector]
    public new Transform transform;
    private CharacterController _cc;
    private Animator _animator;

    private int _layermask;
    
    void Awake () 
	{
        transform = GetComponent<Transform>();
        //_cc = GetComponent<CharacterController>();
        //_animator = GetComponent<Animator>();
        _layermask = LayerMask.GetMask("TouchPad");
        movePixel /= 100;
    }
	
	void Update () 
	{
        if(ManagerGame.moveOn == true)
            Invoke(state.ToString(), 0.0f);

        if (state == State.Dead)
            return;

        if (ManagerGame.ghostMoved != true)
            ProcessInput();
	}

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManagerGame.ghostMoved = false;
            ManagerGame.moveOn = false;

            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                int layer = hit.transform.gameObject.layer;

                if (layer == LayerMask.NameToLayer("TouchPad"))
                {
                    string tag = hit.transform.gameObject.tag;

                    Vector2 johnsNextPosition = transform.position;

                    if (tag == "Right")
                    {
                        Debug.Log("Right");
                        johnsNextPosition.x += movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Right;
                    }
                    else if (tag == "Left")
                    {
                        Debug.Log("Left");
                        johnsNextPosition.x -= movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Left;
                    }
                    else if (tag == "Up")
                    {
                        Debug.Log("Up");
                        johnsNextPosition.y += movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Up;
                    }
                    else if (tag == "Down")
                    {
                        Debug.Log("Down");
                        johnsNextPosition.y -= movePixel;
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Down;
                    }
                    else if (tag == "Mid")
                    {
                        Debug.Log("Mid");
                        johnGhost.transform.position = johnsNextPosition;
                        ManagerGame.ghostMoved = true;
                        state = State.Idle;
                    }
                }
            }
        }
    }




    /** For Animation State **/
    void Idle()
    {

    }
}
