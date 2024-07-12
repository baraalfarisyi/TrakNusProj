using System.ComponentModel.DataAnnotations;

namespace TrakNusLemburProj.Models.ViewModels
{
    public class LemburReportViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string NRP { get; set; }
        public string Name { get; set; }
        public string Divisi { get; set; }
        public string Department { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public decimal TotalJamLembur { get; set; }
    }
}
