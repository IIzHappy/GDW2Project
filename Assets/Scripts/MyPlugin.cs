using System.Runtime.InteropServices;

public static class MyPlugin 
{
    [DllImport("DucknCoverDLL")]
    public static extern void ResetRun();
    [DllImport("DucknCoverDLL")]
    public static extern void UpdateRun(float deltaTime);
    [DllImport("DucknCoverDLL")]
    public static extern void EndRun();
    [DllImport("DucknCoverDLL")]
    public static extern float GetSurvivalTime();
    [DllImport("DucknCoverDLL")]
    public static extern int GetSurvivalRating();
}
