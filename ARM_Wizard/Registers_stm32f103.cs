namespace Stm32f103_Registers
{
    namespace RCC_Register
    {
        public enum RCC_CR : byte
        {
            HSION = 0,
            HSIRDY = 1,
            HSITRIM = 3,
            HSICAL = 8,
            HSEON = 16,
            HSERDY = 17,
            HSEBYP = 18,
            CSSON = 19,
            PLLON = 24,
            PLLRDY = 25
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

        public enum RCC_CFGR : byte
        {
            SW = 0,
            SWS = 2,
            HPRE = 4,
            PPRE1 = 8,
            PPRE2 = 11,
            ADCPRE = 14,
            PLLSRC = 16,
            PLLXTPRE = 17,
            PLLMUL = 18,
            USBPRE = 22,
            MCO = 24
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
