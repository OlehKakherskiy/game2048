using UnityEngine;

public enum Direction
{
    Left = -1,
    Right = 1,
    Up = -4,
    Down = 4
    
}
    public class InputManager:MonoBehaviour
    {
        private GameManager gm;

        public void Awake()
        {
            gm = GameObject.FindObjectOfType<GameManager> ();
        }
        
        public void Start()
        {
            
        }
        public void Update()
        {
            if (Input.GetKeyDown (KeyCode.RightArrow)) 
            {
                Debug.Log("Moving RIGHT");
                gm.doMoving(Direction.Right);
            } 
            else if (Input.GetKeyDown (KeyCode.LeftArrow)) 
            {
                Debug.Log("Moving LEFT");
                gm.doMoving(Direction.Left);
            }
            else if (Input.GetKeyDown (KeyCode.UpArrow)) 
            {
                Debug.Log("Moving UP");
                gm.doMoving(Direction.Up);
            }
            else if (Input.GetKeyDown (KeyCode.DownArrow)) 
            {
                Debug.Log("Moving DOWN");
                gm.doMoving(Direction.Down);
            }
        }
    }