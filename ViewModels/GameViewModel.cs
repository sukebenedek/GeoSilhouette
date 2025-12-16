using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.ViewModels
{
    public partial class GameViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        public string difficulty = "not yet";

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("difficulty", out var diff))
            {
                Difficulty = diff.ToString();
                Console.WriteLine(diff);
            }
        }
    }

}
