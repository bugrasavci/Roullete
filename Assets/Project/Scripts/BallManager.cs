using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour {
    
    public bool spinning = false;
    public Rigidbody ball;
    public Transform resultPoint;
    public Transform originPoint;

    public Transform pivotTransform;
    public Transform pivotWheelTransform;

    private float ballTimeSpeed = 1.3f;

    public Wheel wheel;

    private Transform Target;

    private static readonly Vector3 axis = Vector3.up;
    private float angularSpeed = 5f;
    private bool stopping = false;

    private Vector3 deltaAngularCross = Vector3.zero;

    private bool trigger_animateBall = true;

    private int res = -1;

    void Start () {
        ball.isKinematic = true;
    }

    public void StartSpin()
    {
        ball.isKinematic = true;
        ball.transform.SetParent(originPoint);
        ball.transform.localPosition = Vector3.zero;
        transform.SetParent(pivotTransform);
        transform.localRotation = Quaternion.identity;
        angularSpeed = 5;
        spinning = true;
        trigger_animateBall = true;
    }
    
    public void FindNumber(int result, bool isEuropean)
    {
        result = result == -1 && !isEuropean ? 37 : result;
        Target = wheel.resultCheckerObject[result].transform;
        res = result;
        StartCoroutine(GradualSpeedChange(angularSpeed, 1.5f, 5f));
    }
    private IEnumerator GradualSpeedChange(float startSpeed, float targetSpeed, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            angularSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        angularSpeed = targetSpeed;  // Ensure we set to target speed at the end
        stopping = true;
    }

    private bool bouncing = false;
    public void PlaceToResult(float angleRatio)
    {
        ball.transform.SetParent(resultPoint);
        Vector3 direction = (Target.position - resultPoint.position);
        bouncing = true;
        AudioManager.Instance.StopAuxiliarSound();
        StartCoroutine(BounceSound());
        StartCoroutine(MoveToResultPosition());
    }
    private IEnumerator MoveToResultPosition()
    {
        Vector3 startPosition = ball.transform.localPosition;
        Vector3 endPosition = Vector3.zero; // Hedef pozisyon
        float timeElapsed = 0f;
        float moveDuration = ballTimeSpeed; // Topun geçiş süresi

        // Yavaşça hedef pozisyona doğru hareket ediyoruz
        while (timeElapsed < moveDuration)
        {
            float lerpValue = timeElapsed / moveDuration;
            ball.transform.localPosition = Vector3.Lerp(startPosition, endPosition, lerpValue);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Hedef pozisyona ulaşınca topun pozisyonunu kesin olarak sıfırlıyoruz
        ball.transform.localPosition = endPosition;

        // Zıplama animasyonunu başlatıyoruz
        StartCoroutine(JumpAnimaiton());
    }
    private IEnumerator JumpAnimaiton()
    {
        Vector3 startPosition = ball.transform.localPosition;
        Vector3 endPosition = Vector3.zero; 
        float timeElapsed = 0f;
        float jumpHeight = 0.04f; 
        int jumpCount = 5; 
        float jumpDuration = ballTimeSpeed / jumpCount;

        while (timeElapsed < jumpDuration)
        {
           float lerpValue = timeElapsed / jumpDuration;
            float height = Mathf.Sin(lerpValue * Mathf.PI) * jumpHeight; // Zıplama yüksekliğini sinüs fonksiyonu ile animasyonel hale getiriyoruz

           ball.transform.localPosition = Vector3.Lerp(startPosition, endPosition, lerpValue) + Vector3.up * height;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

       ball.transform.localPosition = endPosition;

        bouncing = false;
    }

    private IEnumerator BounceSound()
    {
        while (bouncing)
        {
            yield return new WaitForSeconds(.3f);
            AudioManager.Instance.PlaySound(1);
        }
    }

    private float CalculateAngleRatio(Vector3 angularCross)
    {
        deltaAngularCross = angularCross - deltaAngularCross;

        Vector3 targetVector = (Target.position - transform.position);
        Vector3 ballVector = (ball.position - transform.position);

        targetVector.y = ballVector.y = 0;

        return (Vector3.Angle(ballVector, targetVector) / 180f);
    }

    private void FixedUpdate()
    {
        if (!spinning)
            return;

        transform.Rotate(axis, angularSpeed);

        if (stopping)
        {
            Vector3 angularCross = Vector3.Cross(transform.forward, (Target.position - transform.position).normalized);
            float angle = Vector3.SignedAngle(transform.forward, (Target.position - transform.position), transform.up);
            float angleRatio = CalculateAngleRatio(angularCross);
            if (deltaAngularCross.y > 0f)
            {
                if(angle < 35 && angle > 0)
                    angularSpeed = angleRatio * 2f;

                if (angleRatio <= 0.2f && trigger_animateBall && angle > 5)
                {
                    trigger_animateBall = false;
                    PlaceToResult(angleRatio);
                }
                else if (angleRatio <= 0.01f && !trigger_animateBall)
                {
                    spinning = false;
                    transform.SetParent(pivotWheelTransform);
                    ball.isKinematic = false;
                    stopping = false;
                    ResultManager.Instance.SetResult(res);
                }
            }       
        }
    }
}
