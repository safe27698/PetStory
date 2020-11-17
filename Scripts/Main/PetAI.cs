using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PetAnimation
{
    Idle,
    Carry,
    Sit,
    In1,
    In2,
    In3,
    WalkL,
    WalkR,
    Sleep,
    Jump,
    RunL,
    RunR
}

public enum PetActivity
{
    Stand,
    Sit,
    Walk,
    Run,
    Sleep,
    Jump,
    Carry,
    Happy,
    Sad
}


public class PetAI : MonoBehaviour
{
    [SerializeField]
    private PetAnimation petAni;
    public PetAnimation PetAni
    {
        get { return petAni; }
        set
        {
            if (petAni != value)
            {
                petAni = value;
                ani.SetTrigger(petAni.ToString());
            }
        }
    }

    [SerializeField]
    private PetActivity petAct;
    public PetActivity PetAct
    {
        get { return petAct; }
        set
        {
            petAct = value;
            Activity();
        }
    }
    public Animator ani;
    public Rigidbody2D rigidbody;
    public float timeDelay;
    public float directMovement = 1;
    public float speed;
    [Space]
    public List<Beacon> beacons = new List<Beacon>();
    public bool beaconOnOff;
    public string beaconUUID;
    public Text batTxt;
    public Text petStat;

    public int id;
    public string namePet;
    public GameObject emoHappy;
    public GameObject emooHungry;
    private float happiness;
    public float Happiness
    {
        get { return happiness; }
        set
        {
            if (value < 0)
                value = 0;

            if (value > 100)
                value = 100;

            if (value < 20)
                emoHappy.SetActive(true);
            else
                emoHappy.SetActive(false);

            happiness = value;
        }
    }

    private float hunger;
    public float Hunger
    {
        get { return hunger; }
        set
        {
            if (value < 0)
                value = 0;

            if (value > 100)
                value = 100;

            if (value < 20)
                emooHungry.SetActive(true);
            else
                emooHungry.SetActive(false);

            hunger = value ;
        }
    }
    [Space]

    public SpriteRenderer head;
    public SpriteRenderer ear;
    public SpriteRenderer eye;
    public SpriteRenderer eyebrow;
    public SpriteRenderer nose;
    public SpriteRenderer mouth;
    public SpriteRenderer pattern;
    public string shirtWearingPath;
    public string pantWearingPath;
    public string shoeWearingPath;
    public string acWearingPath;
    [Space]

    public SpriteRenderer body;
    public SpriteRenderer armLeft;
    public SpriteRenderer armRight;
    public SpriteRenderer legLeft;
    public SpriteRenderer legRight;
    [Space]

    public SpriteRenderer ac;
    public SpriteRenderer shirtBody;
    public SpriteRenderer shirtLeft;
    public SpriteRenderer shirtRight;
    public SpriteRenderer pantBody;
    public SpriteRenderer pantLeft;
    public SpriteRenderer pantRight;
    public SpriteRenderer shoeLeft;
    public SpriteRenderer shoeRight;
    [Space]


    public bool aleart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //        Debug.LogError(petStage);
        if (!beaconOnOff && aleart)
        {
            if (timeDelay <= 0)
            {
                timeDelay = Random.Range(5, 10);
                timeDelay = Random.Range(1, 5);
                PetAct = (PetActivity)Random.Range(0, 6);
                if (PetAct == PetActivity.Jump)
                    timeDelay = 2.2f;
            }
            else
            {
                timeDelay -= Time.deltaTime;
            }
        }
        
        if (PetAct == PetActivity.Run)
        {
            transform.Translate(Vector3.right  * speed * Time.deltaTime * directMovement) ;
        }
        else if (PetAct == PetActivity.Walk)
        {
            transform.Translate(Vector3.right * (speed/5) * Time.deltaTime * directMovement);
        }
        
    }

    public void UpdateBeacon()
    {
        if (beaconOnOff && beacons != null && beacons.Count > 0)
        {
            foreach (Beacon b in beacons)
            {
                if (b.UUID.ToString().ToLower() == beaconUUID.ToLower())
                {
                    string bat = b.instance.Substring(12, 2);
                    int m_Bat = int.Parse(bat, System.Globalization.NumberStyles.HexNumber);
                    batTxt.text = m_Bat.ToString();
                    switch (b.instance.Substring(10, 1))
                    {
                      
                    }
                    //petStat.text = "Activity : " + b.instance.Substring(10, 1) + " "+ petStage.ToString();
                    //Debug.Log("Pet are " + petStage.ToString());
                    beacons.Clear();
                    break;
                }
            }

        }
        else
        {
            batTxt.text = "";
        }
    }

    public void Activity()
    {
        switch (PetAct)
        {
            case PetActivity.Stand:
                PetAni = PetAnimation.Idle;
                break;
            case PetActivity.Sit:
                PetAni = PetAnimation.Sit;
                break;
            case PetActivity.Walk:
                if (directMovement < 0)
                    PetAni = PetAnimation.WalkL;
                else
                    PetAni = PetAnimation.WalkR;
                break;
            case PetActivity.Run:
                if (directMovement < 0)
                    PetAni = PetAnimation.RunL;
                else
                    PetAni = PetAnimation.RunR;
                break;
            case PetActivity.Sleep:
                PetAni = PetAnimation.Sleep;
                break;
            case PetActivity.Jump:
                PetAni = PetAnimation.Jump;
                rigidbody.AddForce(Vector2.up * 100);
                break;
            case PetActivity.Carry:
                PetAni = PetAnimation.Carry;
                break;
            case PetActivity.Happy:
                PetAni = (PetAnimation)Random.Range(3, 6);
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            directMovement *= -1;
            if (directMovement < 0 && PetAct == PetActivity.Walk)
                PetAni = PetAnimation.WalkL;
            else if (petAct == PetActivity.Walk)
                PetAni = PetAnimation.WalkR;

            if (directMovement < 0 && PetAct == PetActivity.Run)
                PetAni = PetAnimation.RunL;
            else if (petAct == PetActivity.Run)
                PetAni = PetAnimation.RunR;
            
        }
    }
}
