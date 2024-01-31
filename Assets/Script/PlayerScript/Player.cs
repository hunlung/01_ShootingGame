using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    PlayerInputAction inputActions;
    /// <summary>
    /// 목숨
    /// </summary>
    private int life = 3;
    /// <summary>
    /// 목숨 파라메터
    /// </summary>
    private int Life
    {
        get => life;
        set
        {
            if (life != value) //목숨 변동시
            {
                life = value;
                if (IsAlive == true) // 살아있다면
                {
                    //StopCoroutine(reviveCoroutine);
                    StopAllCoroutines();
                    Factory.Instance.GetExplosionEffect(transform.position); // 폭발하고
                    Revive(); // 부활

                }
                else
                {
                    gameObject.SetActive(false); // 목숨이 없으면 끄기
                    OnEnd?.Invoke();
                }
                life = Math.Clamp(life, 0, startlife); //목숨 최대치
                onDie?.Invoke(life); // 맞은걸 알리기
            }
        }
    }

    private bool IsAlive => life > 0;
    public Action OnEnd;

    /// <summary>
    /// 시작 목숨
    /// </summary>
    const int startlife = 3;

    private int boom;
    /// <summary>
    /// 시작 폭탄 개수
    /// </summary>
    const int StartBoom = 2;
    /// <summary>
    /// 최대 폭탄 개수
    /// </summary>
    const int MaxBoom = 6;
    /// <summary>
    /// 폭탄 파라메터
    /// </summary>
    private int Boom
    {
        get => boom;
        set
        {
            if (boom != value)
            {
                boom = value;

                boom = Math.Clamp(boom, 0, MaxBoom); //폭탄 최소,최대
                onBoom.Invoke(boom); // 폭탄사용 알리기
            }
        }
    }
    bool isboom => boom > 0;
    public Action<int> onBoom;
    /// <summary>
    /// 점수
    /// </summary>
    private int score = 0;
    /// <summary>
    /// 점수 파라메터
    /// </summary>
    public int Score
    {
        get => score;
        private set
        {
            if (score != value)// 점수 변동시
            {
                score = Math.Min(value, 9999999);
                onScoreChange?.Invoke(score); // 점수변동 알리기(스코어바꾸기)
            }
        }
    }
    public Action<int> onScoreChange;

    Rigidbody2D rigid2d;
    SpriteRenderer spriteRenderer;
    /// <summary>
    /// 이동속도
    /// </summary>
    public float Player_Speed = 1.0f;
    /// <summary>
    /// 연사속도
    /// </summary>
    public float Shoot_interval = 0.2f;
    /// <summary>
    /// 파워샷 속도
    /// </summary>
    public float PowerShoot_interval = 0.7f;
    /// <summary>
    /// 부활하는데 걸리는 시간
    /// </summary>
    public float reviveinterval = 1f;
    /// <summary>
    /// 차지시간
    /// </summary>
    public float Chargetime = 2f;
    /// <summary>
    /// 파워 상태
    /// </summary>
    public int power;
    Vector3 InputDir = Vector3.zero;
    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    Transform ShootTransform;
    /// <summary>
    /// 차지샷 위치
    /// </summary>
    Transform ChargeTransform;
    /// <summary>
    /// 시간재기용
    /// </summary>
    float deltatime;

    bool charge1, charge2, charge3, charge4 = false;
    bool ischargeend = true;
    bool isboomcool = false;

    IEnumerator shootCoroutine;
    IEnumerator powershootCoroutine;
    IEnumerator reviveCoroutine;
    IEnumerator chargeCoroutine;
    IEnumerator chargeshootCoroutine;
    public GameObject chargeattack;



    public Action<int> onDie;

    private void Awake()
    {

        rigid2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러
        inputActions = new PlayerInputAction(); //인풋액션 초기화
        ShootTransform = transform.GetChild(0); // 샷이 원하는곳에서 나가게 하기
        ChargeTransform = transform.GetChild(5);
        shootCoroutine = ShootCoroutine();
        powershootCoroutine = PowerShootCoroutine(); // 코루틴들 설정
        reviveCoroutine = ReviveCoroutine();
        chargeCoroutine = ChargeCoroutine();

        power = 0; // 파워는 기본 0


    }


    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Shoot.performed += OnShootStart;       //키를 입력시 발동
        inputActions.Player.Shoot.canceled += OnShootEnd;        //키를 뗄 시 발동
        inputActions.Player.Charge.performed += OnChargeStart;
        inputActions.Player.Charge.canceled += OnChargeEnd;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Boom.performed += OnBoom;

    }
    private void OnDisable()
    {
        inputActions.Player.Shoot.canceled -= OnShootEnd;
        inputActions.Player.Shoot.performed -= OnShootStart;
        inputActions.Player.Charge.canceled -= OnChargeEnd;
        inputActions.Player.Charge.performed -= OnChargeStart;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove; // 뗏을 때 차지공격 함수 작동
        inputActions.Player.Boom.performed -= OnBoom;
        inputActions.Player.Disable();
    }

    /// <summary>
    /// 키를 누르면 연사
    /// </summary>
    /// <param name="_"></param>
    private void OnShootStart(InputAction.CallbackContext _)
    {
        StartCoroutine(powershootCoroutine);
        StartCoroutine(shootCoroutine);

    }
    /// <summary>
    /// 키를 떼면 연사종료
    /// </summary>
    /// <param name="_"></param>
    private void OnShootEnd(InputAction.CallbackContext _)
    {
        StopCoroutine(powershootCoroutine);
        StopCoroutine(shootCoroutine);
    }
    /// <summary>
    /// 기본샷 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot(ShootTransform.position);
            yield return new WaitForSeconds(Shoot_interval);

        }
    }
    /// <summary>
    /// 파워샷 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator PowerShootCoroutine()
    {
        while (true)
        {

            powerShoot();
            yield return new WaitForSeconds(PowerShoot_interval);
        }
    }
    /// <summary>
    /// 기본 총알 발사
    /// </summary>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    void Shoot(Vector3 position, float angle = 0.0f)
    {
        Factory.Instance.GetBullet(transform.position);
    }
    /// <summary>
    /// 옵션에서 총알 발사
    /// </summary>
    void powerShoot()
    {

        if (transform.GetChild(1).gameObject.activeSelf)
        {
            Factory.Instance.GetPowerShoot(transform.GetChild(1).position);
            if (transform.GetChild(2).gameObject.activeSelf)
            {
                Factory.Instance.GetPowerShoot(transform.GetChild(2).position);
                if (transform.GetChild(3).gameObject.activeSelf)
                {
                    Factory.Instance.GetPowerShoot(transform.GetChild(3).position);
                    if (transform.GetChild(4).gameObject.activeSelf)
                    {
                        Factory.Instance.GetPowerShoot(transform.GetChild(4).position);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 키를 누르면 차지
    /// </summary>
    /// <param name="context"></param>
    private void OnChargeStart(InputAction.CallbackContext context) // 감속
    {
        deltatime = 0f;
        inputActions.Player.Shoot.Disable();
        StartCoroutine(chargeCoroutine);

    }
    /// <summary>
    /// 떼면 차지종료
    /// </summary>
    /// <param name="context"></param>
    private void OnChargeEnd(InputAction.CallbackContext context) // 감속
    {
        inputActions.Player.Shoot.Enable();
        StopCoroutine(chargeCoroutine);
        if (ischargeend == true)
        {
            ischargeend = false;
            



            if (ChargeTransform.GetChild(0).gameObject.activeSelf == true) // 1번옵션이있으면
            {
                ChargeTransform.GetChild(0).gameObject.SetActive(false); //위치보여주기용은 끄고
                Factory.Instance.GetChargeShoot(ChargeTransform.GetChild(0).position);
            }
            if (ChargeTransform.GetChild(1).gameObject.activeSelf == true) // 2번옵션이있으면
            {
                ChargeTransform.GetChild(1).gameObject.SetActive(false); //위치보여주기용은 끄고
                Factory.Instance.GetChargeShoot(ChargeTransform.GetChild(1).position);
            }
            if (ChargeTransform.GetChild(2).gameObject.activeSelf == true) // 3번옵션이있으면
            {
                ChargeTransform.GetChild(2).gameObject.SetActive(false); //위치보여주기용은 끄고
                Factory.Instance.GetChargeShoot(ChargeTransform.GetChild(2).position);
            }
            if (ChargeTransform.GetChild(3).gameObject.activeSelf == true) // 4번옵션이있으면
            {
                ChargeTransform.GetChild(3).gameObject.SetActive(false); //위치보여주기용은 끄고
                Factory.Instance.GetChargeShoot(ChargeTransform.GetChild(3).position);
            }
            else
            {
                ischargeend = true;
            }
            
            StopCoroutine(ChargeShootCoroutine());
            StartCoroutine(ChargeShootCoroutine()); // 6초동안 차지샷
            
        }



    }

    IEnumerator ChargeShootCoroutine()
    {
        float time = 0;

        while (time < 6.0f) //6초가 지나면
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (charge1 == true) // 켜져있는 차지샷 끄고
        {
            charge1 = false;
            transform.GetChild(1).gameObject.SetActive(true); //옵션 활성화
        }
        if (charge2 == true)
        {
            charge2 = false;
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if (charge3 == true)
        {
            charge3 = false;
            transform.GetChild(3).gameObject.SetActive(true);
        }
        if (charge4 == true)
        {
            charge4 = false;
            transform.GetChild(4).gameObject.SetActive(true);
        }
        ischargeend = true;
    }




    /// <summary>
    /// 차지샷 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChargeCoroutine()
    {
        deltatime = 0f;
        while (true)
        {
            deltatime += Time.deltaTime;
            if (deltatime > 1.2f) // 1.2초 이상 차지했으면 //적용할것 : 옵션 활성화 확인 후 활성화된 놈들만 차지샷 위치에서 10초간 발사
            { // 적용할것: 활성화된놈들 비활성화->차지샷위치에서 발사 -> 끝나면 비활 후 비활되있던 옵션들 활성화
                if (transform.GetChild(1).gameObject.activeSelf == true)
                {
                    transform.GetChild(1).gameObject.SetActive(false);      //1.2초이상 차지하면 옵션
                    ChargeTransform.GetChild(0).gameObject.SetActive(true); //비활,차지샷 위치 활성화
                    charge1 = true;
                }
                if (transform.GetChild(2).gameObject.activeSelf == true)
                {
                    transform.GetChild(2).gameObject.SetActive(false);
                    ChargeTransform.GetChild(1).gameObject.SetActive(true);
                    charge2 = true;
                }
                if (transform.GetChild(3).gameObject.activeSelf == true)
                {
                    transform.GetChild(3).gameObject.SetActive(false);
                    ChargeTransform.GetChild(2).gameObject.SetActive(true); //차지샷 위치에서 발사, 끝나면 비활후 비활된 옵션 활성화
                    charge3 = true;
                }
                if (transform.GetChild(4).gameObject.activeSelf == true) //차지샷위치발사는 팩토리쓸 예정, 초 센후 비활된 옵션 활성화
                {
                    transform.GetChild(4).gameObject.SetActive(false);
                    ChargeTransform.GetChild(3).gameObject.SetActive(true);
                    charge4 = true;
                }


            }
            yield return null;
        }


    }
    /// <summary>
    /// 방향 벡터 입력
    /// </summary>
    /// <param name="context"></param>
    private void OnMove(InputAction.CallbackContext context)
    {

        InputDir = context.ReadValue<Vector2>(); //방향에 따른 이동 변수 입력
    }
    /// <summary>
    /// 폭탄사용
    /// </summary>
    /// <param name="context"></param>
    private void OnBoom(InputAction.CallbackContext context)
    {
        if (isboom == true) // 폭탄이 있으면
        {
            if(isboomcool == false) // 폭탄이 끝났으면
            {

            StopCoroutine(invisibleCoroutine());
            Factory.Instance.GetBoom(transform.position); // 폭탄 사용
            Boom--;
            StartCoroutine(invisibleCoroutine());
            }
        }
    }

    IEnumerator invisibleCoroutine()
    {
        isboomcool = true;
        float intime = 0f;
        gameObject.layer = 10; // 무적 레이어 만들기
        while (intime < 2.0f) // 무적시간
        {
            intime += Time.deltaTime;
            float alpha = (MathF.Cos(intime * 30.0f) + 1.0f) * 0.5f; //코사인 결과를 1~0으로 만들기(cos=-1~1, +1로 0~2->나눠서0~1
            spriteRenderer.color = new Color(1, 1, 1, alpha); // 알파에 지정(깜빡거리기)
            yield return null;
        }
        isboomcool = false;
        gameObject.layer = 3; // 무적 해제
        spriteRenderer.color = new Color(1, 1, 1, 1); // 깜박거리기 제거
    }


    void Start()
    {

        life = startlife; // 목숨,폭탄 초기화
        boom = StartBoom;
    }



    private void FixedUpdate() // 물리연산이 이루어질때 호출되는 함수
    {
        //transform.Translate(Time.deltaTime * inputDir * moveSpeed); // 1초당 moveSpeed만큼의 속도로, inputDir의 방향으로 이동한다.
        rigid2d.MovePosition(rigid2d.position + (Vector2)(Time.fixedDeltaTime * InputDir * Player_Speed));//rigid2d로 움직이기

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EBullet")) //적이나 총알에 맞으면


        {
            Life--; // 라이프 깎기
            if(Boom < StartBoom)
            {
                boom = StartBoom;
                onBoom.Invoke(boom);
            }
        }






        else if (collision.gameObject.CompareTag("PowerItem")) //아이템 먹으면 파워업하기
        {
            collision.gameObject.SetActive(false);
            power++;
            switch (power)
            {
                case 1: transform.GetChild(1).gameObject.SetActive(true); break;
                case 2: transform.GetChild(2).gameObject.SetActive(true); break;
                case 3: transform.GetChild(3).gameObject.SetActive(true); break;
                case 4: transform.GetChild(4).gameObject.SetActive(true); break;
                default: break;
            }
        }
        else if (collision.gameObject.CompareTag("BoomItem"))
        {
            collision.gameObject.SetActive(false);
            Boom++;
        }

    }
    /// <summary>
    /// 죽으면 부활하는 함수
    /// </summary>
    private void Revive()
    {
        int random = UnityEngine.Random.Range(1, 2); // 죽을 때 떨구는 파워 수
        if (power > 2) // 파워가 2 이상이면
        {

            for (int i = 0; i < random; i++)
            {
                Factory.Instance.GetPowerItem(transform.position); // 파워 1~2개 생성
            }

        }
        power = 0; // 파워 초기화, 옵션 초기화
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        StartCoroutine(ReviveCoroutine());

    }
    /// <summary>
    /// 부활 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReviveCoroutine()
    {
        float retime = 0f;
        gameObject.transform.position = new Vector3(0, -7.8f, 0); // 밑으로 이동
        gameObject.layer = 12; // 무적 레이어 만들기
        inputActions.Player.Disable(); // 아무행동도 못하는 상태
        while (retime < reviveinterval)
        {

            retime += Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime * 2f); // 밑에서 위로 올라오는 연출

            yield return null;
        }
        gameObject.layer = 10;
        inputActions.Player.Enable(); // 다올라왓으면 행동 가능
        retime = 0;
        while (retime < 4.0f) // 무적시간
        {
            retime += Time.deltaTime;
            float alpha = (MathF.Cos(retime * 30.0f) + 1.0f) * 0.5f; //코사인 결과를 1~0으로 만들기(cos=-1~1, +1로 0~2->나눠서0~1
            spriteRenderer.color = new Color(1, 1, 1, alpha); // 알파에 지정(깜빡거리기)

            yield return null;
        }
        gameObject.layer = 3; // 무적 해제
        spriteRenderer.color = new Color(1, 1, 1, 1); // 깜박거리기 제거

    }


    /// <summary>
    /// 점수 추가하는 변수
    /// </summary>
    /// <param name="getScore">새로 얻은 점수를 스코어에 넣기</param>
    public void AddScore(int getScore)
    {
        Score += getScore;
    }






}
