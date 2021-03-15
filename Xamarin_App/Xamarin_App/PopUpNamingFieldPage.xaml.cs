using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_App.Models;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpNamingFieldPage 
    {
        boundary _Boundary;
        public delegate void UpdateMap();
        public event UpdateMap UpdateMapEvent;
        public PopUpNamingFieldPage(boundary Boundary)
        {
            _Boundary = Boundary;          
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }

        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            string fieldName = (field_name.Text != null ? field_name.Text : field_name.Placeholder);
            var field = new Field { FieldName = fieldName };
            App.Database.SaveField(field);
            foreach (var p in _Boundary.features[0].geometry.coordinates[0])
            {
                var coordinate = new Coordinate { fieldId = field.Id , lat = p[1], lng = p[0] };
                int coordinateId = App.Database.SaveCoordinate(coordinate);
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {         
            await PopupNavigation.Instance.PopAsync(true);
        }

        protected override void OnDisappearing()
        {
            UpdateMapEvent?.Invoke();
        }
    }
}