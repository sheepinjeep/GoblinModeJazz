using UnityEngine;

public class Saxophonist : MonoBehaviour
{
    public Animator animator;
    private int Pose;
    private float poseTimer = 0;
    public float maxPoseTime = 2;
    
    public void ChangePose()
    {
        Pose = Random.Range(1,11);
        poseTimer = 0;
        animator.SetInteger("Pose", Pose);
    }
    void Update()
    {
        poseTimer+=Time.deltaTime;
        if(poseTimer >= maxPoseTime && Pose!=0)
        {
            Pose = 0;
            animator.SetInteger("Pose", Pose);
        }
    }
}
