public enum FoodEnum
{
    None,
    LettuceRaw = 1,
    LettuceChopped = 4,
    TomatoRaw = 16,
    TomatoChopped = 64,
    TomatoBoiled = 256,
    MeatRaw = 1024,
    MeatFried = 4096,
    OnionRaw = 16384,
    OnionFried = 65536,
    OnionBoiled = 262144,
    // 단일 재료
    Salad = LettuceChopped + LettuceChopped,
    TomatoSalad = LettuceChopped + TomatoChopped,
    Steak = MeatFried + OnionFried,
    OnionSoup = MeatFried + OnionBoiled,
    TomatoSoup = MeatFried + TomatoBoiled
    // 요리
}
