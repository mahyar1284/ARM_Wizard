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
    public enum RCC_CR_Val : byte
    {
        HSION = 0,
        HSIRDY = 0,
        HSITRIM = 0,
        HSICAL = 0,
        HSEON = 0,
        HSERDY = 0,
        HSEBYP = 0,
        CSSON = 0,
        PLLON = 0,
        PLLRDY = 0
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
        MCO = 24,
    }
    public enum RCC_CFGR_Val : byte
    {
        SW = 0,
        SWS = 0,
        HPRE = 0,
        PPRE1 = 0,
        PPRE2 = 0,
        ADCPRE = 0,
        PLLSRC = 0,
        PLLXTPRE = 0,
        PLLMUL = 0,
        USBPRE = 0,
        MCO = 0
    }
}