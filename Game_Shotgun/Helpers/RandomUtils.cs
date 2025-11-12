public static class RandomUtils //Hjälpmetode som kan användas utan objekt.
{
    private static readonly Random random = new(); //För att inte samma tal ska slumpas flera gånger i rad.

    public static int BotsChoice() => random.Next(1, 4);
}
