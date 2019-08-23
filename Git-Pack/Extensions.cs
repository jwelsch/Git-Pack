namespace Git_Pack
{
    public static class Extensions
    {
        public static string Pluralize(this string text, string plural, int number)
        {
            return number == 1 ? text : plural;
        }
    }
}
