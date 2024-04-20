using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public enum Lastname
    { 
        Тургенев,
        Пушкин,
        Толстой,
        Достоевский,
        Чехов,
        Булгаков
    }
    public static class LastnameValues
    {
        public static Lastname[] Values => (Lastname[])Enum.GetValues(typeof(Lastname));
    }
}
