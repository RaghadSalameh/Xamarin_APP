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
    public partial class FieldPage : ContentPage
    {
        public FieldPage()
        {
            InitializeComponent();
            var fields = App.Database.GetFields();
            listView.ItemsSource = fields;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = App.Database.GetSearchedFields(e.NewTextValue);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null) return;
            var field = e.SelectedItem as Field;
            //DisplayAlert("selected", field.FieldName,"Ok");
            await Navigation.PushAsync(new MainPage(field));
            listView.SelectedItem = null;
        }
    }
}