using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : Panel {

    private void Awake()
    {
        panelType = PanelEnum.GameOverPanel;
    }

    public void ClickedRetry()
    {
        EventManager.Retry();
    }
}
