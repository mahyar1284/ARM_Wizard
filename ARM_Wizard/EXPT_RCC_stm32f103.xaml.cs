using Stm32f103_Registers.RCC_Register;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ARM_Wizard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EXPT_RCC_stm32f103 : Page
    {
        string outstr;
        RCC_CFGR rcc_cfgr = new RCC_CFGR();
        RCC_CFGR_Val rcc_cfgr_val = new RCC_CFGR_Val();
        RCC_CR rcc_cr = new RCC_CR();
        RCC_CR_Val rcc_cr_val = new RCC_CR_Val();

        #region Static Strings
        static readonly string endline = "\r\n";
        static readonly string HSI_Wait = "while(!" + RCC_CR.HSIRDY.ToString() + ");";
        static readonly string HSE_Wait = "while(!" + RCC_CR.HSERDY.ToString() + ");";
        static readonly string PLL_Wait = "while(!" + RCC_CR.PLLRDY.ToString() + ");";
        static readonly string RCC_Reset = "/* Set HSION bit */" + endline +
            "RCC->CR |= (1 << 0);" + endline +
            "/* Reset SW, HPRE, PPRE1, PPRE2, ADCPRE and MCO bits */" + endline +
            "RCC->CFGR &= (uint32_t)0xF8FF0000;" + endline +
            "/* Reset HSEON, CSSON and PLLON bits */" + endline +
            "RCC->CR &= (uint32_t)0xFEF6FFFF;" + endline +
            "/* Reset HSEBYP bit */" + endline +
            "RCC->CR &= (uint32_t)0xFFFBFFFF;" + endline +
            "/* Reset PLLSRC, PLLXTPRE, PLLMUL and USBPRE/OTGFSPRE bits */" + endline +
            "RCC->CFGR &= (uint32_t)0xFF80FFFF;" + endline +
            "/* Disable all interrupts and clear pending bits  */" + endline +
            "RCC->CIR = 0x009F0000;" + endline +
            "//-------------------------//";
        #endregion

        public EXPT_RCC_stm32f103()
        {
            this.InitializeComponent();





        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            //RadioButton rb = sender as RadioButton;
            //if (rb != null)
            //{
            //    string colorName = rb.Tag.ToString();
            //    switch (colorName)
            //    {
            //        case "KLS":
            //            pll_input = HSI;
            //            break;
            //        case "DKLS":
            //            pll_input = HSE;
            //            break;
            //    }
            //}
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RelativePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Frame)Window.Current.Content).ActualWidth > 800)
            {

            }
            //VisualStateManager.GoToState();
        }

        private void Gnr_btn_Click(object sender, RoutedEventArgs e)
        {
            outstr = "RCC->CR = " + "(" + rcc_cr_val.HSION.ToString() + "<<" + RCC_CR.HSION.ToString() + ") | " +
                 "(" + rcc_cr_val.HSITRIM.ToString() + "<<" + RCC_CR.HSITRIM.ToString() + ") | " +
                 "(" + rcc_cr_val.HSICAL.ToString() + "<<" + RCC_CR.HSICAL.ToString() + ") | " +
                 "(" + rcc_cr_val.HSEON.ToString() + "<<" + RCC_CR.HSEON.ToString() + ") | " +
                 "(" + rcc_cr_val.HSEBYP.ToString() + "<<" + RCC_CR.HSEBYP.ToString() + ") | " +
                 "(" + rcc_cr_val.CSSON.ToString() + "<<" + RCC_CR.CSSON.ToString() + ") | " +
                 "(" + rcc_cr_val.PLLON.ToString() + "<<" + RCC_CR.PLLON.ToString() + ");\r\n";
            mycode.Text = outstr;
        }
    }
}
