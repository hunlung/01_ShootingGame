using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;


public enum PoolObjectType
{
    PlayerBullet = 0,
    HitEffect,
    ExplosionEffect,
    RushingEnemy,
    TwoShootEnemy,
    Boom,
    PowerItem,
    PowerShoot,
    ShootEnemy,
    EBullet,
    ChargeShoot,
    BoomItem,
    ItemEnemy,
    ChargeBulet

}

public class Factory : Singleton<Factory>
{
    BulletPool bullet; // Ǯ ����
    BoomPool boom;
    HitPool hit;
    ExplosionPool explosion;
    RushingEnemyPool rushingenemy;
    TwoShootEnemyPool twoshootenemy;
    PowerItemPool poweritem;
    PowerShootPool powershoot;
    ShootEnemyPool shootenemy;
    EBulletPool ebullet;
    ChargeShootPool chargetshoot;
    BoomItemPool boomitem;
    ItemEnemyPool itemenemy;
    ChargeBulletPool chargebullet;



    /// <summary>
    /// ���� �ε� �Ϸ� �� �� ���� ����Ǵ� �ʱ�ȭ �Լ�
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize(); //�������̵�

        bullet = GetComponentInChildren<BulletPool>();  // ���� �� �ڽ� ������Ʈ���� ������Ʈ ã��
        if (bullet != null)
            bullet.Initialize();

        hit = GetComponentInChildren<HitPool>();
        if (hit != null)
            hit.Initialize();

        explosion = GetComponentInChildren<ExplosionPool>();
        if (explosion != null)
            explosion.Initialize();

        rushingenemy = GetComponentInChildren<RushingEnemyPool>();
        if (rushingenemy != null)
            rushingenemy.Initialize();

        twoshootenemy = GetComponentInChildren<TwoShootEnemyPool>();
        if (twoshootenemy != null)
            twoshootenemy.Initialize();

        poweritem = GetComponentInChildren<PowerItemPool>();
        if (poweritem != null)
            poweritem.Initialize();

