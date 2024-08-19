using Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public class StudentAccountViewModel
{
    public int StudentID { get; set; }
    public string FullName { get; set; }
    public string FeePolicy { get; set; }
    public decimal Balance { get; set; }
    public DateTime LastChanged { get; set; }
    public List<StatementEntry> StatementEntries { get; set; }
}
