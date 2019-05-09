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
}