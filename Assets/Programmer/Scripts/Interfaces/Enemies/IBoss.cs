﻿using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IBoss
{
    public float MaxHealth { get; }
    public float Health { get; set; }
    public bool OnPause { get; set; }
    public bool OnCutScene { get; set; }
    public VFXManager VFXManager { get; }
    public PauseManager PauseManager { get; }
    public EventBus EventBus { get; }
    public SoundsController SoundsController { get; }
    public LayerMask PlayerLayer { get; }
    public void Init(GameManager gameManager, Transform player);
    public Vector3 GetPosition();
    public UniTask ChangeMaterial();
    public void Die();
}
