using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationDrawer : MasterDetailPage
    {
        List<MenuItems> menu;
        public NavigationDrawer()
        {
            InitializeComponent();
            menu = new List<MenuItems>();
            menu.Add(new MenuItems { OptionName = "Main Page" , IconName= "map.png" });
            menu.Add(new MenuItems { OptionName = "My Field" , IconName= "location.png" });
            menu.Add(new MenuItems { OptionName = "My Profile", IconName = "profile.png" });
            menu.Add(new MenuItems { OptionName = "Logout", IconName = "logout.png" });
            navigationList.ItemsSource = menu;
            Detail = new NavigationPage(new MainPage());
        }

        private void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        { 
            try
            {
                //if (e.SelectedItem == null) return;
                var item = e.SelectedItem as MenuItems;

                switch (item.OptionName)
                {
                    case "Main Page":
                        {
                            Detail = new NavigationPage(new MainPage());
                            IsPresented = false;
                        }
                        break;
                    case "My Field":
                        {
                            Detail = new NavigationPage(new FieldPage());
                            IsPresented = false;
                        }
                        break;
                    case "My Profile":
                        {

                        }
                        break;
                }

                //navigationList.SelectedItem = null;
            }
            catch (Exception ex)
            {

            }
        }
    }


    public class MenuItems
    {
        public string OptionName { get; set; }

        public string IconName { get; set; }
    }
}