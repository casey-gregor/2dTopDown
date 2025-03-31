namespace ProjectFiles.Scripts.HealthBar
{
    public interface IView<in T>
    {
        public void SetupView(T value);
        public void UpdateData(T value);
    }
}