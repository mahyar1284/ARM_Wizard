using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ARM_Wizard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();

            //myframe.Navigate(typeof(HomePage));
            //nav_list.SelectedItem = home_box;
            //mainframe.Navigate(typeof(Home_stm32f103));



        }

        private void togglebtn_Click(object sender, RoutedEventArgs e)
        {
            //if()
            try
            {
                (mainframe.Content as Home_stm32f103).STf103Split();
            }
            catch { };

            //mysplitview.IsPaneOpen = !mysplitview.IsPaneOpen;
        }

        private void Mcubrandcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mcumodelcombo.Items.Clear();
            if (mcubrandcombo.SelectedIndex == 0)
            {
                mcumodelcombo.Items.Add("ST");
                mcumodelcombo.Items.Add("LPC(unavailable)");
            }
            //if (mcubrandcombo.SelectedIndex == 1)
            //{
            //    mcumodelcombo.Items.Add("DSPIC");
            //    mcumodelcombo.Items.Add("PIC");
            //}
            //if (mcubrandcombo.SelectedIndex == 2)
            //{
            //    mcumodelcombo.Items.Add("HOLTEK");
            //}
            //if (mcubrandcombo.SelectedIndex == 3)
            //{
            //    mcumodelcombo.Items.Add("ATXMEGA");
            //    mcumodelcombo.Items.Add("ATMEGA");
            //}
            //if (mcubrandcombo.SelectedIndex == 4)
            //{
            //    mcumodelcombo.Items.Add("INTEL");
            //}

        }

        private void Mcumodelcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mcubrandcombo.SelectedIndex == 0)
            {
                mcusubmodelcombo.Items.Clear();
                if (mcumodelcombo.SelectedIndex == 0)
                {
                    mcusubmodelcombo.Items.Add("stm32f103c8");
                    mcusubmodelcombo.Items.Add("stm32f405rg");
                }
            }
        }

        private async void Newprjbtn_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Micro_Wizard Project", new List<string>() { ".mcpr" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Micro_prj";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, "chip_select : " +
                    mcubrandcombo.SelectedItem.ToString() + " -> " +
                    mcumodelcombo.SelectedItem.ToString() + " -> " +
                    mcusubmodelcombo.SelectedItem.ToString() + "\r\n");
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    //this.textBlock.Text = "File " + file.Name + " was saved.";
                }
                else
                {
                    //this.textBlock.Text = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }
            shared_var.project_line = await Windows.Storage.FileIO.ReadTextAsync(file);
            if (shared_var.project_line.Contains("chip_select : ARM -> ST -> stm32f103c8\r\n"))
            {
                mainframe.Navigate(typeof(Home_stm32f103));
            }
            else
            {
                DisplayunsopportedprjtDialog();
            }
        }

        private async void Loadprjbtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".mcpr");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            shared_var.project_line = await Windows.Storage.FileIO.ReadTextAsync(file);
            if (file != null)
            {
                if (shared_var.project_line.Contains("chip_select : ARM -> ST -> stm32f103c8\r\n"))
                {
                    mainframe.Navigate(typeof(Home_stm32f103));
                    mcubrandcombo.SelectedIndex = 0;
                    mcumodelcombo.SelectedIndex = 0;
                    mcusubmodelcombo.SelectedIndex = 0;
                }
                else
                {
                    DisplayunsopportedprjtDialog();
                }
                // Application now has read/write access to the picked file
                //this.textBlock.Text = "Picked photo: " + file.Name;
            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }
        }

        private async void DisplayunsopportedprjtDialog()
        {
            ContentDialog nvicforgetdialog = new ContentDialog
            {
                Title = "Cannot Open The Project",
                Content = "Open Or Create Another Project",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await nvicforgetdialog.ShowAsync();
        }

        private async void Gnrprjbtn_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("c code", new List<string>() { ".c" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "code";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, "//gpio configurations\r\n" +
                    shared_var.GPIO_Func_stm32f103);
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    //this.textBlock.Text = "File " + file.Name + " was saved.";
                }
                else
                {
                    //this.textBlock.Text = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }
        }
    }
}
