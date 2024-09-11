using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{

    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    PlayerStats playerStats;

    public override void InstallBindings()
    {
        Container.Bind<PlayerStats>().FromInstance(playerStats).AsSingle();
        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
    }

}
