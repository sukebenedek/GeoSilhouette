using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string silhouetteImage =
            "https://raw.githubusercontent.com/djaiss/mapsicon/master/all/fr/1024.png";
    }

}
