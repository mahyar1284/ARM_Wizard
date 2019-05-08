using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ARM_Wizard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GPIO_stm32f103 : Page
    {
        string temp_gpio_cfg;
        string gpiox;
        string pinx;
        string mode;
        string speed;
        int state;
        int CRR = 0;
        int eint = 0;
        int gpiox_index = 0;
        int linex = 0;
        bool kls = false;


        public GPIO_stm32f103()
        {
            this.InitializeComponent();
        }
        int dec2bin(int dec)
        {
            int c = 1, n = 0;
            for (int i = 0; i < 16; i++)
            {
                n = n + ((dec / c) % 2) * (int)(10 ^ i);
                c *= 2;
            }
            return n;
        }
        private void Gnrbtn_Click(object sender, RoutedEventArgs e)
        {
            //         get data from user           //

            if (!(iocombo.SelectedItem == null || pincombo.SelectedItem == null))
            {
                gpiox = iocombo.SelectedItem.ToString();
                gpiox_index = iocombo.SelectedIndex;
                pinx = pincombo.SelectedItem.ToString();
                mode = modecombo.SelectedIndex.ToString();
                speed = speedcombo.SelectedIndex.ToString();
                state = statecombo.SelectedIndex;
                eint = eintcombo.SelectedIndex;
                linex = linecombo.SelectedIndex;

                if (int.Parse(mode) <= 2)
                {
                    CRR = 0X00;
                    CRR = CRR + (int.Parse(mode) << 2);
                }
                else
                {
                    CRR = int.Parse(speed) + 1;
                    mode = (int.Parse(mode) - 3).ToString();
                    CRR = CRR + (int.Parse(mode) << 2);
                }
                /////////////////////////////////////////////////////
                if (kls)
                {
                    if (int.Parse(pinx) > 7)
                    {
                        temp_gpio_cfg = gpiox + "->CRH" + " &= " + " ~(" + (16).ToString() + "<<" + ((int.Parse(pinx) - 8) * 4).ToString() + ");\r\n";
                        temp_gpio_cfg = temp_gpio_cfg + gpiox + "->CRH" + " |= " + " (" + CRR.ToString() + "<<" + ((int.Parse(pinx) - 8) * 4).ToString() + ");";
                    }
                    else
                    {
                        temp_gpio_cfg = gpiox + "->CRL" + " &= " + " ~(" + (16).ToString() + "<<" + (int.Parse(pinx) * 4).ToString() + ");\r\n";
                        temp_gpio_cfg = temp_gpio_cfg + gpiox + "->CRL" + " |= " + " (" + CRR.ToString() + "<<" + (int.Parse(pinx) * 4).ToString() + ");";
                    }

                    if (state == 1)
                        temp_gpio_cfg = temp_gpio_cfg + "\r\n" + gpiox + "->BSRR |= " + "(1<<" + pinx + ");";
                    else
                        temp_gpio_cfg = temp_gpio_cfg + "\r\n" + gpiox + "->BSRR |= " + "(1<<" + (int.Parse(pinx) + 16).ToString() + ");";

                }
                else
                {
                    if (int.Parse(pinx) > 7)
                    {
                        temp_gpio_cfg = gpiox + "->CRH" + " = " + " (" + CRR.ToString() + "<<" + ((int.Parse(pinx) - 8) * 4).ToString() + ");";
                    }
                    else
                    {
                        temp_gpio_cfg = gpiox + "->CRL" + " = " + " (" + CRR.ToString() + "<<" + (int.Parse(pinx) * 4).ToString() + ");";
                    }
                    if (state == 1)
                        temp_gpio_cfg = temp_gpio_cfg + "\r\n" + gpiox + "->BSRR = " + "(1<<" + pinx + ");";
                    else
                        temp_gpio_cfg = temp_gpio_cfg + "\r\n" + gpiox + "->BSRR = " + "(1<<" + (int.Parse(pinx) + 16).ToString() + ");";
                }
                if (eint > 0)
                {
                    if (int.Parse(mode) == 1 || int.Parse(mode) == 2)
                    {
                        if (int.Parse(pinx) < 4)
                        {
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->CR1" + " &= " + " ~(" + (16).ToString() + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");\r\n";
                            temp_gpio_cfg = temp_gpio_cfg + "EXTI->CR1" + " |= " + " (" + gpiox_index + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");";
                        }
                        else if (int.Parse(pinx) < 8)
                        {
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->CR2" + " &= " + " ~(" + (16).ToString() + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");\r\n";
                            temp_gpio_cfg = temp_gpio_cfg + "EXTI->CR2" + " |= " + " (" + gpiox_index + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");";
                        }
                        else if (int.Parse(pinx) < 12)
                        {
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->CR3" + " &= " + " ~(" + (16).ToString() + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");\r\n";
                            temp_gpio_cfg = temp_gpio_cfg + "EXTI->CR3" + " |= " + " (" + gpiox_index + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");";
                        }
                        else if (int.Parse(pinx) < 16)
                        {
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->CR1" + " &= " + " ~(" + (16).ToString() + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");\r\n";
                            temp_gpio_cfg = temp_gpio_cfg + "EXTI->CR1" + " |= " + " (" + gpiox_index + "<<" + ((int.Parse(pinx)) * 4).ToString() + ");";
                        }
                        temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->IMR" + " |= " + " (1<<" + pinx + ");";
                        if (eint == 1)
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->RTSR" + " |= " + " (1<<" + pinx + ");";
                        if (eint == 2)
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->FTSR" + " |= " + " (1<<" + pinx + ");";
                        if (eint == 3)
                        {
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->RTSR" + " |= " + " (1<<" + pinx + ");";
                            temp_gpio_cfg = temp_gpio_cfg + "\r\nEXTI->FTSR" + " |= " + " (1<<" + pinx + ");";
                        }
                        DisplayNvicForgetDialog();
                    }
                    else
                    {
                        DisplayNotIntAvailDialog();
                    }
                }
            }
            else
            {
                DisplayfillcmpltDialog();
            }
            codetxt.Text = temp_gpio_cfg;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                string colorName = rb.Tag.ToString();
                switch (colorName)
                {
                    case "KLS":
                        kls = true;
                        break;
                    case "DKLS":
                        kls = false;
                        break;
                }
            }
        }
        private async void DisplayNvicForgetDialog()
        {
            ContentDialog nvicforgetdialog = new ContentDialog
            {
                Title = "Enable AFIO/GPIOx CLKs",
                Content = "Don't forget to Enable GPIO and AFIO clocks \r\n Also don't Forget To Config NVIC Registers",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await nvicforgetdialog.ShowAsync();
        }
        private async void DisplayfillcmpltDialog()
        {
            ContentDialog notcmpltdialog = new ContentDialog
            {
                Title = "Cannot Generate The Code",
                Content = "Please Fill up the form completly",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await notcmpltdialog.ShowAsync();
        }
        private async void DisplayNotIntAvailDialog()
        {
            ContentDialog NotIntAvaildialog = new ContentDialog
            {
                Title = "Cannot Generate The Code",
                Content = "interrupt is only available when pin mode is input",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await NotIntAvaildialog.ShowAsync();
        }

        private void Addtofuncbtn_Click(object sender, RoutedEventArgs e)
        {
            shared_var.GPIO_Func_stm32f103 = "void GPIO_Config()\r\n" +
                "{\r\n" +
                temp_gpio_cfg + "\r\n" +
                "}\r\n";
        }
    }


}

