using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public static class GenreValues
    {
        public static Genre[] Values => (Genre[])Enum.GetValues(typeof(Genre));
    }
    public enum Genre
    {
        Роман,
        Детектив,
        Научная,
        Фантастика
    }
}
