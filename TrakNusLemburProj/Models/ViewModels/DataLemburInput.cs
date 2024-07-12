namespace TrakNusLemburProj.Models.ViewModels
{
    public class DataLemburInput
    {
        public string NRP { get; set; }
        public string Name { get; set; }
        public string Divisi { get; set; }
        public string Department { get; set; }
        public DateTime TglLembur { get; set; }
        public DateTime MulaiLembur { get; set; }
        public DateTime AkhirLembur { get; set; }
    }
}
