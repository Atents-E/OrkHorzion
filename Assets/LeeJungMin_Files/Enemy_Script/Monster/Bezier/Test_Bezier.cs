using System.Collections;
using UnityEngine;
using UnityEditor;


public class Test_Bezier : MonoBehaviour
{
    public Transform target;

    [Range(0,1)]
    public float testTime = 0.5f;

    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    private void Awake()
    {
        p1 = transform.position;
        p2 = transform.position + transform.up * 6.0f;
        p3 = target.position + transform.up * 10.0f;
        p4 = target.position;
    }

    private void Update()
    {
        testTime += Time.deltaTime * 0.7f;
        transform.position = BezierTest(p1, p2, p3, p4, testTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            Destroy(this.gameObject,0.1f);
        }
    }

    // 3차원 곡선
    public Vector3 BezierTest(Vector3 P_1, Vector3 P_2, Vector3 P_3, Vector3 P_4, float value)
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, value);
        Vector3 B = Vector3.Lerp(P_2, P_3, value);
        Vector3 C = Vector3.Lerp(P_3, P_4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);
        return F;
    }

    // 2차원 곡선
    public Vector3 BezierTest2(Vector3 P_1, Vector3 P_2, Vector3 P_3, float value)
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, value);
        Vector3 B = Vector3.Lerp(P_2, P_3, value);

        Vector3 D = Vector3.Lerp(A, B, value);

        return D;
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Test_Bezier))]
public class Test2_Bezier : Editor
{
    private void OnSceneGUI()
    {
        Test_Bezier Generator = (Test_Bezier)target;

        Generator.p1 = Handles.PositionHandle(Generator.p1, Quaternion.identity);
        Generator.p2 = Handles.PositionHandle(Generator.p2, Quaternion.identity);
        Generator.p3 = Handles.PositionHandle(Generator.p3, Quaternion.identity);
        Generator.p4 = Handles.PositionHandle(Generator.p4, Quaternion.identity);

        Handles.DrawLine(Generator.p1, Generator.p2);
        Handles.DrawLine(Generator.p3, Generator.p4);

        int count = 50;
        for (float i = 0; i < count; i++)
        {
            float Value_Before = i / count;
            Vector3 Before = Generator.BezierTest(Generator.p1, Generator.p2, Generator.p3, Generator.p4, Value_Before);

            float Value_After = (i + 1) / count;
            Vector3 After = Generator.BezierTest(Generator.p1, Generator.p2, Generator.p3, Generator.p4, Value_After);

            Handles.DrawLine(Before, After);
        }
    }

}
#endif