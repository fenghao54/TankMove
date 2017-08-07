using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour
{
    private PlayerCharacter player;
    public int FloorMask;
    private Vector3 mousePos;
    public float speed;
    private Vector3 offset;
   // private Vector3 Target;
    List<Vector3> targets=new List<Vector3>();
	void Start ()
	{
	    player = GetComponent<PlayerCharacter>();
	    FloorMask = LayerMask.GetMask("Floor");
	}
	
	
	void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            targets.Add(GetPos());
        }
        if (targets.Count == 0)
        {
            return;
        }
        offset = player.transform.position - targets[0];
        if (Vector3.Angle(player.transform.forward, offset) <178)//如果没有旋转到位
        {
            Debug.Log(Vector3.Angle(player.transform.forward, offset));
            Debug.Log("开始旋转");
            Rotate();
        }
        else if (offset.magnitude > 0.5)//如果还有位移
        {
            Debug.Log("开始移动");
            player.transform.position += -offset.normalized * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("删除点0");
            targets.RemoveAt(0);
        }
    }
    Vector3 GetPos()//获得鼠标点击的点
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, FloorMask))
        {
            mousePos = hit.point;
            mousePos.y = 0;
        }
        return mousePos;
    }
    void Rotate()//向目标旋转插值
    {
        Debug.Log("检测");
        Quaternion q = new Quaternion();
        q.SetLookRotation(-offset);
        //Debug.Log(Vector3.Angle(player.transform.forward, offset));
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, q, 0.1f);
    }
}
