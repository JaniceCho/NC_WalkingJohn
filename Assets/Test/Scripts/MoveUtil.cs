using UnityEngine;
using System.Collections;

public class MoveUtil : MonoBehaviour
{ 
    // 한 프레임씩 이동시키고, 목표지점까지 남은 거리 반환.
    public static float MoveByFrame(
        Transform self,
        Transform target,
        float moveSpeed)
    {
        Vector2 targetPos = target.position;
        Vector2 framePos = Vector2.MoveTowards(
            self.position, targetPos, moveSpeed * Time.deltaTime);
        self.position = framePos;
        return Vector2.Distance(framePos, targetPos);
    }
}
