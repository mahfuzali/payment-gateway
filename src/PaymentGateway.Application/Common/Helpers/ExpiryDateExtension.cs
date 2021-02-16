namespace PaymentGateway.Application.Common.Helpers
{
    public static class ExpiryDateExtension
    {
        public static int[] ExpiryDateArray(this string expiryDate)
        {
            string[] split = expiryDate.Split("/");
            return new int[] { int.Parse(split[0]), int.Parse(split[1]) };
        }
    }
}
