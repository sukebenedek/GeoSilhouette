using GeoSilhouette.Classes;
using System.Text.Json;

namespace GeoSilhouette
{
    public partial class App : Application
    {

        public static List<Country>? Countries { get; set; }

        public App(){
            InitializeComponent();

            MainPage = new AppShell();

            Countries = Task.Run(async () => await API.GetCountries()).Result?.Where(c => c.unMember).ToList();

        }


    }
}
