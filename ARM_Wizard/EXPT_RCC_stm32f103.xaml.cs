using RCC_Register;
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
        bool pll_input = false;
        public EXPT_RCC_stm32f103()
        {
            this.InitializeComponent();
            outstr = "RCC->CR = " + "(" + RCC_CR_Val.HSEON.ToString() + "<<" + RCC_CR.HSEON.ToString() + ") |" +
                  "(" + RCC_CR_Val.HSITRIM.ToString() + "<<" + RCC_CR.HSITRIM.ToString() + ") |" +
                  "(" + RCC_CR_Val.HSICAL.ToString() + "<<" + RCC_CR.HSICAL.ToString() + ") |" +
                  "(" + RCC_CR_Val.HSEON.ToString() + "<<" + RCC_CR.HSEON.ToString() + ") |" +
                  "(" + RCC_CR_Val.HSEBYP.ToString() + "<<" + RCC_CR.HSEBYP.ToString() + ") |" +
                  "(" + RCC_CR_Val.CSSON.ToString() + "<<" + RCC_CR.CSSON.ToString() + ") |" +
                  "(" + RCC_CR_Val.PLLON.ToString() + "<<" + RCC_CR.PLLON.ToString() + ") |";

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
    }
}
