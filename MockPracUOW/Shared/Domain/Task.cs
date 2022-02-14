using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPracUOW.Shared.Domain
{
	public class Task
	{
		public int Id { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = "Name does not match requirements. Maximum 15 words")]
		public string TaskName { get; set; }
		public bool IsCompleted { get; set; }
		[Required]
		public DateTime? DueDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
	}
}
