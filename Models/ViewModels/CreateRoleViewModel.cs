using System.ComponentModel.DataAnnotations;

namespace GESTIONDESETUDIANTS.Models.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		[Display(Name = "Role Name")]
		public string RoleName { get; set; }
	}
}
