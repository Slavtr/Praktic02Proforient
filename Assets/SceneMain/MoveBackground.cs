using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    float speed=50;
    private Transform back_Transform;
    private float back_Size;
    private float back_pos;


    // Start is called before the first frame update
    void Start()
    {
        back_Transform = GetComponent<Transform>();
        back_Size = GetComponent<SpriteRenderer>().bounds.size.x*2;




    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        back_pos = back_pos+ speed * Time.deltaTime;
        back_pos = Mathf.Repeat(back_pos, back_Size);
        back_Transform.position = new Vector3(back_pos,0, 0);
    }
}
