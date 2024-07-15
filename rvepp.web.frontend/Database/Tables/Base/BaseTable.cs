namespace rvepp.web.frontend.Database.Tables.Base
{
    public class BaseTable
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool Active { get; set; }
    }
}