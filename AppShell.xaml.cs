namespace GeoSilhouette
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.ChosePage), typeof(Pages.ChosePage));
            Routing.RegisterRoute(nameof(Pages.GamePage), typeof(Pages.GamePage));
            Routing.RegisterRoute(nameof(Pages.StatsPage), typeof(Pages.StatsPage));

        }
    }
}
