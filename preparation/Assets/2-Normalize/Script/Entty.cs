using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entty : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    public virtual float _hp
    {
        get { return hp; }
        set { hp = value; }
    }
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected GameObject bullet;

    protected float attackDelay = 0;

    private SpriteRenderer spriteRenderer;

    public bool isInvi;
    public bool isHitEffect;
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (_hp <= 0) Dead();
    }
    public virtual void OnHit(float damage)
    {
        if (isInvi == false)
        { 
            _hp -= damage;
        }
        if(isHitEffect == false)
            StartCoroutine(HitAnim());
    }
    private IEnumerator HitAnim()
    {
        isHitEffect = true;

        Color beforeColor = spriteRenderer.color;
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime * 4;
            spriteRenderer.color = new Color(1, timer, timer);
            yield return null;
        }
        while (timer < 1)
        {
            timer += Time.deltaTime * 4;
            spriteRenderer.color = new Color(beforeColor.r, timer, timer);
            yield return null;
        }
        spriteRenderer.color = beforeColor;
        isHitEffect = false;

    }
    protected abstract void Dead();
}
