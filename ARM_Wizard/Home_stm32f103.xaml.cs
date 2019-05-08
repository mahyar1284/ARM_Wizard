using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ARM_Wizard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home_stm32f103 : Page
    {
        public Home_stm32f103()
        {
            this.InitializeComponent();
        }

        public void STf103Split()
        {
            stm32f103c8splitview.IsPaneOpen = !stm32f103c8splitview.IsPaneOpen;
        }

        private void Nav_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(stm32f103c8splitview.IsPaneOpen)
            {
                stm32f103c8splitview.IsPaneOpen = false;
            }
            if (nav_list.SelectedIndex == 0)
            {

            }
            if (nav_list.SelectedIndex == 3)
            {
                myframe.Navigate(typeof(RCC_stm32f103));
            }
            if (nav_list.SelectedIndex==5)
            {
                myframe.Navigate(typeof(GPIO_stm32f103));
            }
        }
    }
}
