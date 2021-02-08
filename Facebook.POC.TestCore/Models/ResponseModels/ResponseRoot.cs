using System.Collections.Generic;

namespace Facebook.POC.TestCore.Models.ResponseModels
{
    public class ResponseRoot<T>
    {
        public List<T> Data { get; set; }

        public Paging Paging { get; set; }
    }

    public class Paging
    {
        public Cursors Cursors { get; set; }
    }

    public class Cursors
    {
        public string Before { get; set; }

        public string After { get; set; }
    }
}