        powershoot = GetComponentInChildren<PowerShootPool>();
        if (powershoot != null)
            powershoot.Initialize();
        boom = GetComponentInChildren<BoomPool>();
        if (boom != null)
            boom.Initialize();
        shootenemy = GetComponentInChildren<ShootEnemyPool>();
        if (shootenemy != null)
            shootenemy.Initialize();
        chargetshoot = GetComponentInChildren<ChargeShootPool>();
        if (chargetshoot != null)
            chargetshoot.Initialize();
        ebullet = GetComponentInChildren<EBulletPool>();
        if (ebullet != null)
            ebullet.Initialize();
        boomitem = GetComponentInChildren<BoomItemPool>();
        if (boomitem != null)
            boomitem.Initialize();
        itemenemy = GetComponentInChildren<ItemEnemyPool>();
        if (itemenemy != null)
            itemenemy.Initialize();
        chargebullet = GetComponentInChildren<ChargeBulletPool>();
        if (chargebullet != null)
            chargebullet.Initialize();



    }



    /// <summary>
    /// Ǯ���ִ� ���� ������Ʈ �ϳ� ��������
    /// </summary>
    /// <param name="type">������ ������Ʈ�� ����</param>
    /// <returns>Ȱ��ȭ�� ������Ʈ</returns>
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.PlayerBullet:
                result = bullet.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.HitEffect:
                result = hit.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.ExplosionEffect:
                result = explosion.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.Boom:
                result = boom.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.RushingEnemy:
                result = rushingenemy.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.TwoShootEnemy:
                result = twoshootenemy.GetObject(position, euler).gameObject;
                break;

            case PoolObjectType.PowerItem:
                result = poweritem.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.PowerShoot:
                result = powershoot.GetObject(position, euler).gameObject;
                break;

            case PoolObjectType.ShootEnemy:
                result = shootenemy.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.ItemEnemy:
                result = itemenemy.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.BoomItem:
                result = boomitem.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.ChargeShoot:
                result = chargetshoot.GetObject(position, euler).gameObject;
                break;

            case PoolObjectType.EBullet:
                result = ebullet.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.ChargeBulet:
                result = chargebullet.GetObject(position, euler).gameObject;
                break;

        }

        return result;
    }


    public Bullet GetBullet(Vector3 position, float angle = 0.0f)
    {
        return bullet.GetObject(position, angle * Vector3.forward);
    }

    public Explosion GetHitEffect()
    {
        return hit.GetObject();
    }

    public Explosion GetHitEffect(Vector3 position, float angle = 0.0f)
    {
        return hit.GetObject(position, angle * Vector3.forward);
    }

    public Explosion GetExplosionEffect()
    {
        return explosion.GetObject();
    }

    public Explosion GetExplosionEffect(Vector3 position, float angle = 0.0f)
    {
        return explosion.GetObject(position, angle * Vector3.forward);
    }



    public Enemy GetRushingEnemy(Transform spawn)
    {
        return rushingenemy.GetObject();
    }

    public Enemy GetRushingEnemy(Vector3 position, float angle = 0.0f)
    {
        return rushingenemy.GetObject(position, angle * Vector3.forward);
    }
    public Enemy GetTwoShootEnemy()
    {
        return twoshootenemy.GetObject();
    }
    public Enemy GetTwoShootEnemy(Vector3 position, float angle = 0.0f)
    {
        return twoshootenemy.GetObject(position, angle * Vector3.forward);
    }
    public Boom GetBoom()
    {
        return boom.GetObject();
    }
    public Boom GetBoom(Vector3 position, float angle = 0.0f)
    {
        return boom.GetObject(position, angle * Vector3.forward);


    }

    public Item GetPowerItem()
    {
        return poweritem.GetObject();
    }

    public Item GetPowerItem(Vector3 position, float angle = 0.0f)
    {
        return poweritem.GetObject(position, angle * Vector3.forward);


    }

    public PowerBullet GetPowerShoot()
    {
        return powershoot.GetObject();
    }

    public PowerBullet GetPowerShoot(Vector3 position, float angle = 0.0f)
    {
        return powershoot.GetObject(position, angle * Vector3.forward);

    }
        public ShootEnemy GetShootEnemy()
    {
        return shootenemy.GetObject();
    }

    public ShootEnemy GetShootEnemy(Vector3 position, float angle = 0.0f)
    {
        return shootenemy.GetObject(position, angle * Vector3.forward);

    }
        public ChargeShoot GetChargeShoot()
    {
        return chargetshoot.GetObject();
    }

    public ChargeShoot GetChargeShoot(Vector3 position, float angle = 0.0f)
    {
        return chargetshoot.GetObject(position, angle * Vector3.forward);

    }
        public EBullet GetEBullet()
    {
        return ebullet.GetObject();
    }

    public EBullet GetEBullet(Vector3 position, float angle = 0.0f)
    {
        return ebullet.GetObject(position, angle * Vector3.forward);

    }
        public Item GetBoomItem()
    {
        return boomitem.GetObject();
    }

    public Item GetBoomItem(Vector3 position, float angle = 0.0f)
    {
        return boomitem.GetObject(position, angle * Vector3.forward);

    }
    
        public ItemEnemy GetItemEnemy()
    {
        return itemenemy.GetObject();
    }

    public ItemEnemy GetItemEnemy(Vector3 position, float angle = 0.0f)
    {
        return itemenemy.GetObject(position, angle * Vector3.forward);

    }
    
        public Bullet GetChargeBullet()
    {
        return chargebullet.GetObject();
    }

    public Bullet GetChargeBullet(Vector3 position, float angle = 0.0f)
    {
        return chargebullet.GetObject(position, angle * Vector3.forward);

    }



}
