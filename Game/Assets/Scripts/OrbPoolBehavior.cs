using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPoolBehavior : MonoBehaviour
{
    static int NumOrbs = 3;
    public GameObject Prefab;
    public List<GameObject> Orbs = new List<GameObject>();
    public int ActiveOrbCount;

    void Start()
    {
        for (int i = 0; i < NumOrbs; i++)
        {
           Vector3 pos = new Vector3(-10, 0, 0);
           Quaternion rot = Quaternion.Euler(0, 0, 0);
           GameObject NewOrb = (GameObject)Instantiate(Prefab, pos, rot);
           NewOrb.GetComponent<OrbBehavior>().OrbID = i;
           Orbs.Add(NewOrb);
        }

        ActiveOrbCount = 0;
    }

    public void Collect(int i){
        ActiveOrbCount--;
        Orbs[i].GetComponent<OrbBehavior>().OnCollect();
    }

    public void Launch(Vector2 i_Velocity, float i_StartX, float i_Scale){
        for(int i = 0; i < NumOrbs; i++){
            if (!Orbs[i].GetComponent<OrbBehavior>().Active){
                OrbBehavior Orb = Orbs[i].GetComponent<OrbBehavior>();
                Orb.ChangeParams(i_Velocity, i_StartX, i_Scale);
                Orb.Launch();
                ActiveOrbCount++;
                break;
            }
        }
    }

    public int GetActiveCount(){
        return ActiveOrbCount;
    }

    public int GetInactiveCount(){
        return NumOrbs - ActiveOrbCount;
    }

    public int GetTotalCount(){
        return NumOrbs;
    }



    public void RandomLaunch(){
        float r_Scale = Random.Range(0.3f, 4.0f);
        float r_StartX = Random.Range(-5f, 5f);
        float r_X = Random.Range(-0.3f, 0.3f);
        float r_Y = Random.Range(-1.7f, -4.8f);
        Vector2 r_Vel = new Vector2(r_X, r_Y);

        Launch(r_Vel, r_StartX, r_Scale);
    }

    public void CollectFirstActive(){
        for(int i = 0; i < NumOrbs; i++){
            if(Orbs[i].GetComponent<OrbBehavior>().Active){
                Collect(i);
                break;
            }
        }
    }


}
