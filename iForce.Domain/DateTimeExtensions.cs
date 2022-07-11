namespace iForce.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime SetLocalIfUnspecified(this DateTime source)
            => source.Kind == DateTimeKind.Unspecified
            ? new DateTime(source.Ticks, DateTimeKind.Local)
            : source;
    }
}
