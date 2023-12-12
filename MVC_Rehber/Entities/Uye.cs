using System.ComponentModel.DataAnnotations;

namespace MVC_Rehber.Entities
{
	public class Uye
	{
		[Key]
		public int UyeID { get; set; }

		[StringLength(50)]
		[Display(Name = "Adınız")]
		[Required(ErrorMessage = "Lütfen adınızı yazınız.")]
		public string Ad { get; set; }
		[StringLength(50)]
		[Display(Name = "Soyadınız")]
		[Required(ErrorMessage = "Lütfen soyadınızı yazınız.")]
		public string Soyad { get; set; }

		[StringLength(100)]
		[Display(Name = "Kullanıcı Adı")]
		[Required(ErrorMessage = "Lütfen kullanıcı adını yazınız.")]
		public string KullaniciAdi { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Mail Adresi")]
		[StringLength(500)]
		[Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
		public string MailAdresi { get; set; }
		
		[Display(Name ="Açıklama")]
		public string Aciklama { get; set; }
		public virtual Rehber Rehber { get; set; }
	}
}
	

