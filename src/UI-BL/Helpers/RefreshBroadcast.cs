namespace TestJob01.UI_BL.Helpers;
internal sealed class RefreshBroadcast {
    private static readonly Lazy<RefreshBroadcast>
        Lazy =
            new Lazy<RefreshBroadcast>
                (() => new RefreshBroadcast());

    public static RefreshBroadcast Instance => Lazy.Value;

    private RefreshBroadcast() {
    }

    public event Action RefreshRequested;
    public void CallRequestRefresh() {
        RefreshRequested?.Invoke();
    }
}
