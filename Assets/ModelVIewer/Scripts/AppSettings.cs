using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ModelViewerDemo/Settings")]
public class AppSettings : ScriptableObject
{
    [Header("Icons")]
    public Sprite LocalIcon;
    public Sprite RemoteIcon;
    public Sprite DownloadedIcon;
    public Sprite RefreshIcon;
    public Sprite SettingsIcon;

}
