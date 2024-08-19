using Zhankui_Wang_Prob_Asst_3_Part_1.Models;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Service
{
    public class PageLogic
    {
        public static List<Course> GetPagedList(List<Course> list, int pageNumber, int pageSize)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
