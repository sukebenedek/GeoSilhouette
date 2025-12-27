using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string silhouetteImage = App.Countries[22].SilhouetteImage;

        [ObservableProperty]
        private ObservableCollection<Country> countries = App.Countries.ToObservableCollection();

    }


}
