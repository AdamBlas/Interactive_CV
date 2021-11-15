using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHubOpener : Activable
{
    string url = "https://github.com/AdamBlas";

    public override void OnActivate()
    {
        System.Diagnostics.Process.Start(url);
    }
    public override void OnDeactivate()
    {
        System.Diagnostics.Process.Start(url);
    }
}
