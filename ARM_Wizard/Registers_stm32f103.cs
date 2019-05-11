namespace Stm32f103_Registers
{
    namespace RCC_Register
    {
        public class RCC_CR
        {
            public const byte HSION = 0;
            public const byte HSIRDY = 1;
            public const byte HSITRIM = 3;
            public const byte HSICAL = 8;
            public const byte HSEON = 16;
            public const byte HSERDY = 17;
            public const byte HSEBYP = 18;
            public const byte CSSON = 19;
            public const byte PLLON = 24;
            public const byte PLLRDY = 25;
        }
        public class RCC_CR_Val
        {
            public byte HSION = 0;
            public byte HSIRDY = 0;
            public byte HSITRIM = 0;
            public byte HSICAL = 0;
            public byte HSEON = 0;
            public byte HSERDY = 0;
            public byte HSEBYP = 0;
            public byte CSSON = 0;
            public byte PLLON = 0;
            public byte PLLRDY = 0;
        }

        public class RCC_CFGR
        {
            public byte SW = 0;
            public byte SWS = 2;
            public byte HPRE = 4;
            public byte PPRE1 = 8;
            public byte PPRE2 = 11;
            public byte ADCPRE = 14;
            public byte PLLSRC = 16;
            public byte PLLXTPRE = 17;
            public byte PLLMUL = 18;
            public byte USBPRE = 22;
            public byte MCO = 24;
        }
        public class RCC_CFGR_Val
        {
            public byte SW = 0;
            public byte SWS = 0;
            public byte HPRE = 0;
            public byte PPRE1 = 0;
            public byte PPRE2 = 0;
            public byte ADCPRE = 0;
            public byte PLLSRC = 0;
            public byte PLLXTPRE = 0;
            public byte PLLMUL = 0;
            public byte USBPRE = 0;
            public byte MCO = 0;
        }
    }
}
