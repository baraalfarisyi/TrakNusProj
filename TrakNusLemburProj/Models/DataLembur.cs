namespace TrakNusLemburProj.Models
{
    public class DataLembur
    {
        public Guid Id { get; set; }
        public string NRP { get; set; }
        public string Name { get; set; }
        public string Divisi { get; set; }
        public string Department { get; set; }
        public DateTime TglLembur { get; set; }
        public DateTime MulaiLembur { get; set; }
        public DateTime AkhirLembur { get; set; }
        public decimal TotalJamLembur
        {
            get
            {
                return (decimal)(AkhirLembur - MulaiLembur).TotalHours;
            }
        }
    }
}
