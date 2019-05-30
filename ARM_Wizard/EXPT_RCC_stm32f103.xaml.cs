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
        string RCC_HSE_MODE;
        RCC_CFGR_Val rcc_cfgr_val = new RCC_CFGR_Val();
        RCC_CR_Val rcc_cr_val = new RCC_CR_Val();
        byte sys_clock_src;
        byte pll_clock_src;

        #region Static Strings
        static readonly string endline = "\r\n";
        static readonly string tab1x = "\t";
        static readonly string tab2xx = "\t\t";
        static readonly string tab3xxx = "\t\t\t";
        static readonly string tab4xxxx = "\t\t\t\t";
        static readonly string HSI_Wait = "while(!" + RCC_CR.HSIRDY.ToString() + ");";
        static readonly string HSE_Wait = "while(!" + RCC_CR.HSERDY.ToString() + ");";
        static readonly string PLL_Wait = "while(!" + RCC_CR.PLLRDY.ToString() + ");";
        static readonly string RCC_Reset = "/* Reset RCC Registers to 8MHz HSI Clock*/" + endline +
            "/* Set HSION bit */" + endline +
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

        private void RelativePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((Frame)Window.Current.Content).ActualWidth > 800)
            {

            }
            //VisualStateManager.GoToState();
        }

        private void Gnr_btn_Click(object sender, RoutedEventArgs e)
        {
            rcc_cr_val.HSEON = 1;
            rcc_cfgr_val.PLLMUL = byte.Parse(PLL_MUL_ONUI.Text);
            switch (sys_clock_src)
            {
                case 0:
                    #region HSI
                    outstr = RCC_Reset;
                    outstr += endline +
                        "/* Setting AHB/Core and APB1/2 and ADC1/2 Prescalers */" + endline +
                        "RCC->CFGR |= (" + AHBCoreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.HPRE).ToString() + "); // AHB/Core Prescaler" + endline +
                        "RCC->CFGR |= (" + APB1OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE1).ToString() + "); // APB1 Prescaler" + endline +
                        "RCC->CFGR |= (" + APB2OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE2).ToString() + "); // APB2 Prescaler" + endline +
                        "RCC->CFGR |= (" + ADCPreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.ADCPRE).ToString() + "); // ADC Prescaler" + endline +
                        "/* ------------------------------------- */" + endline +
                        "//FLASH->ACR |= (" + FlashLatOnUI.SelectedIndex.ToString() + " << 0); // Setting Flash R/W Letency" + endline +
                        "SCB->VTOR = FLASH_BASE | VECT_TAB_OFFSET; // Vector Table Relocation in Internal FLASH.";
                    #endregion
                    break;
                case 1:
                    #region HSE
                    RCC_HSE_MODE = "/*RCC Registers to HSE Clock*/" + endline +
                       "{" + endline +
                       tab1x + "uint32_t StartUpCounter = 0, HSEStatus = 0;" + endline +
                       tab1x + "/* Enable HSE */" + endline +
                       tab1x + "RCC->CR |= (" + rcc_cr_val.HSEON.ToString() + "<< " + ((byte)RCC_CR.HSEON).ToString() + ");" + endline +
                       tab1x + "/* Wait till HSE is ready and if Time out is reached exit */" + endline +
                       tab1x + "do" + endline +
                       tab1x + "{" + endline +
                       tab2xx + "HSEStatus = RCC->CR & RCC_CR_HSERDY;" + endline +
                       tab2xx + "StartUpCounter++;" + endline +
                       tab1x + "} while((RCC->CR & (1 << " + ((byte)RCC_CR.HSERDY).ToString() + ") == 0) && (StartUpCounter != HSE_STARTUP_TIMEOUT));" + endline +
                       tab1x + "if((RCC->CR & 1 << " + ((byte)RCC_CR.HSERDY).ToString() + ")) == (1 << " + ((byte)RCC_CR.HSERDY).ToString() + "))" + endline +
                       tab1x + "{" + endline +
                       tab2xx + "FLASH->ACR |= (1 << 4); // Enabling Flash Buffer Prefetch" + endline +
                       tab2xx + "FLASH->ACR &= (3 << 0); // Setting R/W Letency To Zero" + endline +
                       tab2xx + "//FLASH->ACR |= (" + FlashLatOnUI.SelectedIndex.ToString() + " << 0); // Setting Flash R/W Letency" + endline +
                       tab2xx + "/* Setting AHB/Core and APB1/2 and ADC1/2 Prescalers */" + endline +
                       tab2xx + "RCC->CFGR |= (" + AHBCoreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.HPRE).ToString() + "); // AHB/Core Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + APB1OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE1).ToString() + "); // APB1 Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + APB2OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE2).ToString() + "); // APB2 Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + ADCPreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.ADCPRE).ToString() + "); // ADC Prescaler" + endline +
                       tab2xx + "/* ------------------------------------- */" + endline +
                       tab2xx + "RCC->CFGR &= !(3" + " << " + ((byte)RCC_CFGR.SW).ToString() + "); // resetting sys clk src mux" + endline +
                       tab2xx + "RCC->CFGR |= (" + ((byte)rcc_cfgr_val.SW).ToString() + " << " + ((byte)RCC_CFGR.SW).ToString() + "); // selecting sys clk src" + endline +
                       tab2xx + "while((RCC->CFGR & (1 << " + ((byte)RCC_CFGR.SWS).ToString() + ")) != (" + sys_clock_src.ToString() + " << " + ((byte)RCC_CFGR.SWS).ToString() + ")); // wait" + endline +
                       tab1x + "} else" + endline +
                       tab1x + "{" + endline +
                       tab2xx + "//error!" + endline +
                       tab1x + "}" + endline +
                       tab1x + "SCB->VTOR = FLASH_BASE | VECT_TAB_OFFSET; // Vector Table Relocation in Internal FLASH." + endline +
                       tab1x + "" + endline +
                       "} //-------------------------//" + endline;
                    outstr = RCC_Reset + endline + RCC_HSE_MODE;
                    #endregion
                    break;
                case 2:
                    if (pll_clock_src == 1)
                    {
                        RCC_HSE_MODE = "/*RCC Registers to HSE Clock*/" + endline +
                       "{" + endline +
                       tab1x + "uint32_t StartUpCounter = 0, HSEStatus = 0;" + endline +
                       tab1x + "/* Enable HSE */" + endline +
                       tab1x + "RCC->CR |= (" + rcc_cr_val.HSEON.ToString() + "<< " + ((byte)RCC_CR.HSEON).ToString() + ");" + endline +
                       tab1x + "/* Wait till HSE is ready and if Time out is reached exit */" + endline +
                       tab1x + "do" + endline +
                       tab1x + "{" + endline +
                       tab2xx + "HSEStatus = RCC->CR & RCC_CR_HSERDY;" + endline +
                       tab2xx + "StartUpCounter++;" + endline +
                       tab1x + "} while((RCC->CR & (1 << " + ((byte)RCC_CR.HSERDY).ToString() + ") == 0) && (StartUpCounter != HSE_STARTUP_TIMEOUT));" + endline +
                       tab1x + "if((RCC->CR & 1 << " + ((byte)RCC_CR.HSERDY).ToString() + ")) == (1 << " + ((byte)RCC_CR.HSERDY).ToString() + "))" + endline +
                       tab1x + "{" + endline +
                       tab2xx + "FLASH->ACR |= (1 << 4); // Enabling Flash Buffer Prefetch" + endline +
                       tab2xx + "FLASH->ACR &= (3 << 0); // Resetting Flash R/W Letency" + endline +
                       tab2xx + "//FLASH->ACR |= (" + FlashLatOnUI.SelectedIndex.ToString() + " << 0); // Setting Flash R/W Letency" + endline +
                       tab2xx + "/* Setting AHB/Core and APB1/2 and ADC1/2 Prescalers */" + endline +
                       tab2xx + "RCC->CFGR |= (" + AHBCoreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.HPRE).ToString() + "); // AHB/Core Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + APB1OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE1).ToString() + "); // APB1 Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + APB2OnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.PPRE2).ToString() + "); // APB2 Prescaler" + endline +
                       tab2xx + "RCC->CFGR |= (" + ADCPreOnUI.SelectedIndex.ToString() + " << " + ((byte)RCC_CFGR.ADCPRE).ToString() + "); // ADC Prescaler" + endline +
                       tab2xx + "/* ------------------------------------- */" + endline;

                        outstr = RCC_Reset + endline + RCC_HSE_MODE + tab1x + "/*PLL Configuration*/" + endline +
                           tab2xx + "RCC->CFGR &= (uint32_t)((uint32_t)~(RCC_CFGR_PLLSRC | RCC_CFGR_PLLXTPRE | RCC_CFGR_PLLMULL)); " + endline +
                           tab2xx + "RCC->CFGR |= (uint32_t)(RCC_CFGR_PLLSRC_HSE | RCC_CFGR_PLLMULL" + ((byte)rcc_cfgr_val.PLLMUL).ToString() + ");" + endline +
                           tab2xx + "RCC->CR |= RCC_CR_PLLON; // turning on the pll system." + endline +
                           tab2xx + "while((RCC->CR & RCC_CR_PLLRDY) == 0); // waiting till pll becomes ready." + endline +
                           tab2xx + "RCC->CFGR &= (uint32_t)((uint32_t)~(RCC_CFGR_SW)); // resetting sys clock src" + endline +
                           tab2xx + "RCC->CFGR |= (uint32_t)RCC_CFGR_SW_PLL;  // setting sys clock src to pll" + endline +
                           tab2xx + "while ((RCC->CFGR & (uint32_t)RCC_CFGR_SWS) != (uint32_t)0x08); /* Wait till PLL is used as system clock source */" + endline +
                           tab2xx + "SCB->VTOR = FLASH_BASE | VECT_TAB_OFFSET; // Vector Table Relocation in Internal FLASH." + endline +
                           tab1x + "} else" + endline +
                           tab1x + "{" + endline +
                           tab2xx + "//error!" + endline +
                           tab1x + "}" + endline +
                           tab1x + "" + endline +
                           "} //-------------------------//" + endline;
                    }
                    if (pll_clock_src == 0)
                    {
                        outstr = RCC_Reset + endline + tab1x + "/*PLL Configuration*/" + endline +
                           tab2xx + "RCC->CFGR &= (uint32_t)((uint32_t)~(RCC_CFGR_PLLSRC | RCC_CFGR_PLLXTPRE | RCC_CFGR_PLLMULL)); " + endline +
                           tab2xx + "RCC->CFGR |= (uint32_t)(RCC_CFGR_PLLSRC_HSI | RCC_CFGR_PLLMULL" + ((byte)rcc_cfgr_val.PLLMUL).ToString() + ");" + endline +
                           tab2xx + "RCC->CR |= RCC_CR_PLLON; // turning on the pll system." + endline +
                           tab2xx + "while((RCC->CR & RCC_CR_PLLRDY) == 0); // waiting till pll becomes ready." + endline +
                           tab2xx + "RCC->CFGR &= (uint32_t)((uint32_t)~(RCC_CFGR_SW)); // resetting sys clock src" + endline +
                           tab2xx + "RCC->CFGR |= (uint32_t)RCC_CFGR_SW_PLL;  // setting sys clock src to pll" + endline +
                           tab2xx + "while ((RCC->CFGR & (uint32_t)RCC_CFGR_SWS) != (uint32_t)0x08); /* Wait till PLL is used as system clock source */" + endline +
                           tab2xx + "//FLASH->ACR |= (" + FlashLatOnUI.SelectedIndex.ToString() + " << 0); // Setting Flash R/W Letency" + endline +
                           tab2xx + "SCB->VTOR = FLASH_BASE | VECT_TAB_OFFSET; // Vector Table Relocation in Internal FLASH." + endline +
                           tab1x + "} else" + endline +
                           tab1x + "{" + endline +
                           tab2xx + "//error!" + endline +
                           tab1x + "}" + endline +
                           tab1x + "" + endline +
                           "} //-------------------------//" + endline;
                    }
                    break;
            }
            mycode.Text = outstr;
        }

        private void sysinputclkrdbtn_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                string localstr = rb.Tag.ToString();
                if (localstr == "HSI")
                    sys_clock_src = 0;
                if (localstr == "HSE")
                    sys_clock_src = 1;
                if (localstr == "PLL")
                    sys_clock_src = 2;
            }
        }

        private void pllsrcrdbtn_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                string localstr = rb.Tag.ToString();
                if (localstr == "HSI")
                    pll_clock_src = 0;
                if (localstr == "HSE")
                    sys_clock_src = 1;
            }
        }
    }
}