using System.ComponentModel.DataAnnotations;
namespace MVC_Rehber.Entities
{
	public class Rehber
	{
		[Key]
		public int RehberID { get; set; }
		
		public string Ad { get; set; }
		public string Soyad { get; set; }
		public string CepTelefon { get; set; }
		public string IsTelefon { get; set; }
		public string MailAdres { get; set; }

		public int UyeID { get; set; } // FK
        public virtual List<Uye> Uyeler { get; set; }


    }
}
