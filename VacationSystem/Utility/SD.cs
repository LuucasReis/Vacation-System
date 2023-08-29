namespace Utility
{
    public static class SD
    {
        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum Regime
        {
            CLT,
            PJ
        }
        public const string SessionToken = "JWTToken";
        public const string statusApprove = "Aprovada";
        public const string statusReject = "Rejeitada";
        public const string defaultStatus = "Processando";

        public const string defaultPassword = "123";
    }
}